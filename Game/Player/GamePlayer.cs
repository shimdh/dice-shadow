using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     게임중인 플레이어 관리;
/// </summary>
public class GamePlayer : MonoBehaviour
{
    public int BattleDiceTotalCount; // 총 주사위 갯수;
    public int CardTotal = 3; // 갖고 있는 카드 갯수;

    public GameCard[] Cards = new GameCard[3]; // 소지한 각 카드정보;
    public int CurrentNum; // 현재 블럭 번호;
    public List<int> DiceNumbers = new List<int>(); // 나온 각 주사위 갯수;

    public bool GotEvent = false; // 블럭에서의 이벤트 수행여부;
    public int HealthTotalCount; // HP 토탈 포인트;
    public bool IsForward = true; // 이동방향;
    public int NextNum; // 다음에 이동할 블럭 번호;

    public GamePlayerCharacter PlayerCharacter;
    public int PlayerIndex; // 플레이어의 인덱스;
    public string PlayerName; // 플레이어 이름;
    public int RemainMoveCount; // 남은 이동할 블럭수;
    public Block TargetBlock; // 이동할 블럭;
    public GameObject TargetBlockObject; // 이동할 블럭 오브젝트;
    private GameSceneController _sceneController; // 게임 씬 컨트롤러;


    // Use this for initialization
    private void Start()
    {
        _sceneController = GameSceneController.Instance;
        CurrentNum = 0;

        InitCards();
        InitHealthTotal();
    }


    /// <summary>
    ///     이동을 완료했을 때 호출;
    /// </summary>
    /// <returns></returns>
    public void MoveCompleted()
    {
        int targetPlayerNo;

        RemoveBlockPosition();
        TargetBlock.VisitedPlayers.Add(PlayerIndex);

        // 현재 머문 블럭에 이미 다른 플레이어가 있을시;
        if (TargetBlock.VisitedPlayers.Count == 2)
        {
            if (_sceneController.MoveManager.MoveDicePanel.activeSelf)
            {
                _sceneController.DisableMovePanel();
            }

            _sceneController.ActionStateImage(_sceneController.BattleStateImage);

            DataCenter.State = DataCenter.GameState.Battled;
            Debug.Log("EnableBattlePanel");
            _sceneController.EnableBattlePanel(TargetBlock);
            return;
        }

        DataCenter.State = DataCenter.GameState.Started;

        switch (TargetBlock.BlockState)
        {
                // 블럭의 종류가 워프일경우;
            case DataCenter.BlockState.Warp1:
            case DataCenter.BlockState.Warp2:
            case DataCenter.BlockState.Warp3:
                NextNum = TargetBlock.WarpTargetNo;

                TweenMoveTo(true);
                break;
            case DataCenter.BlockState.RandomBox:
                if (!GotEvent)
                {
                    HealthTotalCount += 1;
                    GotEvent = true;
                    ChangePlayerTurn();
                }
                else
                {
                    ChangePlayerTurn();
                }
                break;
            case DataCenter.BlockState.Battle: // 블럭의 종류가 배틀일경우;
                ChangePlayerTurn();
                break;
            case DataCenter.BlockState.Monster: // 블럭의 속성이 몬스터일경우;
                Debug.Log("DataCenter.BlockState.Monster");

                if (TargetBlock.MonsterCard.HealthPoint > 0)
                {
                    GotEvent = true;
                    if (_sceneController.MoveManager.MoveDicePanel.activeSelf)
                    {
                        _sceneController.DisableMovePanel();
                    }
                    _sceneController.ActionStateImage(_sceneController.BattleStateImage);

                    DataCenter.State = DataCenter.GameState.Monster;
                    Debug.Log("EnableMonsterPanel");
                    _sceneController.EnableMonsterPanel(TargetBlock);

                    return;
                }
                ChangePlayerTurn();

                break;

            case DataCenter.BlockState.Keeper: // 블럭의 속성이 키퍼일 경우;
                if (TargetBlock.MonsterCard.HealthPoint > 0)
                {
                    GotEvent = true;
                    if (_sceneController.MoveManager.MoveDicePanel.activeSelf)
                    {
                        _sceneController.DisableMovePanel();
                    }

                    _sceneController.ActionStateImage(_sceneController.BattleStateImage);

                    DataCenter.State = DataCenter.GameState.Monster;
                    Debug.Log("EnableMonsterPanel");
                    _sceneController.EnableMonsterPanel(TargetBlock);

                    return;
                }
                ChangePlayerTurn();

                break;
            case DataCenter.BlockState.Gamble:
                if (!GotEvent)
                {
                    GotEvent = true;
                    if (_sceneController.MoveManager.MoveDicePanel.activeSelf)
                    {
                        _sceneController.DisableMovePanel();
                    }
                    DataCenter.State = DataCenter.GameState.Gamble;

                    _sceneController.EnableGamblePanel(TargetBlock);

                    return;
                }
                ChangePlayerTurn();
                break;

            case DataCenter.BlockState.Ladder: // 블럭의 속성이 사다리 일 경우;
                if (PlayerCharacter.Level >= TargetBlock.LevelLadder)
                {
                    if (!GotEvent)
                    {
                        GotEvent = true;
                        if (_sceneController.MoveManager.MoveDicePanel.activeSelf)
                        {
                            _sceneController.DisableMovePanel();
                        }
                        _sceneController.ActionStateImage(_sceneController.LadderStateImage);

                        DataCenter.State = DataCenter.GameState.Ladder;
                        _sceneController.EnableMonsterPanel(TargetBlock);
                        return;
                    }
                    NextNum = TargetBlock.GoNumberLadder;
                    TweenMoveTo(true);
                    return;
                }
                ChangePlayerTurn();
                break;
            case DataCenter.BlockState.RuleLow:
                if (!GotEvent)
                {
                    GotEvent = true;
                    DataCenter.BattleRule = DataCenter.BattleDiceRule.Low;
                    DataCenter.BattleRuleRemainTurn = 3;

                    ChangePlayerTurn();
                }
                else
                {
                    ChangePlayerTurn();
                }
                break;
            case DataCenter.BlockState.ComeHere:
                if (!GotEvent)
                {
                    GotEvent = true;
                    targetPlayerNo = PlayerIndex + 1;

                    if (targetPlayerNo >= DataCenter.PlayerCount)
                    {
                        targetPlayerNo = 0;
                    }

                    _sceneController.ActionStateImage(_sceneController.ComeStateImage);

                    _sceneController.PlayersManager.Players[targetPlayerNo].RemoveBlockPosition();
                    _sceneController.PlayersManager.Players[targetPlayerNo].NextNum = CurrentNum;
                    _sceneController.PlayersManager.Players[targetPlayerNo].TweenMoveTo(true);
                    return;
                }
                ChangePlayerTurn();
                break;

            case DataCenter.BlockState.ImGoing:
                if (!GotEvent)
                {
                    GotEvent = true;
                    targetPlayerNo = PlayerIndex + 1;
                    //			target_player_no = DataCenter.playerTurnNo + 1;
                    if (targetPlayerNo >= DataCenter.PlayerCount)
                    {
                        targetPlayerNo = 0;
                    }
                    //			ChangePlayerTurn();
                    _sceneController.ActionStateImage(_sceneController.GoStateImage);

                    NextNum = _sceneController.PlayersManager.Players[targetPlayerNo].CurrentNum;
                    TweenMoveTo(true);
                    return;
                }
                ChangePlayerTurn();
                break;

            default: // 나머지 블럭일 경우;
                ChangePlayerTurn();
                break;
        }

        DataCenter.BattleRuleRemainTurn -= 1;
        if (DataCenter.BattleRuleRemainTurn <= 0)
        {
            DataCenter.BattleRuleRemainTurn = 0;
            DataCenter.BattleRule = DataCenter.BattleDiceRule.High;
        }

        if (DataCenter.GameState.Started == DataCenter.State)
        {
            Debug.Log("EnableMovePanel");
            _sceneController.EnableMovePanel();
        }

        IsForward = true;
    }

