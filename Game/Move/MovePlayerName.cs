using UnityEngine;
using System.Collections;

/// <summary>
/// Move player name.
/// </summary>
public class MovePlayerName : MonoBehaviour {
    private UILabel playerNameLabel;

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
	/// Updates the name label.
	/// </summary>
	/// <param name='player_name'>
	/// Player_name.
	/// </param>
    public void UpdateNameLabel(string player_name)
    {   
        playerNameLabel.text = player_name;
    }
}
