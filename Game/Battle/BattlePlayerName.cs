using UnityEngine;

/// <summary>
///     배틀 패널의 각 플레이어 이름 관리;
/// </summary>
public class BattlePlayerName : MonoBehaviour
{
    public string PlayerName;
    private UILabel _playerNameLabel;


    private void Awake()
    {
        _playerNameLabel = gameObject.GetComponent<UILabel>();
    }


    /// <summary>
    ///     플레이어의 이름을 라벨에 적용;
    /// </summary>
    /// <param name="playerName">플레이어 이름;</param>
    /// <returns></returns>
    public void UpdatePlayerName(string playerName)
    {
        if (_playerNameLabel == null)
        {
            _playerNameLabel = gameObject.GetComponent<UILabel>();
        }
        PlayerName = playerName;
        _playerNameLabel.text = PlayerName;
    }
}