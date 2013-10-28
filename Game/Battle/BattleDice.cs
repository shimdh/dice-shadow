using UnityEngine;
using System.Collections;


/// <summary>
/// 배틀 주사위 관리;
/// </summary>
public class BattleDice : MonoBehaviour {
	public int diceNumber; // 주사위에 표시될 갯수;
	private UILabel diceLabel; // 화면에 갯수를 표시할 라벨;

    void Awake()
    {
        diceLabel = gameObject.GetComponent<UILabel>();
    }
	
    // Use this for initialization
	void Start () {
        
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	
	
	/// <summary>
    /// 주사위 라벨에 갯수를 표시;
	/// </summary>
	/// <param name="dice_number"></param>
	/// <returns></returns>
	public void UpdateDiceLabel(int dice_number) {
		diceNumber = dice_number;
		diceLabel.text = diceNumber.ToString();
	}

	
	
	/// <summary>
    /// 주사위 수를 생성;
	/// </summary>
	/// <returns></returns>
	public void GenerateDiceNumber() {
		diceNumber = Random.Range(1, 7);
		diceLabel.text = diceNumber.ToString();
	}
}
