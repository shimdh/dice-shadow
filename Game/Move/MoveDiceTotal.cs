using UnityEngine;
using System.Collections;


/// <summary>
/// 이동시에 반영할 주사위에 표시된 총 갯수 관리;
/// </summary>
public class MoveDiceTotal : MonoBehaviour {
	public int number; // 주사위들에서 나온 총 갯수;
    public UILabel totalLabel; // 주사위들에서 나온 총 갯수를 표시할 라벨;

    void Awake()
    {
        totalLabel = gameObject.GetComponent<UILabel>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/// <summary>
    /// 주사위들에 나온 갯수를 합하여 라벨에 표시;
	/// </summary>
	/// <param name="total_number"></param>
	/// <returns></returns>
	public void ApplyTotalNumber(int total_number) {
		number = total_number;
        totalLabel.text = number.ToString();
	}
}
