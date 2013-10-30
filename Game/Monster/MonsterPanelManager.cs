using UnityEngine;

/// <summary>
/// Monster panel manager.
/// </summary>
public class MonsterPanelManager : MonoBehaviour
{
    public GameObject MonsterPanel;
    public BattleDices[] BattleDices;
    public ButtonMonsterRollManager RollManager;

    
    /// <summary>
    /// 전체 주사위 오브젝트들을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableAllMonsterDices()
    {
        foreach (var t in BattleDices)
        {
            t.DisableAllDices();
        }
    }


    /// <summary>
    /// 유효한 주사위 오브젝트들만 활성화;
    /// </summary>
    /// <param name="diceCount">각 주사위 갯수;</param>
    /// <returns></returns>
    public void ActiveValidMonsterDices(int[] diceCount)
    {
        for (var i = 0; i < BattleDices.Length; i++)
        {
            BattleDices[i].ActiveValidDices(diceCount[i]);
            BattleDices[i].ResetValidDices();
        }
    }


    /// <summary>
    /// 배틀 패널 활성화;
    /// </summary>
    /// <returns></returns>
    public void ActiveMonsterPanel()
    {
        Debug.Log("ActiveMonsterPanel");
        MonsterPanel.SetActive(true);
        DisableAllMonsterDices();
    }
}
