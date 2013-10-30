using System;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// 배틀 주사위 관리;
/// </summary>
public class BattleDice : MonoBehaviour {
	public int DiceNumber; // 주사위에 표시될 갯수;
	private UILabel _diceLabel; // 화면에 갯수를 표시할 라벨;

    void Awake()
    {
        _diceLabel = gameObject.GetComponent<UILabel>();
    }
	
	
	/// <summary>
    /// 주사위 라벨에 갯수를 표시;
	/// </summary>
	/// <param name="diceNumber"></param>
	/// <returns></returns>
	public void UpdateDiceLabel(int diceNumber) {
		DiceNumber = diceNumber;
		_diceLabel.text = Convert.ToString(DiceNumber);
	}

	
	
	/// <summary>
    /// 주사위 수를 생성;
	/// </summary>
	/// <returns></returns>
	public void GenerateDiceNumber() {
		DiceNumber = Random.Range(1, 6);
        _diceLabel.text = Convert.ToString(DiceNumber);
	}
}
