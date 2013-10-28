using UnityEngine;
using System.Collections;


/// <summary>
/// 배틀 패널에서의 생명 포인트 관리;
/// </summary>
public class BattlePlayerHealth : MonoBehaviour {
    public int healthCount;
    private UILabel healthCountLabel;

    void Awake()
    {
        healthCountLabel = gameObject.GetComponent<UILabel>();
    }

	// Use this for initialization
	void Start () {
//        UpdateHealthPoint(0);
	}
	
	// Update is called once per frame
	void Update () {

    }

    
    /// <summary>
    /// 정해진 생명 포인트를 라벨에 적용;
    /// </summary>
    /// <param name="hp_count">생명 포인트;</param>
    /// <returns></returns>
    public void UpdateHealthPoint(int hp_count)
    {
        if (healthCountLabel == null)
        {
            healthCountLabel = gameObject.GetComponent<UILabel>();
        }
        healthCount = hp_count;
		
		Debug.Log("healthCount: " + healthCount.ToString());

        healthCountLabel.text = healthCount.ToString();
    }
}
