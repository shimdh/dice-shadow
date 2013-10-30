using System;
using UnityEngine;


/// <summary>
/// 이동시에 반영할 주사위에 표시된 총 갯수 관리;
/// </summary>
public class MoveDiceTotal : MonoBehaviour {
	public int Number; // 주사위들에서 나온 총 갯수;
    public UILabel TotalLabel; // 주사위들에서 나온 총 갯수를 표시할 라벨;

    void Awake()
    {
        TotalLabel = gameObject.GetComponent<UILabel>();
    }

	
	/// <summary>
    /// 주사위들에 나온 갯수를 합하여 라벨에 표시;
	/// </summary>
	/// <param name="totalNumber"></param>
	/// <returns></returns>
	public void ApplyTotalNumber(int totalNumber) {
		Number = totalNumber;
        TotalLabel.text = Convert.ToString(Number);
	}
}
