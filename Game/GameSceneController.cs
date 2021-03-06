﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Game scene controller.
/// </summary>
public class GameSceneController : MonoBehaviour
{
    private static GameSceneController _instance;

    public CameraMove CamMove; // 카메라 이동관련 클래스;
    public Vector3 CamPivotLocation;
    public Transform CamPivotTr;
    public UILabel ShowLabel; // 상태를 보여주기 위한 라벨;
    public float ShowStateTime = 0.5f;
    public GameObject ThreeCamera; // 3D 카메라;
    public GameObject TwoCamera; // 2D 카메라;
    public GameObject UiCamera; // UI 카메라;

    #region State Image Objects

    public GameObject BattleStateImage;
    public GameObject ComeStateImage;
    public GameObject GameOverStateImage;
    public GameObject GoStateImage;
    public GameObject LadderStateImage;
    public GameObject VictoryStateImage;

    #endregion

    public static GameSceneController Instance
    {
        get
        {
            if (_instance != null) return _instance;
            Debug.LogWarning("No singleton exist! Creating new one.");
            var owner = new GameObject(DataCenter.GameSceneObjectName);
            _instance = owner.AddComponent<GameSceneController>();
            return _instance;
        }
    }

    #region Manger Objects

    public BattlePanelManager BattleManager; // 배틀 패널 매니저 클래스;
    public GamblePanelManager GambleManager; // 겜블 패널 매니저 클래스;
    public MonsterPanelManager MonsterManager; // 몬스터 패널 매니저 클래스;
    public MovePanelManager MoveManager; // 이동 패널 매니저 클래스;
    public GamePlayersManager PlayersManager; // 플레이어 관리 클래스;

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning("A singleton already exist! Destroying new one.");
            Destroy(this);
        }
    }

    private void Start()
    {
        MoveManager = GetComponent<MovePanelManager>();
        BattleManager = GetComponent<BattlePanelManager>();
        MonsterManager = GetComponent<MonsterPanelManager>();
        GambleManager = GetComponent<GamblePanelManager>();
        PlayersManager = GetComponent<GamePlayersManager>();

        if (CamPivotTr)
        {
            CamPivotLocation = CamPivotTr.position;
        }

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Player"), true);

        EnableMovePanel();
    }


    /// <summary>
    ///     이동 주사위 패널을 활성화;
    /// </summary>
    /// <returns></returns>
    public void EnableMovePanel()
    {
        int playerNo = DataCenter.PlayerTurnNo;

        ActiveTwoCamera();

//		camMove.Follow(false);
//		camMove.gameObject.transform.position = camPivotLocation;

        if (BattleManager.BattlePanel.activeSelf)
        {
            DisableBattlePanel();
        }

        if (MonsterManager.MonsterPanel.activeSelf)
        {
            DisableMonsterPanel();
        }

        if (GambleManager.GamblePanel.activeSelf)
        {
            DisableGamblePanel();
        }

        PlayersManager.Players[playerNo].UpdatePlayerName();

        Debug.Log("playerName: " + PlayersManager.Players[playerNo].PlayerName);
        Debug.Log("player_no: " + playerNo);

        MoveManager.ActiveMovePanel(true, PlayersManager.Players[playerNo].PlayerName);

        if (DataCenter.PlayerTurnNo == 1)
        {
            MoveManager.RollManager.OnClick();
        }
    }

    /// <summary>
    ///     이동 주사위 패널을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableMovePanel()
    {
        MoveManager.ActiveMovePanel(false, "");
    }


    /// <summary>
    ///     이동 주사위 패널에 나온 주사위수의 총합을 해당 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">이동할 주사위의 갯수;</param>
    /// <returns></returns>
    public void ApplyMoveDiceNumber(int num)
    {
        int playerNo = DataCenter.PlayerTurnNo;

        DisableMovePanel();

        PlayersManager.Players[playerNo].RemainMoveCount = num;
        PlayersManager.Players[playerNo].MovePlayer();
    }


    /// <summary>
    ///     수만큼 해당 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">이동할 주사위의 수;</param>
    /// <param name="playerNo">이동할 플레이어의 인덱스;</param>
    /// <returns></returns>
    public void ApplyMoveDiceNumber(int num, int playerNo)
    {
        if (num < 0)
        {
            PlayersManager.Players[playerNo].IsForward = false;
            num = -num;
        }

        PlayersManager.Players[playerNo].RemainMoveCount = num;
        PlayersManager.Players[playerNo].MovePlayer();
    }


    /// <summary>
    ///     배틀에서 진 플레이어에 적용하여 이동;
    /// </summary>
    /// <param name="num">전투에서의 주사위 갯수의 차;</param>
    /// <param name="playerNo">전투에서 진 플레이어 인덱스;이동할 플레이어;</param>
    /// <returns></returns>
    public void ApplyMoveDiceFormBattle(int num, int playerNo)
    {
        if (num < 0)
        {
            num = -num;
        }
        PlayersManager.Players[playerNo].IsForward = true;

        DisableBattlePanel();

        PlayersManager.Players[playerNo].RemainMoveCount = num;
        PlayersManager.Players[playerNo].MovePlayer();
    }


    /// <summary>
    /// 전투에서 진 후 시작지점으로 이동;
    /// </summary>
    /// <param name="currentNum">현재 블럭번호;</param>
    /// <param name="playerNo">플레이어의 인덱스;</param>
    public void ApplyMoveToStartFromBattle(int currentNum, int playerNo)
    {
        int startNo;

        if (currentNum < 100)
        {
            startNo = 0;
        }
        else if (currentNum < 200)
        {
            startNo = 0;
        }
        else if (currentNum < 300)
        {
            startNo = 100;
        }
        else
        {
            startNo = 200;
        }

        DisableBattlePanel();

        PlayersManager.Players[playerNo].RemainMoveCount = 0;
        PlayersManager.Players[playerNo].NextNum = startNo;
        PlayersManager.Players[playerNo].TweenMoveTo(true);
    }


    /// <summary>
    ///     전투 패널을 활성화;
    /// </summary>
    /// <param name="targetBlock">전투가 벌어진 블럭;</param>
    /// <returns></returns>
    public void EnableBattlePanel(Block targetBlock)
    {
        ActiveTwoCamera();

        BattleManager.ActiveBattlePanel();

        int i = 0;
        var diceCount = new List<int>();
        foreach (int playerNumber in targetBlock.VisitedPlayers)
        {
            GamePlayer gamePlayer = PlayersManager.Players[playerNumber];
            gamePlayer.InitBattleDiceTotal(i);

            BattleManager.BattleDices[i].BattlePlayerName.UpdatePlayerName(gamePlayer.PlayerName);
            diceCount.Add(gamePlayer.BattleDiceTotalCount + gamePlayer.PlayerCharacter.Level - 1);

            BattleManager.BattleDices[i].BattlePlayerHealth.UpdateHealthPoint(gamePlayer.HealthTotalCount);

            i += 1;
        }

        BattleManager.ActiveValidBattleDices(diceCount.ToArray());
    }


    /// <summary>
    ///     전투 패널 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableBattlePanel()
    {
        BattleManager.BattlePanel.SetActive(false);
    }
	
	
    /// <summary>
    ///     몬스터 패널 활성화;
    /// </summary>
    /// <param name='targetBlock'>
    ///     도착한 블럭;
    /// </param>
    public void EnableMonsterPanel(Block targetBlock)
    {
        Debug.Log("In EnableMonsterPanel method");

        ActiveTwoCamera();

        MonsterManager.ActiveMonsterPanel();

        var diceCount = new List<int>();

        int playerNumber = targetBlock.VisitedPlayers[0];

        GamePlayer gamePlayer = PlayersManager.Players[playerNumber];
        gamePlayer.InitBattleDiceTotal(1);

        targetBlock.MonsterCard.InitDiceCount();

        Debug.Log("monsterName: " + targetBlock.MonsterCard.MonsterName);

        MonsterManager.BattleDices[0].BattlePlayerName.UpdatePlayerName(targetBlock.MonsterCard.MonsterName);
        MonsterManager.BattleDices[1].BattlePlayerName.UpdatePlayerName(gamePlayer.PlayerName);

        diceCount.Add(targetBlock.MonsterCard.DefenceDiceCount);
        diceCount.Add(gamePlayer.BattleDiceTotalCount + gamePlayer.PlayerCharacter.Level - 1);

        Debug.Log("monsterCard.healthPoint: " + targetBlock.MonsterCard.HealthPoint);

        MonsterManager.BattleDices[0].BattlePlayerHealth.UpdateHealthPoint(targetBlock.MonsterCard.HealthPoint);
        MonsterManager.BattleDices[1].BattlePlayerHealth.UpdateHealthPoint(gamePlayer.HealthTotalCount);

        MonsterManager.ActiveValidMonsterDices(diceCount.ToArray());

        if (DataCenter.PlayerTurnNo == 1)
        {
            MonsterManager.RollManager.OnClick();
        }
    }

	
    /// <summary>
    ///     몬스터 패널 비활성화;
    /// </summary>
    public void DisableMonsterPanel()
    {
        MonsterManager.MonsterPanel.SetActive(false);
    }


    /// <summary>
    ///     겜블패널 활성화;
    /// </summary>
    /// <param name='targetBlock'>
    ///     Target_block.
    /// </param>
    public void EnableGamblePanel(Block targetBlock)
    {
        Debug.Log("In EnableGamblePanel method");

        ActiveTwoCamera();

        GambleManager.ActiveGamblePanel();

        var diceCount = new List<int>();

        int playerNumber = targetBlock.VisitedPlayers[0];
        GamePlayer gamePlayer = PlayersManager.Players[playerNumber];

        gamePlayer.InitBattleDiceTotal(1);

        targetBlock.MonsterCard.InitDiceCount();


        GambleManager.BattleDices[0].BattlePlayerName.UpdatePlayerName("System");
        GambleManager.BattleDices[1].BattlePlayerName.UpdatePlayerName(gamePlayer.PlayerName);

        diceCount.Add(gamePlayer.BattleDiceTotalCount + gamePlayer.PlayerCharacter.Level - 1);
        diceCount.Add(gamePlayer.BattleDiceTotalCount + gamePlayer.PlayerCharacter.Level - 1);

        GambleManager.BattleDices[0].BattlePlayerHealth.UpdateHealthPoint(1);
        GambleManager.BattleDices[1].BattlePlayerHealth.UpdateHealthPoint(gamePlayer.HealthTotalCount);

        GambleManager.ActiveValidGambleDices(diceCount.ToArray());
    }

    /// <summary>
    ///     겜블패널 비활성화;
    /// </summary>
    public void DisableGamblePanel()
    {
        GambleManager.GamblePanel.SetActive(false);
    }

    /// <summary>
    /// 2D 카메라 활성화;
    /// </summary>
    public void ActiveTwoCamera()
    {
        if (ThreeCamera.activeSelf)
        {
            ThreeCamera.SetActive(false);
        }

        if (!TwoCamera.activeSelf)
        {
            TwoCamera.SetActive(true);
        }

        if (UiCamera.activeSelf)
        {
            UiCamera.SetActive(false);
        }

        if (!UiCamera.activeSelf)
        {
            UiCamera.SetActive(true);
        }
    }

    /// <summary>
    /// 3D 카메라 활성화;
    /// </summary>
    /// <param name="objectTr"></param>
    public void ActiveThreeCamera(Transform objectTr)
    {
        if (TwoCamera.activeSelf)
        {
            TwoCamera.SetActive(false);
        }

        if (!ThreeCamera.activeSelf)
        {
            ThreeCamera.SetActive(true);
        }

        CamMove.Follow(objectTr);
    }


    /// <summary>
    /// 게임 재시작;
    /// </summary>
    public void RestartGame()
    {
        if (BattleManager.BattlePanel.activeSelf)
        {
            BattleManager.BattlePanel.SetActive(false);
        }

        if (MonsterManager.MonsterPanel.activeSelf)
        {
            MonsterManager.MonsterPanel.SetActive(false);
        }

        if (GambleManager.GamblePanel.activeSelf)
        {
            GambleManager.GamblePanel.SetActive(false);
        }

        if (MoveManager.MoveDicePanel.activeSelf)
        {
            MoveManager.MoveDicePanel.SetActive(true);
        }

        for (int i = 0; i < DataCenter.PlayerCount; i++)
        {
            PlayersManager.Players[i].RemainMoveCount = 0;
            PlayersManager.Players[i].NextNum = 0;
            PlayersManager.Players[i].JustMoveTo();
        }

        DataCenter.PlayerTurnNo = 0;

        EnableMovePanel();
    }

	
	/// <summary>
	/// 정해진 이미지 오브젝트를 잠시 활성화하고 비활성화하는 코루틴을 호출;
	/// </summary>
	/// <param name='stateImage'>
	/// 보여줄 이미지 오브젝트;
	/// </param>
    public void ActionStateImage(GameObject stateImage)
    {
        StartCoroutine(ShowStateImage(stateImage));
    }
	
	
	/// <summary>
	/// 정해진 이미지 오브젝트를 잠시 활성화하고 비활성화한다;
	/// </summary>
	/// <returns>
	/// The state image.
	/// </returns>
	/// <param name='stateImage'>
	/// 보여줄 이미지 오브젝트;
	/// </param>
    public IEnumerator ShowStateImage(GameObject stateImage)
    {
        if (!stateImage.activeSelf)
        {
            stateImage.SetActive(true);
        }

        yield return new WaitForSeconds(ShowStateTime);

        if (stateImage.activeSelf)
        {
            stateImage.SetActive(false);
        }
    }
	
	
    /// <summary>
    ///     게임 종료시에 호출;
    /// </summary>
    public void OnApplicationQuit()
    {
        _instance = null;
    }
}