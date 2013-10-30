using UnityEngine;
using System.Collections;

/// <summary>
/// Button monster roll manager.
/// </summary>
public class ButtonMonsterRollManager : MonoBehaviour
{
    private GameSceneController _sceneController;

    // Use this for initialization
    void Start()
    {
        _sceneController = GameSceneController.Instance;
    }


    /// <summary>
    /// 롤 버튼 클릭시;
    /// </summary>
    /// <returns></returns>
    public void OnClick()
    {
        MonsterRoll();

        StartCoroutine("ApplyMonsterResult");

    }

    /// <summary>
    /// Applies the monster result.
    /// </summary>
    /// <returns>
    /// The monster result.
    /// </returns>
    public IEnumerator ApplyMonsterResult()
    {
        yield return new WaitForSeconds(2.0f);

        if (DataCenter.BattleRule == DataCenter.BattleDiceRule.High)
        {
            if (_sceneController.MonsterManager.BattleDices[0].DiceTotalNumber
            > _sceneController.MonsterManager.BattleDices[1].DiceTotalNumber)
            {
                var failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                //				int move_count = sceneController.MonsterManager.BattleDiceObjects [1].diceTotalNumber
                //                    - sceneController.MonsterManager.BattleDiceObjects [0].diceTotalNumber;

                StartCoroutine("DisableMonsterPanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
                //				ApplyFailPlayer (fail_player_no, move_count);


            }
            else if (_sceneController.MonsterManager.BattleDices[0].DiceTotalNumber
          < _sceneController.MonsterManager.BattleDices[1].DiceTotalNumber)
            {
                var failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];

                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                StartCoroutine("DisableMonsterPanel");
                ApplyWinPlayer(winPlayerNo);

            }
            else
            {
                if (DataCenter.PlayerTurnNo == 1)
                {
                    OnClick();
                }
            }
        }
        else
        {
            if (_sceneController.MonsterManager.BattleDices[0].DiceTotalNumber
            < _sceneController.MonsterManager.BattleDices[1].DiceTotalNumber)
            {
                var failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                //				int move_count = sceneController.MonsterManager.BattleDiceObjects [1].diceTotalNumber
                //                    - sceneController.MonsterManager.BattleDiceObjects [0].diceTotalNumber;

                StartCoroutine("DisableMonsterPanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
                //				ApplyFailPlayer (fail_player_no, move_count);
            }
            else if (_sceneController.MonsterManager.BattleDices[0].DiceTotalNumber
          > _sceneController.MonsterManager.BattleDices[1].DiceTotalNumber)
            {
                var failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];

                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                StartCoroutine("DisableMonsterPanel");
                ApplyWinPlayer(winPlayerNo);

            }
            else
            {
                if (DataCenter.PlayerTurnNo == 1)
                {
                    OnClick();
                }
            }
        }
    }


    /// <summary>
    /// 각 플레이어 주사위 수를 생성;
    /// </summary>
    /// <returns></returns>
    public void MonsterRoll()
    {
        Debug.Log("BattleRoll");
        for (var i = 0; i < 2; i++)
        {
            _sceneController.MonsterManager.BattleDices[i].RollDices();
        }
    }


    /// <summary>
    /// 진 플레이어를 주사위 차만큼 뒤로 이동;
    /// </summary>
    /// <param name="failPlayerNo"></param>
    /// <param name="moveCount"></param>
    /// <returns></returns>
    public void ApplyFailPlayer(int failPlayerNo, int moveCount)
    {
        _sceneController.ShowLabel.text = "Lost";
        _sceneController.PlayersManager.Players[failPlayerNo].HealthTotalCount -= 1;
        if (_sceneController.PlayersManager.Players[failPlayerNo].HealthTotalCount <= 0)
        {
            _sceneController.ShowLabel.text = "Finished";
        }
        _sceneController.ApplyMoveDiceFormBattle(moveCount, failPlayerNo);
    }

    public void ApplyFailToMoveStart(int failPlayerNo, int currentNo)
    {
        _sceneController.ShowLabel.text = "Lost";
        _sceneController.PlayersManager.Players[failPlayerNo].HealthTotalCount -= 1;
        if (_sceneController.PlayersManager.Players[failPlayerNo].HealthTotalCount <= 0)
        {
            _sceneController.ShowLabel.text = "Game Over";
            _sceneController.ActionStateImage(_sceneController.GameOverStateImage);
            _sceneController.RestartGame();
        }
        else
        {
            _sceneController.ApplyMoveToStartFromBattle(currentNo, failPlayerNo);
        }
    }

    /// <summary>
    /// Applies the window player.
    /// </summary>
    /// <param name='winPlayerNo'>
    /// Win_player_no.
    /// </param>
    public void ApplyWinPlayer(int winPlayerNo)
    {
        _sceneController.ShowLabel.text = "Win";
        var winPlayer = _sceneController.PlayersManager.Players[winPlayerNo];
        Debug.Log("Monster.ApplyWinPlayer");

        _sceneController.ActionStateImage(_sceneController.VictoryStateImage);

        winPlayer.TargetBlock.MonsterCard.HealthPoint -= 1;
        if (winPlayer.TargetBlock.BlockState == DataCenter.BlockState.Keeper)
        {
            if (winPlayer.TargetBlock.MonsterCard.HealthPoint == 0)
            {
                if (winPlayer.TargetBlock.KeeperObject.activeSelf)
                {
                    winPlayer.TargetBlock.KeeperObject.SetActive(false);
                }
            }
        }
        winPlayer.PlayerCharacter.Experience += winPlayer.TargetBlock.MonsterCard.ExperiencePoint;

        winPlayer.MoveCompleted();

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
    public IEnumerator DisableMonsterPanel()
    {
        yield return new WaitForSeconds(0.2f);
        _sceneController.DisableMonsterPanel();
    }
}
