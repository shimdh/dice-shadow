using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 전투시 나오는 패널 관리;
/// </summary>
public class BattlePanelManager : MonoBehaviour {
	public GameObject battlePanel;
	public BattleDices[] battleDices;
	public ButtonBattleRollManager rollManager;
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 전체 주사위 오브젝트들을 비활성화;
	/// </summary>
	/// <returns></returns>
	public void DisableAllBattleDices() {
		for (int i = 0; i < battleDices.Length; i++) {
			battleDices[i].DisableAllDices();
		}
	}
	
	
	/// <summary>
    /// 유효한 주사위 오브젝트들만 활성화;
	/// </summary>
	/// <param name="dice_count">각 주사위 갯수;</param>
	/// <returns></returns>
	public void ActiveValidBattleDices(int[] dice_count) {
		for (int i = 0; i < battleDices.Length; i++) {			
			battleDices[i].ActiveValidDices(dice_count[i]);
			battleDices[i].ResetValidDices();
		}
	}
	
    
	/// <summary>
    /// 배틀 패널 활성화;
	/// </summary>
	/// <returns></returns>
	public void ActiveBattlePanel() {
		battlePanel.SetActive(true);
		DisableAllBattleDices();
	}
}
