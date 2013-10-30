using System;
using UnityEngine;


/// <summary>
/// 배틀시 주사위에 나온 갯수의 총합 관리;
/// </summary>
public class BattleDiceTotal : MonoBehaviour {
	public int DiceTotalNumber; // 주사위에 나온 갯수의 총합;
    private UILabel _diceTotalLabel; // 주사위에 나온 갯수의 총합을 표시할 라벨 오브젝트;

    void Awake()
    {
        _diceTotalLabel = gameObject.GetComponent<UILabel>();
    }
	
	/// <summary>
    /// 해당하는 주사위 수의 총갯수를 라벨에 적용;
	/// </summary>
	/// <param name="diceNumber"></param>
	/// <returns></returns>
	public void UpdateDiceTotalLabel(int diceNumber) {
        if (_diceTotalLabel == null)
        {
            _diceTotalLabel = gameObject.GetComponent<UILabel>();
        }
		DiceTotalNumber = diceNumber;

        _diceTotalLabel.text = Convert.ToString(DiceTotalNumber);
	}
}