    /// <summary>
    ///     플레이어를 이동;
    /// </summary>
    /// <returns></returns>
    public void MovePlayer()
    {
        //		RemoveBlockPosition ();

        if (IsForward)
        {
            if (TargetBlock != null)
            {
                NextNum = TargetBlock.NextFowardBlockNum != -1 ? TargetBlock.NextFowardBlockNum : CurrentNum + 1;
            }
            else
            {
                NextNum = CurrentNum + 1;
            }
        }
        else
        {
            if (TargetBlock != null)
            {
                NextNum = TargetBlock.NextReverseBlockNum != -1 ? TargetBlock.NextReverseBlockNum : CurrentNum - 1;
            }
            else
            {
                NextNum = CurrentNum - 1;
            }
        }

        TweenMoveTo(false);
    }

    /// <summary>
    ///     각블럭 이동시 호출;
    /// </summary>
    public void MovedBlock()
    {
        RemainMoveCount -= 1;
        //Debug.Log ("remainMoveCount: " + remainMoveCount.ToString ());

        TargetBlock = TargetBlockObject.GetComponent<Block>();

        if (TargetBlock.BonusExpPoint > 0 && IsForward)
        {
            PlayerCharacter.Experience += TargetBlock.BonusExpPoint;
        }

        switch (TargetBlock.BlockState)
        {
            case DataCenter.BlockState.Goal:
                _sceneController.ShowLabel.text = "Finished";
                return;

            case DataCenter.BlockState.Keeper:
                if (TargetBlock.MonsterCard.HealthPoint > 0)
                {
                    RemainMoveCount = 0;
                }
                break;
            case DataCenter.BlockState.Start:
                RemainMoveCount = 0;
                break;

            case DataCenter.BlockState.Battle:
                if (TargetBlock.VisitedPlayers.Count > 0)
                {
                    // 현재 방문한 블럭에 이미 방문한 사용자가 있다면;
                    RemainMoveCount = 0;
                }
                break;
        }


        CurrentNum = NextNum;

        if (RemainMoveCount <= 0)
        {
            RemainMoveCount = 0;
            MoveCompleted();
        }
        else
        {
            MovePlayer();
        }
    }

