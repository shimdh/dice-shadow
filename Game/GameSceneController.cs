using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game scene controller.
/// </summary>
public class GameSceneController : MonoBehaviour {

    private static GameSceneController instance;

    public static GameSceneController Instance {
        get {
            if (instance == null) {
                Debug.LogWarning("No singleton exist! Creating new one.");
                GameObject owner = new GameObject(DataCenter.gameSceneObjectName);
                instance = owner.AddComponent<GameSceneController>();
            }
            return instance;
        }
    }

    #region Manger Objects
    public MovePanelManager movePanelManager;
    public BattlePanelManager battlePanelManager;
    public MonsterPanelManager monsterPanelManager;
    public GamblePanelManager gamblePanelManager;
	public GamePlayersManager gamePlayersManager;
    #endregion
	
	public CameraMove camMove;
    public UILabel showLabel;
	public Transform camPivotTr;
	public Vector3 camPivotLocation;
	public GameObject twoCamera;
	public GameObject threeCamera;
	public GameObject uiCamera;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Debug.LogWarning("A singleton already exist! Destroying new one.");
            Destroy(this);
        }
    }

    private void Start() {
//        gamePlayersManager = gameObject.GetComponent<GamePlayersManager>();
//        movePanelManager = gameObject.GetComponent<MovePanelManager>();
//        battlePanelManager = gameObject.GetComponent<BattlePanelManager>();
//        monsterPanelManager = gameObject.GetComponent<MonsterPanelManager>();
//		gemblePanelManager = gameObject.GetComponent<GemblePanelManager>();		
		
		camPivotLocation = camPivotTr.position;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Player"), true);

        EnableMovePanel();
    
    }

    /// <summary>
    /// 이동 주사위 패널을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableMovePanel() {
        movePanelManager.ActiveMovePanel(false, "");

    }


    /// <summary>
    /// 이동 주사위 패널을 활성화;
    /// </summary>
    /// <returns></returns>
    public void EnableMovePanel() {
        int player_no = DataCenter.playerTurnNo;
		
		ActiveTwoCamera ();
		
//		camMove.Follow(false);
//		camMove.gameObject.transform.position = camPivotLocation;
		
		if (battlePanelManager.battlePanel.activeSelf) {
			DisableBattlePanel();
		}
		
		if (monsterPanelManager.monsterPanel.activeSelf) {
			DisableMonsterPanel();
		}
		
		if (gamblePanelManager.gamblePanel.activeSelf) {
			DisableGamblePanel();
		}
		
        gamePlayersManager.players[player_no].UpdatePlayerName();

        Debug.Log("playerName: " + gamePlayersManager.players[player_no].playerName);
        Debug.Log("player_no: " + player_no);

        movePanelManager.ActiveMovePanel(true, gamePlayersManager.players[player_no].playerName);
    }



    /// <summary>
    /// 이동 주사위 패널에 나온 주사위수의 총합을 해당 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">이동할 주사위의 갯수;</param>
    /// <returns></returns>
    public void ApplyMoveDiceNumber(int num) {
        var player_no = DataCenter.playerTurnNo;

        DisableMovePanel();

        gamePlayersManager.players[player_no].remainMoveCount = num;
        gamePlayersManager.players[player_no].MovePlayer();
    }


    /// <summary>
    /// 수만큼 해당 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">이동할 주사위의 수;</param>
    /// <param name="player_no">이동할 플레이어의 인덱스;</param>
    /// <returns></returns>
    public void ApplyMoveDiceNumber(int num, int player_no) {
        if (num < 0) {
            gamePlayersManager.players[player_no].isForward = false;
        }

        gamePlayersManager.players[player_no].remainMoveCount = num;
        gamePlayersManager.players[player_no].MovePlayer();

    }


    /// <summary>
    /// 배틀에서 진 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">전투에서의 주사위 갯수의 차;</param>
    /// <param name="player_no">전투에서 진 플레이어 인덱스;이동할 플레이어;</param>
    /// <returns></returns>
    public void ApplyMoveDiceFormBattle(int num, int player_no) {

        if (num < 0) {
            num = -num;
        }
        gamePlayersManager.players[player_no].isForward = false;

        DisableBattlePanel();

        gamePlayersManager.players[player_no].remainMoveCount = num;
        gamePlayersManager.players[player_no].MovePlayer();

    }
	
	public void ApplyMoveToStartFromBattle(int current_num, int player_no) {
		int start_no;
		
		if (current_num < 100) {
			start_no = 0;
		} else if (current_num < 200) {
			start_no = 0;
		} else if (current_num < 300) {
			start_no = 100;
		} else {
			start_no = 200;
		}
		
		DisableBattlePanel();
		
		gamePlayersManager.players[player_no].remainMoveCount = 0;
		gamePlayersManager.players[player_no].nextNum = start_no;
		gamePlayersManager.players[player_no].TweenMoveTo(true);
	}



    /// <summary>
    /// 전투 패널을 활성화;
    /// </summary>
    /// <param name="target_block">전투가 벌어진 블럭;</param>
    /// <returns></returns>
    public void EnableBattlePanel(Block target_block) {
		ActiveTwoCamera();
//		camMove.Follow(false);
//		camMove.gameObject.transform.position = camPivotLocation;
		
		battlePanelManager.ActiveBattlePanel();

        int i = 0;
        List<int> dice_count = new List<int>();
        foreach (int player_number in target_block.visitedPlayers) {
			GamePlayer game_player = gamePlayersManager.players[player_number];
			Debug.Log("player_number: " + player_number.ToString());
			Debug.Log("healthTotalCount: " + game_player.healthTotalCount);
			
            game_player.InitBattleDiceTotal(i);

            battlePanelManager.battleDices[i].battlePlayerName.UpdatePlayerName(game_player.playerName);
            dice_count.Add(game_player.battleDiceTotalCount + game_player.playerCharacter.Level - 1);

            battlePanelManager.battleDices[i].battlePlayerHealth.UpdateHealthPoint(game_player.healthTotalCount);

            i += 1;
        }
		
		Debug.Log("dice_count.ToArray()[0]: "+ dice_count.ToArray()[0].ToString());
		Debug.Log("dice_count.ToArray()[1]: "+ dice_count.ToArray()[1].ToString());
        battlePanelManager.ActiveValidBattleDices(dice_count.ToArray());
    }


    /// <summary>
    /// 전투 패널을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableBattlePanel() {
        battlePanelManager.battlePanel.SetActive(false);
    }
	
	/// <summary>
	/// Enables the monster panel.
	/// </summary>
	/// <param name='target_block'>
	/// Target_block.
	/// </param>
    public void EnableMonsterPanel(Block target_block) {
        Debug.Log("In EnableMonsterPanel method");
		
		ActiveTwoCamera();
		
//		camMove.Follow(false);
//		camMove.gameObject.transform.position = camPivotLocation;

        monsterPanelManager.ActiveMonsterPanel();
        
        List<int> dice_count = new List<int>();

        int player_number = target_block.visitedPlayers[0];
		
		GamePlayer game_player = gamePlayersManager.players[player_number];
        game_player.InitBattleDiceTotal(1);

        target_block.monsterCard.InitDiceCount();

        Debug.Log("monsterName: " + target_block.monsterCard.monsterName);

        monsterPanelManager.battleDices[0].battlePlayerName.UpdatePlayerName(target_block.monsterCard.monsterName);
        monsterPanelManager.battleDices[1].battlePlayerName.UpdatePlayerName(game_player.playerName);
		
        dice_count.Add(target_block.monsterCard.defenceDiceCount);
        dice_count.Add(game_player.battleDiceTotalCount + game_player.playerCharacter.Level - 1);

        Debug.Log("monsterCard.healthPoint: " + target_block.monsterCard.healthPoint);

        monsterPanelManager.battleDices[0].battlePlayerHealth.UpdateHealthPoint(target_block.monsterCard.healthPoint);
        monsterPanelManager.battleDices[1].battlePlayerHealth.UpdateHealthPoint(game_player.healthTotalCount);
		
        monsterPanelManager.ActiveValidMonsterDices(dice_count.ToArray());
    }
	
	/// <summary>
	/// Disables the monster panel.
	/// </summary>
    public void DisableMonsterPanel() {
        monsterPanelManager.monsterPanel.SetActive(false);
    }
	
	
	/// <summary>
	/// Enables the gemble panel.
	/// </summary>
	/// <param name='target_block'>
	/// Target_block.
	/// </param>
	public void EnableGamblePanel(Block target_block) {
        Debug.Log("In EnableGamblePanel method");
		
		ActiveTwoCamera();
		
//		camMove.Follow(false);
//		camMove.gameObject.transform.position = camPivotLocation;

        gamblePanelManager.ActiveGamblePanel();
        
        List<int> dice_count = new List<int>();

        int player_number = target_block.visitedPlayers[0];
		GamePlayer game_player = gamePlayersManager.players[player_number];
		
        game_player.InitBattleDiceTotal(1);

        target_block.monsterCard.InitDiceCount();

        

        gamblePanelManager.battleDices[0].battlePlayerName.UpdatePlayerName("System");
        gamblePanelManager.battleDices[1].battlePlayerName.UpdatePlayerName(game_player.playerName);
		
        dice_count.Add(game_player.battleDiceTotalCount + game_player.playerCharacter.Level - 1);
        dice_count.Add(game_player.battleDiceTotalCount + game_player.playerCharacter.Level - 1);

        gamblePanelManager.battleDices[0].battlePlayerHealth.UpdateHealthPoint(1);
        gamblePanelManager.battleDices[1].battlePlayerHealth.UpdateHealthPoint(game_player.healthTotalCount);
		
		Debug.Log("dice_count.ToArray()[0]: "+ dice_count.ToArray()[0].ToString());
		Debug.Log("dice_count.ToArray()[1]: "+ dice_count.ToArray()[1].ToString());
        gamblePanelManager.ActiveValidGambleDices(dice_count.ToArray());
    }
	
	/// <summary>
	/// Disables the gemble panel.
	/// </summary>
	public void DisableGamblePanel() {
        gamblePanelManager.gamblePanel.SetActive(false);
    }
	
	/// <summary>
	/// Raises the application quit event.
	/// </summary>
    public void OnApplicationQuit() {
        instance = null;
    }

	public void ActiveTwoCamera ()
	{
		if (threeCamera.activeSelf) {
    		threeCamera.SetActive(false);
    	}
    	
    	if (!twoCamera.activeSelf) {
    		twoCamera.SetActive(true);
    	}
		
		if (uiCamera.activeSelf) {
			uiCamera.SetActive(false);			
		}
		
		if (!uiCamera.activeSelf) {
			uiCamera.SetActive(true);
		}
	}
	
	public void ActiveThreeCamera(Transform object_tr) {
		if (twoCamera.activeSelf) {
			twoCamera.SetActive(false);
		}
		
		if (!threeCamera.activeSelf) {
			threeCamera.SetActive(true);
		}
		
		camMove.Follow(object_tr);
	}
}
