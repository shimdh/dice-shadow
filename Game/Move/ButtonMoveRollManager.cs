using System.Collections;
using UnityEngine;

/// <summary>
///     이동시에 주사위를 돌리는 버튼 관리;
/// </summary>
public class ButtonMoveRollManager : MonoBehaviour
{
    private GameSceneController _sceneController;

    // Use this for initialization
    private void Start()
    {
        GetObjects();
    }


    /// <summary>
    ///     이동 패널에서 롤 버튼 클릭시;
    /// </summary>
    /// <returns></returns>
    public void OnClick()
    {
        _sceneController.MoveManager.MoveDicesManager.GenerateDiceNumber();
        var sum = _sceneController.MoveManager.MoveDicesManager.TotalDiceNumber;

        var playerNo = DataCenter.PlayerTurnNo;
        var movingPlayer = _sceneController.PlayersManager.Players[playerNo];

        if (movingPlayer.TargetBlock != null)
        {
            if (movingPlayer.TargetBlock.VisitedPlayers.Count > 0)
            {
                movingPlayer.TargetBlock.VisitedPlayers.Remove(playerNo);
            }
        }

        StartCoroutine("ShowTotalNumber", sum);
    }

    /// <summary>
    ///     필요한 오브젝트들 설정;
    /// </summary>
    /// <returns></returns>
    public void GetObjects()
    {   
        _sceneController = GameSceneController.Instance;
    }


    /// <summary>
    ///     주사위에 나온 총 합에 따른 실행;
    /// </summary>
    /// <param name="sum"></param>
    /// <returns></returns>
    private IEnumerator DisableRoll(int sum)
    {
        yield return new WaitForSeconds(.5f);
        _sceneController.ApplyMoveDiceNumber(sum);
    }


    /// <summary>
    ///     주사위들에 표시된 총합을 표시;
    /// </summary>
    /// <param name="sum"></param>
    /// <returns></returns>
    private IEnumerator ShowTotalNumber(int sum)
    {
        yield return new WaitForSeconds(.5f);
        _sceneController.MoveManager.DiceTotal.ApplyTotalNumber(sum);
        StartCoroutine("DisableRoll", sum);
    }
}