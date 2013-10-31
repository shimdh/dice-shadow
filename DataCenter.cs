/// <summary>
///     게임내에 공통적으로 쓰이는 데이터 관리;
/// </summary>
public static class DataCenter
{
    #region Enums

    /// <summary>
    ///     Battle dice rule.
    /// </summary>
    public enum BattleDiceRule // 전투시 이길 주사위 룰;
    {
        High, // 주사위들의 합이 높은 플레이어가 승;
        Low, // 주사위들의 합이 낮은 플레이어가 승;
    };

    /// <summary>
    ///     Block state.
    /// </summary>
    public enum BlockState // 블럭의 상태;
    {
        Monster, // 몬스터;
        Warp1, // 워프;
        Warp2, // 워프;
        Warp3, // 워프;
        Ladder, // 상위 블럭으로 갈수 있음;
        CoolFill,
        CoolReset,
        RuleHigh, // 전투룰 변경;
        RuleLow, // 전투룰 변경;
        Battle, // 전투;
        RandomBox, // 보너스 상자;
        Gamble, // 겜블;
        ComeHere, // 타겟 플레이어를 내 위치로 이동;
        ImGoing, // 자신을 타켓 플레이어 위치로 이동;
        Start, // 출발지;
        Goal, // 목적지;
        Blank,
        Keeper, // 몬스터 블럭이나 무조건 멈춤;
    };

    /// <summary>
    ///     Game state.
    /// </summary>
    public enum GameState // 게임의 상태;
    {
        Idle, // 초기 상태;
        Started, // 게임 시작됨;
        Battled, // 다른 상대와 전투;
        Monster, // 몬스터와 전투;
        Gamble, // 겜블;
        Ladder, // 사다리;
        Paused,
        MyTurn, // 내 턴;
        Finished, // 게임 종료;
    };

    #endregion

    #region Variables

    public const string GameSceneObjectName = "go_SceneManager"; // 게임씬 매니저의 이름;
    public const string HexPrefix = "hex_tile_"; // 블럭 이름을 가져올 접두어;
    public static int PlayerCount = 2; // 현재 게임중인 플레이어 수;
    public static int MoveAllDiceCount = 3; // 화면에 있는 전체 이동 주사위 갯수(안보이는 것도 포함);
    public static int MoveDiceCount = 1; // 이동 주사위의 갯수;
    public static int PlayerTurnNo = 0; // 현재 게임 진행중인 플레이어 인덱스;
    public static GameState State = GameState.Idle; // 현재 게임의 상태;
    public static BattleDiceRule BattleRule = BattleDiceRule.High; // 현재 진행중인 게임의 배틀시 주사위 룰;
    public static int BattleRuleRemainTurn = 0;

    public static int[] BlockCount = {20, 10, 10};

    public static int[] PlayerLevel =
    {
        100, 200, 300, 400, 500, 600, 700, 800, 900
    };

    #endregion
}