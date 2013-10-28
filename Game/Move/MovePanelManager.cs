using UnityEngine;
using System.Collections;


/// <summary>
/// 이동 패널 관리;
/// </summary>
public class MovePanelManager : MonoBehaviour {
	public GameObject moveDicePanel; // 이동시에 나오는 주사위 패널 오브젝트;	
    public MoveDices moveDices;
    public MoveDiceTotal moveDiceTotal;
    public MovePlayerName movePlayerName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 이동 패널의 활성/바횔성화;
    /// 플레이어의 이름을 표시;
	/// </summary>
	/// <param name="is_actived">이동 패널의 활성화 여부;</param>
	/// <param name="player_name">패널에 표시될 플레이어 이름;</param>
	/// <returns></returns>
	public void ActiveMovePanel(bool is_actived, string player_name) {
		moveDicePanel.SetActive(is_actived);
        movePlayerName.UpdateNameLabel(player_name);
        moveDiceTotal.ApplyTotalNumber(0);
        moveDices.ResetDices();        
	}
}
