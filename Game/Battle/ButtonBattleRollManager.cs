using System.Collections;
using UnityEngine;

/// <summary>
///     배틀시에 주사위를 돌릴 버튼 관리;
/// </summary>
public class ButtonBattleRollManager : MonoBehaviour
{
    private GameSceneController _sceneController;

    private void Awake()
    {
        _sceneController = GameSceneController.Instance;
    }

    /// <summary>
    ///     배틀 버튼 클릭시;
    /// </summary>
    /// <returns></returns>
    public void OnClick()
    {
        BattleRoll();

        StartCoroutine("ApplyBattleResult");

        //ApplyBattleResult();
    }

    /// <summary>
    ///     Applies the battle result.
    /// </summary>
    /// <returns>
    ///     The battle result.
    /// </returns>
    public IEnumerator ApplyBattleResult()
    {
        yield return new WaitForSeconds(2.0f);
        int diceTotalNumber0 = _sceneController.BattleManager.BattleDices[0].DiceTotalNumber;
        int diceTotalNumber1 = _sceneController.BattleManager.BattleDices[1].DiceTotalNumber;
        GamePlayer failPlayer = _sceneController.PlayersManager.Players[DataCenter.PlayerTurnNo];

        if (DataCenter.BattleRule == DataCenter.BattleDiceRule.High)
        {
            if (diceTotalNumber0 > diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[1];
                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];
//                int move_count = sceneController.BattleManager.BattleDiceObjects[1].diceTotalNumber
//                    - sceneController.BattleManager.BattleDiceObjects[0].diceTotalNumber;

                StartCoroutine("DisableBattlePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(winPlayerNo);
            }
            else if (diceTotalNumber0 < diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];
                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[1];
//                int move_count = sceneController.BattleManager.BattleDiceObjects[0].diceTotalNumber
//                    - sceneController.BattleManager.BattleDiceObjects[1].diceTotalNumber;

                StartCoroutine("DisableBattlePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(winPlayerNo);
            }
        }
        else
        {
            if (diceTotalNumber0 < diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[1];
                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];
//                int move_count = sceneController.BattleManager.BattleDiceObjects[1].diceTotalNumber
//                    - sceneController.BattleManager.BattleDiceObjects[0].diceTotalNumber;

                StartCoroutine("DisableBattlePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(winPlayerNo);
            }
            else if (diceTotalNumber0 > diceTotalNumber1)
            {
                var failPlayerNo = failPlayer.TargetBlock.VisitedPlayers[0];
                var winPlayerNo = failPlayer.TargetBlock.VisitedPlayers[1];
//                int move_count = sceneController.BattleManager.BattleDiceObjects[0].diceTotalNumber
//                    - sceneController.BattleManager.BattleDiceObjects[1].diceTotalNumber;

                StartCoroutine("DisableBattlePanel");
                ApplyFailToMoveStart(failPlayerNo, failPlayer.CurrentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(winPlayerNo);
            }
        }
    }


    /// <summary>
    ///     각 플레이어 주사위 수를 생성;
    /// </summary>
    /// <returns></returns>
    public void BattleRoll()
    {
        if (_sceneController == null)
        {
            _sceneController = GameSceneController.Instance;
        }

        Debug.Log("BattleRoll");
        for (var i = 0; i < 2; i++)
        {
            _sceneController.BattleManager.BattleDices[i].RollDices();
        }
    }


    /// <summary>
    ///     진 플레이어를 주사위 차만큼 뒤로 이동;
    /// </summary>
    /// <param name="failPlayerNo"></param>
    /// <param name="moveCount"></param>
    /// <returns></returns>
    public void ApplyFailPlayer(int failPlayerNo, int moveCount)
    {
        var failPlayer = _sceneController.PlayersManager.Players[failPlayerNo];
        failPlayer.HealthTotalCount -= 1;
        if (failPlayer.HealthTotalCount <= 0)
        {
            _sceneController.ShowLabel.text = "Finished";
        }
        _sceneController.ApplyMoveDiceFormBattle(moveCount, failPlayerNo);
    }

    public void ApplyFailToMoveStart(int failPlayerNo, int currentNo)
    {
        var failPlayer = _sceneController.PlayersManager.Players[failPlayerNo];
        failPlayer.HealthTotalCount -= 1;
        if (failPlayer.HealthTotalCount <= 0)
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
    ///     Applies the window player.
    /// </summary>
    /// <param name='winPlayerNo'>
    ///     Win_player_no.
    /// </param>
    public void ApplyWinPlayer(int winPlayerNo)
    {
        var winPlayer = _sceneController.PlayersManager.Players[winPlayerNo];
        Debug.Log("Battle.ApplyWinPlayer");

        if (winPlayer.GotEvent) return;
        switch (winPlayer.TargetBlock.BlockState)
        {
            case DataCenter.BlockState.Monster:
                winPlayer.MoveCompleted();
                break;
            case DataCenter.BlockState.RandomBox:
                winPlayer.MoveCompleted();
                break;
        }
    }

    /// <summary>
    ///     Disables the battle panel.
    /// </summary>
    /// <returns>
    ///     The battle panel.
    /// </returns>
    public IEnumerator DisableBattlePanel()
    {
        yield return new WaitForSeconds(0.2f);
        _sceneController.DisableBattlePanel();
    }
}