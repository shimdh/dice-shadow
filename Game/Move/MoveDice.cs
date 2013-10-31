using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     이동시에 쓰이는 각 주사위 관리;
/// </summary>
public class MoveDice : MonoBehaviour
{
    public int Number; // 주사위의 나온 갯수;
    private UILabel _diceLabel; // 주사위 갯수를 표시할 라벨;

    private void Awake()
    {
        _diceLabel = gameObject.GetComponent<UILabel>();
    }


    /// <summary>
    ///     랜덤으로 주사위에 표시할 수 생성;
    /// </summary>
    /// <returns></returns>
    public void GetDiceNumber()
    {
        Number = Random.Range(1, 7);
        _diceLabel.text = Convert.ToString(Number);
    }


    /// <summary>
    ///     주사위에 표시할 수를 0으로 초기화;
    /// </summary>
    /// <returns></returns>
    public void ResetDiceNumber()
    {
        Number = 0;
        _diceLabel.text = Convert.ToString(Number);
    }

    /// <summary>
    ///     Deactives the dice.
    /// </summary>
    public void DeactiveDice()
    {
        gameObject.SetActive(false);
    }
}