    /// <summary>
    ///     다음 플레이어로 턴 변경;
    /// </summary>
    /// <returns></returns>
    public void ChangePlayerTurn()
    {
        PlayerCharacter.Experience += TargetBlock.ExpPoint;
        DataCenter.PlayerTurnNo += 1;
        if (DataCenter.PlayerTurnNo >= DataCenter.PlayerCount)
        {
            DataCenter.PlayerTurnNo = 0;
        }
    }

    /// <summary>
    ///     실제 플레이어를 이동;
    /// </summary>
    /// <param name="isQuick">시간지체 여부;</param>
    /// <returns></returns>
    public void TweenMoveTo(bool isQuick)
    {
        _sceneController.ActiveThreeCamera(gameObject.transform);

        RemoveBlockPosition();

        var movingTime = 0.5f;
        if (isQuick)
        {
            movingTime = 0.0f;
        }

        GotEvent = false;

        SetTargetBlockObject();

        iTween.MoveTo(gameObject, iTween.Hash("position",
            new Vector3(TargetBlockObject.transform.position.x,
                transform.position.y,
                TargetBlockObject.transform.position.z), "time", movingTime,
            "oncomplete", "MovedBlock"));
    }

    public void JustMoveTo()
    {
        _sceneController.ActiveThreeCamera(gameObject.transform);
        RemoveBlockPosition();

        const float movingTime = 0.0f;


        GotEvent = false;

        SetTargetBlockObject();

        iTween.MoveTo(gameObject, iTween.Hash("position",
            new Vector3(TargetBlockObject.transform.position.x,
                transform.position.y,
                TargetBlockObject.transform.position.z), "time", movingTime));
    }

    /// <summary>
    ///     이동할 블럭 오브젝트 선정;
    /// </summary>
    /// <returns></returns>
    public void SetTargetBlockObject()
    {
        var strNum = string.Format("{0:000}", NextNum);
        TargetBlockObject = GameObject.Find(DataCenter.HexPrefix + strNum);
    }

    /// <summary>
    ///     전투시에 쓰이는 주사위 초기화;
    /// </summary>
    /// <param name="battleKind">전투의 종류</param>
    /// <returns></returns>
    public void InitBattleDiceTotal(int battleKind)
    {
        switch (battleKind)
        {
            case 0:
                InitDefenceDiceTotal();
                break;
            case 1:
                InitAttackDiceTotal();
                break;
        }
    }

    /// <summary>
    ///     공격 주사위들의 총갯수;
    /// </summary>
    /// <returns></returns>
    public void InitAttackDiceTotal()
    {
        BattleDiceTotalCount = 0;
        for (int i = 0; i < 3; i++)
        {
            BattleDiceTotalCount += Cards[i].AttackDiceCount;
        }

        if (BattleDiceTotalCount > 10)
        {
            BattleDiceTotalCount = 10;
        }
    }

    /// <summary>
    ///     방어 주사위들의 총갯수;
    /// </summary>
    /// <returns></returns>
    public void InitDefenceDiceTotal()
    {
        BattleDiceTotalCount = 0;
        for (var i = 0; i < 3; i++)
        {
            BattleDiceTotalCount += Cards[i].DefenceDiceCount;
        }

        if (BattleDiceTotalCount > 10)
        {
            BattleDiceTotalCount = 10;
        }
    }

    /// <summary>
    ///     전투시에 쓰이는 생명 포인트 초기화;
    /// </summary>
    /// <returns></returns>
    public void InitHealthTotal()
    {
        HealthTotalCount = 0;
        for (int i = 0; i < 3; i++)
        {
            HealthTotalCount += Cards[i].HealthPoint;
        }
    }

    /// <summary>
    ///     카드를 생성;
    /// </summary>
    /// <returns></returns>
    public void InitCards()
    {
        for (int i = 0; i < 3; i++)
        {
            Cards[i] = new GameCard();
            Cards[i].GeneratePoint();
        }
    }

    /// <summary>
    ///     Updates the name of the player.
    /// </summary>
    public void UpdatePlayerName()
    {
        PlayerName = gameObject.name;
    }

    /// <summary>
    ///     Removes the block position.
    /// </summary>
    public void RemoveBlockPosition()
    {
        if (TargetBlock == null || TargetBlock.VisitedPlayers.Count <= 0) return;
        if (TargetBlock.VisitedPlayers.Contains(PlayerIndex))
        {
            TargetBlock.VisitedPlayers.Remove(PlayerIndex);
        }
    }
}