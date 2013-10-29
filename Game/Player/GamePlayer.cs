using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 게임중인 플레이어 관리;
/// </summary>
public class GamePlayer : MonoBehaviour
{
	public int playerIndex; // 플레이어의 인덱스;
	public int remainMoveCount; // 남은 이동할 블럭수;
	public int currentNum; // 현재 블럭 번호;
	public int nextNum; // 다음에 이동할 블럭 번호;
	public bool isForward = true; // 이동방향;
	public string playerName; // 플레이어 이름;
	
	public int cardTotal = 3; // 갖고 있는 카드 갯수;
	public int battleDiceTotalCount; // 총 주사위 갯수;
	public int healthTotalCount; // HP 토탈 포인트;
	public List<int> diceNumbers = new List<int> (); // 나온 각 주사위 갯수;
	
	public GameCard[] cards = new GameCard[3]; // 소지한 각 카드정보;
	
	public GameObject targetBlockObject; // 이동할 블럭 오브젝트;
	public Block targetBlock; // 이동할 블럭;

	public bool gotEvent = false; // 블럭에서의 이벤트 수행여부;
	
	public GamePlayerCharacter playerCharacter;
	private GameSceneController sceneController;
	
	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start ()
	{
		sceneController = GameSceneController.Instance;
		currentNum = 0;
		
		InitCards ();
		InitHealthTotal ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	/// <summary>
	/// 이동을 완료했을 때 호출;
	/// </summary>
	/// <returns></returns>
	public void MoveCompleted ()
	{
		int target_player_no;
		
		RemoveBlockPosition ();
		targetBlock.visitedPlayers.Add (playerIndex);
//		targetBlock.visitedPlayers.Add (DataCenter.playerTurnNo);
		
        
		if (targetBlock.visitedPlayers.Count == 2) {
			if (sceneController.movePanelManager.moveDicePanel.activeSelf) {
				sceneController.DisableMovePanel ();
			}            
			DataCenter.gameState = DataCenter.GameState.Battled;
			Debug.Log ("EnableBattlePanel");
			sceneController.EnableBattlePanel (targetBlock);

			return;
		}

		DataCenter.gameState = DataCenter.GameState.Started;

		switch (targetBlock.blockState) {
		// 블럭의 종류가 워프일경우;
			case DataCenter.BlockState.Warp1:
			case DataCenter.BlockState.Warp2:
			case DataCenter.BlockState.Warp3:
				nextNum = targetBlock.warpTargetNo;
			
				TweenMoveTo (true);
				break;
			case DataCenter.BlockState.RandomBox:
				if (!gotEvent) {
					healthTotalCount += 1;
					gotEvent = true;
					ChangePlayerTurn ();
				} else {
					ChangePlayerTurn ();
				}
				break;
			case DataCenter.BlockState.Battle: // 블럭의 종류가 배틀일경우;
				ChangePlayerTurn ();
				break;
			case DataCenter.BlockState.Monster:
				Debug.Log ("DataCenter.BlockState.Monster");
			
				if (targetBlock.monsterCard.healthPoint > 0) {
					gotEvent = true;
					if (sceneController.movePanelManager.moveDicePanel.activeSelf) {
						sceneController.DisableMovePanel ();
					}
					DataCenter.gameState = DataCenter.GameState.Monster;
					Debug.Log ("EnableMonsterPanel");
					sceneController.EnableMonsterPanel (targetBlock);

					return;
				} else {
					ChangePlayerTurn ();
				}
            
				break;
				
			case DataCenter.BlockState.Keeper:
				if (targetBlock.monsterCard.healthPoint > 0) {
					gotEvent = true;
					if (sceneController.movePanelManager.moveDicePanel.activeSelf) {
						sceneController.DisableMovePanel ();
					}
					DataCenter.gameState = DataCenter.GameState.Monster;
					Debug.Log ("EnableMonsterPanel");
					sceneController.EnableMonsterPanel (targetBlock);

					return;
				} else {
					ChangePlayerTurn ();
				}
            
				break;
			case DataCenter.BlockState.Gamble:
				if (!gotEvent) {
					gotEvent = true;
					if (sceneController.movePanelManager.moveDicePanel.activeSelf) {
						sceneController.DisableMovePanel ();
					}
					DataCenter.gameState = DataCenter.GameState.Gamble;
				
					sceneController.EnableGamblePanel (targetBlock);

					return;
				} else {
					ChangePlayerTurn ();
					break;
				}
				
			case DataCenter.BlockState.Ladder:
				if ( playerCharacter.Level >= targetBlock.levelLadder) {
					if (!gotEvent) {
						gotEvent = true;
						if (sceneController.movePanelManager.moveDicePanel.activeSelf) {
							sceneController.DisableMovePanel ();
						}
						DataCenter.gameState = DataCenter.GameState.Ladder;
						sceneController.EnableMonsterPanel (targetBlock);
						return;
					} else {
						nextNum = targetBlock.goNumberLadder;
						TweenMoveTo (true);
						return;
					}
				} 
				else {
					ChangePlayerTurn ();
					break;
				}
			case DataCenter.BlockState.RuleLow:
				if (!gotEvent) {
					gotEvent = true;
					DataCenter.battleDiceRule = DataCenter.BattleDiceRule.Low;
					DataCenter.battleRuleRemainTurn = 3;
	            
					ChangePlayerTurn ();
				} else {
					ChangePlayerTurn ();
				}
				break;
			case DataCenter.BlockState.ComeHere:
				if (!gotEvent) {
					gotEvent = true;
					target_player_no = playerIndex + 1;
					//			target_player_no = DataCenter.playerTurnNo + 1;
					if (target_player_no >= DataCenter.playerCount) {
						target_player_no = 0;
					}
					//			ChangePlayerTurn();
					//			sceneController.GamePlayersManager.players[target_player_no].RemoveBlockPosition();
					//			sceneController.gamePlayersManager.players [target_player_no].nextNum = currentNum;
					//			sceneController.gamePlayersManager.players [target_player_no].TweenMoveTo (true);
				
					sceneController.gamePlayersManager.players [target_player_no].RemoveBlockPosition ();
					sceneController.gamePlayersManager.players [target_player_no].nextNum = currentNum;
					sceneController.gamePlayersManager.players [target_player_no].TweenMoveTo (true);
					return;
				} else {
					ChangePlayerTurn ();
					break;
				}
            
//			break;
			case DataCenter.BlockState.ImGoing:
				if (!gotEvent) {
					gotEvent = true;
					target_player_no = playerIndex + 1;
					//			target_player_no = DataCenter.playerTurnNo + 1;
					if (target_player_no >= DataCenter.playerCount) {
						target_player_no = 0;
					}
					//			ChangePlayerTurn();
					nextNum = sceneController.gamePlayersManager.players [target_player_no].currentNum;
					TweenMoveTo (true);
					return;
				} else {
					ChangePlayerTurn ();
					break;
				}
//			break;
			default: // 나머지 블럭일 경우;
				ChangePlayerTurn ();
				break;
		}

		DataCenter.battleRuleRemainTurn -= 1;
		if (DataCenter.battleRuleRemainTurn <= 0) {
			DataCenter.battleRuleRemainTurn = 0;
			DataCenter.battleDiceRule = DataCenter.BattleDiceRule.High;
		}

		if (DataCenter.GameState.Started == DataCenter.gameState) {
			Debug.Log ("EnableMovePanel");
			sceneController.EnableMovePanel ();
		}
		
		isForward = true;
		
	}
	
	/// <summary>
	/// 플레이어를 이동;
	/// </summary>
	/// <returns></returns>
	public void MovePlayer ()
	{
//		RemoveBlockPosition ();
		
		if (isForward) {
			if (targetBlock != null) {
				if (targetBlock.nextFowardBlockNum != -1) {
					nextNum = targetBlock.nextFowardBlockNum;
				} else {
					nextNum = currentNum + 1;
				}
			}
			else {
				nextNum = currentNum + 1;
			}
		} else {
			if (targetBlock != null) {
				if (targetBlock.nextReverseBlockNum != -1) {
					nextNum = targetBlock.nextReverseBlockNum;
				} else {
					nextNum = currentNum - 1;
				}
			}
			else {
				nextNum = currentNum - 1;
			}
		}
		//Debug.Log ("nextNum: " + nextNum.ToString ());
		
		TweenMoveTo (false);
	}
	
	/// <summary>
	/// 각블럭 이동시 호출;
	/// </summary>
	public void MovedBlock ()
	{
		remainMoveCount -= 1;
		//Debug.Log ("remainMoveCount: " + remainMoveCount.ToString ());
		
		targetBlock = targetBlockObject.GetComponent<Block> ();
		
		if (targetBlock.bonusExpPoint > 0 && isForward) {
			playerCharacter.Experience += targetBlock.bonusExpPoint;
		}

		if (targetBlock.blockState == DataCenter.BlockState.Goal) { // 골 블럭이면 종료;
			sceneController.showLabel.text = "Finished";
			return;
		}
		
		if (targetBlock.blockState == DataCenter.BlockState.Keeper && targetBlock.monsterCard.healthPoint > 0) {
			remainMoveCount = 0;
		}
		
		if (targetBlock.blockState == DataCenter.BlockState.Start) {
			remainMoveCount = 0;
		}

		if (targetBlock.blockState == DataCenter.BlockState.Battle) { // 현재 방문한 블럭이 전투블럭이고
			if (targetBlock.visitedPlayers.Count > 0) { // 현재 방문한 블럭에 이미 방문한 사용자가 있다면;
				remainMoveCount = 0;
			}
		}
        
		currentNum = nextNum;
		
		if (remainMoveCount <= 0) {
			remainMoveCount = 0;
			MoveCompleted ();
		} else {
			MovePlayer ();
		}
	}
	
	/// <summary>
	/// 다음 플레이어로 턴 변경;
	/// </summary>
	/// <returns></returns>
	public void ChangePlayerTurn ()
	{
		playerCharacter.Experience += targetBlock.expPoint;
		DataCenter.playerTurnNo += 1;
		if (DataCenter.playerTurnNo >= DataCenter.playerCount) {
			DataCenter.playerTurnNo = 0;
		}
	}
	
	/// <summary>
	/// 실제 플레이어를 이동;
	/// </summary>
	/// <param name="is_quick">시간지체 여부;</param>
	/// <returns></returns>
	public void TweenMoveTo (bool is_quick)
	{		
		sceneController.ActiveThreeCamera(gameObject.transform);
		
		RemoveBlockPosition ();		

		float moving_time = 0.5f;
		if (is_quick) {
			moving_time = 0.0f;
		}

		gotEvent = false;

		SetTargetBlockObject ();
		
		iTween.MoveTo (gameObject, iTween.Hash ("position", 
			new Vector3 (targetBlockObject.transform.position.x, 
				transform.position.y, 
				targetBlockObject.transform.position.z), "time", moving_time,
			"oncomplete", "MovedBlock"));
	}
	
	public void JustMoveTo() {
		sceneController.ActiveThreeCamera(gameObject.transform);
		RemoveBlockPosition ();		

		float moving_time = 0.0f;
		

		gotEvent = false;

		SetTargetBlockObject ();
		
		iTween.MoveTo (gameObject, iTween.Hash ("position", 
			new Vector3 (targetBlockObject.transform.position.x, 
				transform.position.y, 
				targetBlockObject.transform.position.z), "time", moving_time));
	}
	
	/// <summary>
	/// 이동할 블럭 오브젝트 선정;
	/// </summary>
	/// <returns></returns>
	public void SetTargetBlockObject ()
	{
		string str_num = string.Format ("{0:000}", nextNum);
		targetBlockObject = GameObject.Find (DataCenter.hexPrefix + str_num);
	}
	
	/// <summary>
	/// 전투시에 쓰이는 주사위 초기화;
	/// </summary>
	/// <param name="battle_kind">전투의 종류</param>
	/// <returns></returns>
	public void InitBattleDiceTotal (int battle_kind)
	{
		switch (battle_kind) {
			case 0:
				InitDefenceDiceTotal ();
				break;
			case 1:
				InitAttackDiceTotal ();
				break;
			default:
				break;
		}
	}
	
	/// <summary>
	/// 공격 주사위들의 총갯수;
	/// </summary>
	/// <returns></returns>
	public void InitAttackDiceTotal ()
	{
		battleDiceTotalCount = 0;
		for (int i = 0; i < 3; i++) {
			battleDiceTotalCount += cards [i].attackDiceCount;
		}
		
		if (battleDiceTotalCount > 10) {
			battleDiceTotalCount = 10;
		}
	}
	
	/// <summary>
	/// 방어 주사위들의 총갯수;
	/// </summary>
	/// <returns></returns>
	public void InitDefenceDiceTotal ()
	{
		battleDiceTotalCount = 0;
		for (int i = 0; i < 3; i++) {
			battleDiceTotalCount += cards [i].defenceDiceCount;
		}
		
		if (battleDiceTotalCount > 10) {
			battleDiceTotalCount = 10;
		}
	}
	
	/// <summary>
	/// 전투시에 쓰이는 생명 포인트 초기화;
	/// </summary>
	/// <returns></returns>
	public void InitHealthTotal ()
	{
		healthTotalCount = 0;
		for (int i = 0; i < 3; i++) {
			healthTotalCount += cards [i].healthPoint;
		}
	}
	
	/// <summary>
	/// 카드를 생성;
	/// </summary>
	/// <returns></returns>
	public void InitCards ()
	{
		for (int i = 0; i < 3; i++) {
			cards [i] = new GameCard ();
			cards [i].GeneratePoint ();
		}
	}
	
	/// <summary>
	/// Updates the name of the player.
	/// </summary>
	public void UpdatePlayerName ()
	{
		playerName = gameObject.name;
	}
	
	/// <summary>
	/// Removes the block position.
	/// </summary>
	public void RemoveBlockPosition ()
	{
		if (targetBlock != null && targetBlock.visitedPlayers.Count > 0) {
			if (targetBlock.visitedPlayers.Contains (playerIndex)) {
				targetBlock.visitedPlayers.Remove (playerIndex);
			}
		}
	}
}
