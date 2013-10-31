using UnityEngine;

/// <summary>
///     Gemble panel manager.
/// </summary>
public class GamblePanelManager : MonoBehaviour
{
    public BattleDices[] BattleDices;
    public GameObject GamblePanel;


    /// <summary>
    ///     전체 주사위 오브젝트들을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableAllGambleDices()
    {
        foreach (var t in BattleDices)
        {
            t.DisableAllDices();
        }
    }


    /// <summary>
    ///     유효한 주사위 오브젝트들만 활성화;
    /// </summary>
    /// <param name="diceCount">각 주사위 갯수;</param>
    /// <returns></returns>
    public void ActiveValidGambleDices(int[] diceCount)
    {
        for (var i = 0; i < BattleDices.Length; i++)
        {
            BattleDices[i].ActiveValidDices(diceCount[i]);
            BattleDices[i].ResetValidDices();
        }
    }


    /// <summary>
    ///     배틀 패널 활성화;
    /// </summary>
    /// <returns></returns>
    public void ActiveGamblePanel()
    {
        Debug.Log("ActiveGemblePanel");
        GamblePanel.SetActive(true);
        DisableAllGambleDices();
    }
}