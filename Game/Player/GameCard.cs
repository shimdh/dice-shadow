using UnityEngine;
using System.Collections;


/// <summary>
/// 플레이어가 소지한 카드 관리;
/// </summary>
[System.Serializable]
public class GameCard {
	public int attackDiceCount; // 공격 주사위 갯수;
	public int defenceDiceCount; // 수비 주사위 갯수;
	public int diceCount; // 사용되는 주사위 갯수;
	public int healthPoint; // 소유하고 있는 생명 포인트;
	
	/// <summary>
    /// 클래스 초기화;
    /// 현재는 랜덤이나 속성을 설정하게 변경해야 함;
	/// </summary>
	/// <returns></returns>
	public GameCard ()
	{
        //this.attackDiceCount = Random.Range(1, 3); // 공격 주사위 갯수 생성;
        //this.defenceDiceCount = Random.Range(1, 3); // 방어 주사위 갯수 생성;
        //this.healthPoint = Random.Range(1, 3); // 생명 포인트 생성;
    }

    /// <summary>
    /// 포인트들을 생성;
    /// </summary>
    /// <returns></returns>
    public void GeneratePoint()
    {
        this.attackDiceCount = Random.Range(1, 3); // 공격 주사위 갯수 생성;
        this.defenceDiceCount = Random.Range(1, 3); // 방어 주사위 갯수 생성;
        this.healthPoint = Random.Range(1, 3); // 생명 포인트 생성;
    }
}
