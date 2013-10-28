using UnityEngine;
using System.Collections;


/// <summary>
/// 배틀시 주사위에 나온 갯수의 총합 관리;
/// </summary>
public class BattleDiceTotal : MonoBehaviour {
	public int diceTotalNumber; // 주사위에 나온 갯수의 총합;
    private UILabel diceTotalLabel; // 주사위에 나온 갯수의 총합을 표시할 라벨 오브젝트;

    void Awake()
    {
        diceTotalLabel = gameObject.GetComponent<UILabel>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 해당하는 주사위 수의 총갯수를 라벨에 적용;
	/// </summary>
	/// <param name="dice_number"></param>
	/// <returns></returns>
	public void UpdateDiceTotalLabel(int dice_number) {
        if (diceTotalLabel == null)
        {
            diceTotalLabel = gameObject.GetComponent<UILabel>();
        }
		diceTotalNumber = dice_number;

		diceTotalLabel.text = diceTotalNumber.ToString();
	}
}
