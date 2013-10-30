using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     각 블럭관리;
/// </summary>
public class Block : MonoBehaviour
{
    public DataCenter.BlockState BlockState; // 블럭의 상태;
    public int BonusExpPoint = 0; // 블럭에 할당된 보너스 경험치;
    public int ExpPoint = 0; // 블럭에 할당된 경험치;
    public int GoNumberLadder; // 블럭이 Ladder인경우 가야할 상위 블럭번호;
    public GameObject KeeperObject; // 키퍼 오브젝트;
    public int LevelLadder; // 블럭이 Ladder인경우 필요한 플레이어의 레벨;
    public MonsterCard MonsterCard; // 블럭에 할당된 몬스터의 속성;
    public int NextFowardBlockNum = -1; // 다음으로 연결될 블럭 번호;
    public int NextReverseBlockNum = -1; // 이전으로 연결될 블럭 번호;
    public List<int> VisitedPlayers; // 방문중인 플레이어들의 인덱스 리스트;
    public int WarpTargetNo; // 이동할 블럭의 번호(워프 블럭일 경우에 해당);


    // Use this for initialization
    private void Start()
    {
        VisitedPlayers = new List<int>();
    }
}