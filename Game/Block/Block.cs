using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 각 블럭관리;
/// </summary>
public class Block : MonoBehaviour {
	public DataCenter.BlockState blockState; // 블럭의 상태;
	public int warpTargetNo; // 이동할 블럭의 번호(워프 블럭일 경우에 해당);
	public List<int> visitedPlayers; // 방문중인 플레이어들의 인덱스 리스트;
    public MonsterCard monsterCard; // 블럭에 할당된 몬스터의 속성;
	public int expPoint = 0; // 블럭에 할당된 경험치;
	public int goNumberLadder; // 블럭이 Ladder인경우 가야할 상위 블럭번호;
	public int levelLadder; // 블럭이 Ladder인경우 필요한 플레이어의 레벨;
	public int nextFowardBlockNum = -1;
	public int nextReverseBlockNum = -1;
	public int bonusExpPoint = 0;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
		visitedPlayers = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
