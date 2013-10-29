using UnityEngine;
using System.Collections;

/// <summary>
/// Button gemble roll manager.
/// </summary>
public class ButtonGambleRollManager : MonoBehaviour {

	private GameSceneController sceneController;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
		sceneController = GameSceneController.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 롤 버튼 클릭시;
    /// </summary>
    /// <returns></returns>
    public void OnClick()
    {
        GambleRoll();

        StartCoroutine("ApplyGambleResult");
        
    }
	
	/// <summary>
	/// Applies the monster result.
	/// </summary>
	/// <returns>
	/// The monster result.
	/// </returns>
    public IEnumerator ApplyGambleResult()
    {
        yield return new WaitForSeconds(2.0f);
		int dice_total_number_0 = sceneController.gamblePanelManager.battleDices[0].diceTotalNumber;
		int dice_total_number_1 = sceneController.gamblePanelManager.battleDices[1].diceTotalNumber;
		GamePlayer fail_player = sceneController.gamePlayersManager.players[DataCenter.playerTurnNo];

        if (DataCenter.battleDiceRule == DataCenter.BattleDiceRule.High)
        {
            if (dice_total_number_0 > dice_total_number_1)
            {
                int fail_player_no = fail_player.targetBlock.visitedPlayers[0];
                
//                int move_count = dice_total_number_1 - dice_total_number_0;
				
				StartCoroutine("DisableGamblePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//                ApplyFailPlayer(fail_player_no, move_count);

            }
            else if (dice_total_number_0 < dice_total_number_1)
            {   
                int win_player_no = fail_player.targetBlock.visitedPlayers[0];
                
				StartCoroutine("DisableGamblePanel");
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
                int fail_player_no = fail_player.targetBlock.visitedPlayers[0];
                
//                int move_count = dice_total_number_1 - dice_total_number_0;

                StartCoroutine("DisableGamblePanel");
				ApplyFailToMoveStart(fail_player_no, fail_player.currentNum);
//				ApplyFailPlayer(fail_player_no, move_count);
            }
            else if (dice_total_number_0 > dice_total_number_1)
            {
                int win_player_no = fail_player.targetBlock.visitedPlayers[0];

                StartCoroutine("DisableGamblePanel");
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
    public void GambleRoll()
    {
        Debug.Log("GambleRoll");
        for (int i = 0; i < 2; i++)
        {
            sceneController.gamblePanelManager.battleDices[i].RollDices();
        }
    }


    /// <summary>
    /// 진 플레이어를 주사위 차만큼 뒤로 이동;
    /// </summary>
    /// <param name="fail_player_no"></param>
    /// <param name="move_count"></param>
    /// <returns></returns>
    public void ApplyFailPlayer(int fail_player_no, int move_count)
    {
        sceneController.showLabel.text = "Lost";
        sceneController.gamePlayersManager.players[fail_player_no].healthTotalCount -= 1;
        if (sceneController.gamePlayersManager.players[fail_player_no].healthTotalCount <= 0 )
		{
			sceneController.showLabel.text = "Finished";
		}
        sceneController.ApplyMoveDiceFormBattle(move_count, fail_player_no);
    }
	
	public void ApplyFailToMoveStart(int fail_player_no, int current_no) {
		sceneController.showLabel.text = "Lost";
        sceneController.gamePlayersManager.players[fail_player_no].healthTotalCount -= 1;
        if (sceneController.gamePlayersManager.players[fail_player_no].healthTotalCount <= 0 )
		{
			sceneController.showLabel.text = "Game Over";
			sceneController.ActionStateImage(sceneController.gameOverStateImage);			
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
        sceneController.showLabel.text = "Win";
		GamePlayer win_player = sceneController.gamePlayersManager.players[win_player_no];
		Debug.Log("Gamble.ApplyWinPlayer");
		win_player.targetBlock.monsterCard.healthPoint -= 1;
		
		win_player.MoveCompleted();
    }
	
	/// <summary>
	/// Disables the monster panel.
	/// </summary>
	/// <returns>
	/// The monster panel.
	/// </returns>
	public IEnumerator DisableGamblePanel()
	{
		yield return new WaitForSeconds(0.2f);
		sceneController.DisableGamblePanel();
	}
}
