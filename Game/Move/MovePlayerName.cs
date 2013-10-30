using UnityEngine;

/// <summary>
/// Move player name.
/// </summary>
public class MovePlayerName : MonoBehaviour {
    private UILabel _playerNameLabel;

    void Awake()
    {
        _playerNameLabel = gameObject.GetComponent<UILabel>();
    }

	
	/// <summary>
	/// Updates the name label.
	/// </summary>
	/// <param name='playerName'>
	/// Player_name.
	/// </param>
    public void UpdateNameLabel(string playerName)
    {   
        _playerNameLabel.text = playerName;
    }
}
