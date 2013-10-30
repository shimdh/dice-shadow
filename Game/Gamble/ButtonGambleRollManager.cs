using UnityEngine;
using System.Collections;

/// <summary>
/// Button gemble roll manager.
/// </summary>
public class ButtonGambleRollManager : MonoBehaviour
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
        GambleRoll();

        StartCoroutine("ApplyGambleResult");

    }

    /// <summary>
    /// Applies the monster result.
    /// </summary>
    /// <returns>
    /// The monster result.
    /// </returns>
    public IEnumerator ApplyGambleResult()
    {
        yield return new WaitForSeconds(2.0f);
        var diceTotalNumber0 = _sceneController.GambleManager.BattleDices[0].DiceTotalNumber;
        var diceTotalNumber1 = _sceneController.GambleManager.BattleDices[1].DiceTotalNumber;
        var failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];

        if (DataCenter.BattleRule == DataCenter.BattleDiceRule.High)
        {
            if (diceTotalNumber0 > diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                //                int move_count = dice_total_number_1 - dice_total_number_0;

                StartCoroutine("DisableGamblePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
                //                ApplyFailPlayer(fail_player_no, move_count);

            }
            else if (diceTotalNumber0 < diceTotalNumber1)
            {
                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                StartCoroutine("DisableGamblePanel");
                ApplyWinPlayer(winPlayerNo);
            }
        }
        else
        {
            if (diceTotalNumber0 < diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                //                int move_count = dice_total_number_1 - dice_total_number_0;

                StartCoroutine("DisableGamblePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
                //				ApplyFailPlayer(fail_player_no, move_count);
            }
            else if (diceTotalNumber0 > diceTotalNumber1)
            {
                int winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];

                StartCoroutine("DisableGamblePanel");
                ApplyWinPlayer(winPlayerNo);

            }
        }
    }


    /// <summary>
    /// 각 플레이어 주사위 수를 생성;
    /// </summary>
    /// <returns></returns>
    public void GambleRoll()
    {
        Debug.Log("GambleRoll");
        for (var i = 0; i < 2; i++)
        {
            _sceneController.GambleManager.BattleDices[i].RollDices();
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
        Debug.Log("Gamble.ApplyWinPlayer");
        winPlayer.TargetBlock.MonsterCard.HealthPoint -= 1;

        winPlayer.MoveCompleted();
    }

    /// <summary>
    /// Disables the monster panel.
    /// </summary>
    /// <returns>
    /// The monster panel.
    /// </returns>
    public IEnumerator DisableGamblePanel()
    {
        yield return new WaitForSeconds(0.2f);
        _sceneController.DisableGamblePanel();
    }
}
