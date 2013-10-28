using UnityEngine;
using System.Collections;


/// <summary>
/// 배틀 패널의 각 플레이어 이름 관리;
/// </summary>
public class BattlePlayerName : MonoBehaviour {
	private UILabel playerNameLabel;
    public string playerName;


    void Awake()
    {
        playerNameLabel = gameObject.GetComponent<UILabel>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 플레이어의 이름을 라벨에 적용;
	/// </summary>
	/// <param name="player_name">플레이어 이름;</param>
	/// <returns></returns>
	public void UpdatePlayerName(string player_name) {
        if (playerNameLabel == null)
        {
            playerNameLabel = gameObject.GetComponent<UILabel>();
        }
        playerName = player_name;
        playerNameLabel.text = playerName;
        Debug.Log("playerName: " + playerName);
	}
}
