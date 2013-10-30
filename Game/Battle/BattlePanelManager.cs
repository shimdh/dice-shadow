using UnityEngine;


/// <summary>
/// 전투시 나오는 패널 관리;
/// </summary>
public class BattlePanelManager : MonoBehaviour {
	public GameObject BattlePanel;
	public BattleDices[] BattleDices;
	public ButtonBattleRollManager RollManager;
	
	
	/// <summary>
    /// 전체 주사위 오브젝트들을 비활성화;
	/// </summary>
	/// <returns></returns>
	public void DisableAllBattleDices()
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
	public void ActiveValidBattleDices(int[] diceCount) {
		for (var i = 0; i < BattleDices.Length; i++) {			
			BattleDices[i].ActiveValidDices(diceCount[i]);
			BattleDices[i].ResetValidDices();
		}
	}
	
    
	/// <summary>
    /// 배틀 패널 활성화;
	/// </summary>
	/// <returns></returns>
	public void ActiveBattlePanel() {
		BattlePanel.SetActive(true);
		DisableAllBattleDices();
	}
}
