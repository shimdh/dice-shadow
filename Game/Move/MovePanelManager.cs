using UnityEngine;

/// <summary>
/// 이동 패널 관리;
/// </summary>
public class MovePanelManager : MonoBehaviour {
	public GameObject MoveDicePanel; // 이동시에 나오는 주사위 패널 오브젝트;	
    public MoveDicesManager MoveDicesManager;
    public MoveDiceTotal DiceTotal;
    public MovePlayerName MovePlayerName;
	public ButtonMoveRollManager RollManager;

	/// <summary>
    /// 이동 패널의 활성/바횔성화;
    /// 플레이어의 이름을 표시;
	/// </summary>
	/// <param name="isActived">이동 패널의 활성화 여부;</param>
	/// <param name="playerName">패널에 표시될 플레이어 이름;</param>
	/// <returns></returns>
	public void ActiveMovePanel(bool isActived, string playerName) {
		MoveDicePanel.SetActive(isActived);
        MovePlayerName.UpdateNameLabel(playerName);
        DiceTotal.ApplyTotalNumber(0);
        MoveDicesManager.ResetDices();        
	}
}
