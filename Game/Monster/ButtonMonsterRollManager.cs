using UnityEngine;
using System.Collections;

/// <summary>
/// Button monster roll manager.
/// </summary>
public class ButtonMonsterRollManager : MonoBehaviour
{	
	private GameSceneController sceneController;

	void Awake ()
	{
        
	}

	// Use this for initialization
	void Start ()
	{
		sceneController = GameSceneController.Instance;
	}

	// Update is called once per frame
	void Update ()
	{

	}


	/// <summary>
	/// 롤 버튼 클릭시;
	/// </summary>
	/// <returns></returns>
	public void OnClick ()
	{
		MonsterRoll ();

		StartCoroutine ("ApplyMonsterResult");
        
	}
	
	/// <summary>
	/// Applies the monster result.
	/// </summary>
	/// <returns>
	/// The monster result.
	/// </returns>
	public IEnumerator ApplyMonsterResult ()
	{
		yield return new WaitForSeconds(2.0f);

		if (DataCenter.battleDiceRule == DataCenter.BattleDiceRule.High) {
			if (sceneController.monsterPanelManager.battleDices [0].diceTotalNumber
            > sceneController.monsterPanelManager.battleDices [1].diceTotalNumber) {
				GamePlayer fail_player = sceneController.gamePlayersManager.players [DataCenter.playerTurnNo];
				int fail_player_no = fail_player.targetBlock.visitedPlayers [0];
                
				int move_count = sceneController.monsterPanelManager.battleDices [1].diceTotalNumber
                    - sceneController.monsterPanelManager.battleDices [0].diceTotalNumber;
				
				StartCoroutine ("DisableMonsterPanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//				ApplyFailPlayer (fail_player_no, move_count);
                

			} else if (sceneController.monsterPanelManager.battleDices [0].diceTotalNumber
            < sceneController.monsterPanelManager.battleDices [1].diceTotalNumber) {
				GamePlayer fail_player = sceneController.gamePlayersManager.players [DataCenter.playerTurnNo];
                
				int win_player_no = fail_player.targetBlock.visitedPlayers [0];
                
				StartCoroutine ("DisableMonsterPanel");
				ApplyWinPlayer (win_player_no);

			} else {

			}
		} else {
			if (sceneController.monsterPanelManager.battleDices [0].diceTotalNumber
            < sceneController.monsterPanelManager.battleDices [1].diceTotalNumber) {
				GamePlayer fail_player = sceneController.gamePlayersManager.players [DataCenter.playerTurnNo];
				int fail_player_no = fail_player.targetBlock.visitedPlayers [0];
                
				int move_count = sceneController.monsterPanelManager.battleDices [1].diceTotalNumber
                    - sceneController.monsterPanelManager.battleDices [0].diceTotalNumber;

				StartCoroutine ("DisableMonsterPanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//				ApplyFailPlayer (fail_player_no, move_count);
			} else if (sceneController.monsterPanelManager.battleDices [0].diceTotalNumber
            > sceneController.monsterPanelManager.battleDices [1].diceTotalNumber) {
				GamePlayer fail_player = sceneController.gamePlayersManager.players [DataCenter.playerTurnNo];
                
				int win_player_no = fail_player.targetBlock.visitedPlayers [0];

				StartCoroutine ("DisableMonsterPanel");
				ApplyWinPlayer (win_player_no);

			} else {

			}
		}
	}


	/// <summary>
	/// 각 플레이어 주사위 수를 생성;
	/// </summary>
	/// <returns></returns>
	public void MonsterRoll ()
	{
		Debug.Log ("BattleRoll");
		for (int i = 0; i < 2; i++) {
			sceneController.monsterPanelManager.battleDices [i].RollDices ();
		}
	}


	/// <summary>
	/// 진 플레이어를 주사위 차만큼 뒤로 이동;
	/// </summary>
	/// <param name="fail_player_no"></param>
	/// <param name="move_count"></param>
	/// <returns></returns>
	public void ApplyFailPlayer (int fail_player_no, int move_count)
	{
		// TODO: 졌을시에 초기로 돌아가기로 변경;
		sceneController.showLabel.text = "Lost";
		sceneController.gamePlayersManager.players [fail_player_no].healthTotalCount -= 1;
		if (sceneController.gamePlayersManager.players [fail_player_no].healthTotalCount <= 0) {
			sceneController.showLabel.text = "Finished";
		}		
		sceneController.ApplyMoveDiceFormBattle (move_count, fail_player_no);
	}
	
	public void ApplyFailToMoveStart(int fail_player_no, int current_no) {
		sceneController.showLabel.text = "Lost";
		sceneController.gamePlayersManager.players [fail_player_no].healthTotalCount -= 1;
		if (sceneController.gamePlayersManager.players [fail_player_no].healthTotalCount <= 0) {
			sceneController.showLabel.text = "Finished";
		}	
		
		sceneController.ApplyMoveToStartFromBattle(current_no, fail_player_no);		
	}
	
	/// <summary>
	/// Applies the window player.
	/// </summary>
	/// <param name='win_player_no'>
	/// Win_player_no.
	/// </param>
	public void ApplyWinPlayer (int win_player_no)
	{
		sceneController.showLabel.text = "Win";
		GamePlayer win_player = sceneController.gamePlayersManager.players [win_player_no];
		Debug.Log ("Monster.ApplyWinPlayer");
		win_player.targetBlock.monsterCard.healthPoint -= 1;
		if (win_player.targetBlock.blockState == DataCenter.BlockState.Keeper) {
			if (win_player.targetBlock.monsterCard.healthPoint == 0) {
				if (win_player.targetBlock.keeperObject.activeSelf) {
					win_player.targetBlock.keeperObject.SetActive (false);
				}
			}			
		}
		win_player.playerCharacter.Experience += win_player.targetBlock.monsterCard.experiencePoint;
		
		win_player.MoveCompleted ();
		
//        if (!win_player.gotEvent)
//        {
//			switch (win_player.targetBlock.blockState) {
//			default:
//				win_player.MoveCompleted();
//			break;
//			}            
//        }
	}
	
	/// <summary>
	/// Disables the monster panel.
	/// </summary>
	/// <returns>
	/// The monster panel.
	/// </returns>
	public IEnumerator DisableMonsterPanel ()
	{
		yield return new WaitForSeconds(0.2f);
		sceneController.DisableMonsterPanel ();
	}
}
