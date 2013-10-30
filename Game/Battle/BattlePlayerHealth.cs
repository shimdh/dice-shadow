using System;
using UnityEngine;


/// <summary>
/// 배틀 패널에서의 생명 포인트 관리;
/// </summary>
public class BattlePlayerHealth : MonoBehaviour {
    public int HealthCount;
    private UILabel _healthCountLabel;

    void Awake()
    {
        _healthCountLabel = gameObject.GetComponent<UILabel>();
    }

    
    /// <summary>
    /// 정해진 생명 포인트를 라벨에 적용;
    /// </summary>
    /// <param name="hpCount">생명 포인트;</param>
    /// <returns></returns>
    public void UpdateHealthPoint(int hpCount)
    {
        if (_healthCountLabel == null)
        {
            _healthCountLabel = gameObject.GetComponent<UILabel>();
        }
        HealthCount = hpCount;

        Debug.Log("healthCount: " + Convert.ToString(HealthCount));

        _healthCountLabel.text = Convert.ToString(HealthCount);
    }
}
