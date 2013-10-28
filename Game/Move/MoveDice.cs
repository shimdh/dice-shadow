using UnityEngine;
using System.Collections;


/// <summary>
/// 이동시에 쓰이는 각 주사위 관리;
/// </summary>
public class MoveDice : MonoBehaviour {
	public int number; // 주사위의 나온 갯수;
	private UILabel diceLabel; // 주사위 갯수를 표시할 라벨;

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
    /// 랜덤으로 주사위에 표시할 수 생성;
	/// </summary>
	/// <returns></returns>
	public void GetDiceNumber() {		
		number = Random.Range(1, 7);
		diceLabel.text = number.ToString();
	}
	
	
	/// <summary>
    /// 주사위에 표시할 수를 0으로 초기화;
	/// </summary>
	/// <returns></returns>
	public void ResetDiceNumber() {
		number = 0;
		diceLabel.text = number.ToString();
    }

	/// <summary>
	/// Deactives the dice.
	/// </summary>
    public void DeactiveDice()
    {
        gameObject.SetActive(false);
    }
}
