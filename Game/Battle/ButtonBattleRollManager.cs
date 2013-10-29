using UnityEngine;
using System.Collections;


/// <summary>
/// 배틀시에 주사위를 돌릴 버튼 관리;
/// </summary>
public class ButtonBattleRollManager : MonoBehaviour {
    private GameSceneController sceneController;

    void Awake()
    {
        sceneController = GameSceneController.Instance;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 배틀 버튼 클릭시;
	/// </summary>
	/// <returns></returns>
	public void OnClick() {
		BattleRoll ();

        StartCoroutine("ApplyBattleResult");

        //ApplyBattleResult();
	}
	
	/// <summary>
	/// Applies the battle result.
	/// </summary>
	/// <returns>
	/// The battle result.
	/// </returns>
    public IEnumerator ApplyBattleResult()
    {
        yield return new WaitForSeconds(2.0f);
		int dice_total_number_0 = sceneController.battlePanelManager.battleDices[0].diceTotalNumber;
		int dice_total_number_1 = sceneController.battlePanelManager.battleDices[1].diceTotalNumber;
		GamePlayer fail_player = sceneController.gamePlayersManager.players[DataCenter.playerTurnNo];

        if (DataCenter.battleDiceRule == DataCenter.BattleDiceRule.High)
        {
            if (dice_total_number_0 > dice_total_number_1)
            {
                
                int fail_player_no = fail_player.targetBlock.visitedPlayers[1];
                int win_player_no = fail_player.targetBlock.visitedPlayers[0];
//                int move_count = sceneController.battlePanelManager.battleDices[1].diceTotalNumber
//                    - sceneController.battlePanelManager.battleDices[0].diceTotalNumber;
				
				StartCoroutine("DisableBattlePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(win_player_no);

            }
            else if (dice_total_number_0 < dice_total_number_1)
            {
                int fail_player_no = fail_player.targetBlock.visitedPlayers[0];
                int win_player_no = fail_player.targetBlock.visitedPlayers[1];
//                int move_count = sceneController.battlePanelManager.battleDices[0].diceTotalNumber
//                    - sceneController.battlePanelManager.battleDices[1].diceTotalNumber;
				
				StartCoroutine("DisableBattlePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(win_player_no);

            }
            else
            {

            }
        }
        else
        {
            if (dice_total_number_0 < dice_total_number_1)
            {
                int fail_player_no = fail_player.targetBlock.visitedPlayers[1];
                int win_player_no = fail_player.targetBlock.visitedPlayers[0];
//                int move_count = sceneController.battlePanelManager.battleDices[1].diceTotalNumber
//                    - sceneController.battlePanelManager.battleDices[0].diceTotalNumber;
				
				StartCoroutine("DisableBattlePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(win_player_no);

            }
            else if (dice_total_number_0 > dice_total_number_1)
            {
                int fail_player_no = fail_player.targetBlock.visitedPlayers[0];
                int win_player_no = fail_player.targetBlock.visitedPlayers[1];
//                int move_count = sceneController.battlePanelManager.battleDices[0].diceTotalNumber
//                    - sceneController.battlePanelManager.battleDices[1].diceTotalNumber;

				StartCoroutine("DisableBattlePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//                ApplyFailPlayer(fail_player_no, move_count);
                ApplyWinPlayer(win_player_no);

            }
            else
            {

            }
        }
    }
	
	
	/// <summary>
    /// 각 플레이어 주사위 수를 생성;
	/// </summary>
	/// <returns></returns>
	public void BattleRoll ()
	{
        Debug.Log("BattleRoll");
		for (int i = 0; i < 2; i++) {
            sceneController.battlePanelManager.battleDices[i].RollDices();
		}
	}
	
	
	/// <summary>
    /// 진 플레이어를 주사위 차만큼 뒤로 이동;
	/// </summary>
	/// <param name="fail_player_no"></param>
	/// <param name="move_count"></param>
	/// <returns></returns>
	public void ApplyFailPlayer (int fail_player_no, int move_count)
	{
		GamePlayer fail_player = sceneController.gamePlayersManager.players[fail_player_no];
        fail_player.healthTotalCount -= 1;
        if (fail_player.healthTotalCount <= 0)
		{
			sceneController.showLabel.text = "Finished";
		}
        sceneController.ApplyMoveDiceFormBattle(move_count, fail_player_no);
	}
	
	public void ApplyFailToMoveStart(int fail_player_no, int current_no) {
		GamePlayer fail_player = sceneController.gamePlayersManager.players[fail_player_no];
        fail_player.healthTotalCount -= 1;
        if (fail_player.healthTotalCount <= 0)
		{
			sceneController.showLabel.text = "Game Over";
			StartCoroutine("ShowStateImage", sceneController.gameOverStateImage);
			sceneController.RestartGame();
		}
		else {
			sceneController.ApplyMoveToStartFromBattle(current_no, fail_player_no);  
		}
		      
	}
	
	/// <summary>
	/// Applies the window player.
	/// </summary>
	/// <param name='win_player_no'>
	/// Win_player_no.
	/// </param>
    public void ApplyWinPlayer(int win_player_no)
    {
        
		GamePlayer win_player = sceneController.gamePlayersManager.players[win_player_no];
		Debug.Log("Battle.ApplyWinPlayer");
		
        if (!win_player.gotEvent)
        {
			switch (win_player.targetBlock.blockState) {
			case DataCenter.BlockState.Monster:				
				win_player.MoveCompleted();
				break;
			case DataCenter.BlockState.RandomBox:
				win_player.MoveCompleted();
				break;
			default:
			break;
			}            
        }
    }
	
	/// <summary>
	/// Disables the battle panel.
	/// </summary>
	/// <returns>
	/// The battle panel.
	/// </returns>
	public IEnumerator DisableBattlePanel()
	{
		yield return new WaitForSeconds(0.2f);
		sceneController.DisableBattlePanel();
	}
}
