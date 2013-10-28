using UnityEngine;
using System.Collections;


/// <summary>
/// 이동시에 주사위를 돌리는 버튼 관리;
/// </summary>
public class ButtonMoveRollManager : MonoBehaviour {
    private GameSceneController sceneController;
    //private GameSceneManager sceneManager; // 게임씬 매니저;

    void Awake()
    {
//        GetObjects();
    }

	
    // Use this for initialization
	void Start () {
        GetObjects ();
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	
	
	/// <summary>
    /// 이동 패널에서 롤 버튼 클릭시;
	/// </summary>
	/// <returns></returns>
	public void OnClick() {
        sceneController.movePanelManager.moveDices.GenerateDiceNumber();
        int sum = sceneController.movePanelManager.moveDices.totalDiceNumber;

        int player_no = DataCenter.playerTurnNo;
        GamePlayer moving_player = sceneController.gamePlayersManager.players[player_no];
		
		if (moving_player.targetBlock != null) {
			if (moving_player.targetBlock.visitedPlayers.Count > 0) {
                moving_player.targetBlock.visitedPlayers.Remove(player_no);
			}
		}
		
		StartCoroutine("ShowTotalNumber", sum);
	}
	
	/// <summary>
    /// 필요한 오브젝트들 설정;
	/// </summary>
	/// <returns></returns>
	public void GetObjects ()
	{
        //sceneManager = GameObject.Find(DataCenter.gameSceneObjectName).GetComponent<GameSceneManager>();
        sceneController = GameSceneController.Instance;
	}

	
	
	/// <summary>
    /// 주사위에 나온 총 합에 따른 실행;
	/// </summary>
	/// <param name="sum"></param>
	/// <returns></returns>
	IEnumerator DisableRoll(int sum) {
		yield return new WaitForSeconds(.5f);
        sceneController.ApplyMoveDiceNumber(sum);
	}

	
	
	/// <summary>
    /// 주사위들에 표시된 총합을 표시;
	/// </summary>
	/// <param name="sum"></param>
	/// <returns></returns>
	IEnumerator ShowTotalNumber (int sum)
	{
		yield return new WaitForSeconds(.5f);
        sceneController.movePanelManager.moveDiceTotal.ApplyTotalNumber(sum);
		StartCoroutine("DisableRoll", sum);
	}
}
