[System.Serializable]
public class MonsterCard
{
    public string MonsterName;

    public int DefenceDiceCount = 1;
    public int HealthPoint = 1;
    public int ExperiencePoint = 2;

    /// <summary>
    /// Inits the dice count.
    /// </summary>
    public void InitDiceCount()
    {
        //        defenceDiceCount = Random.Range(1, 4);
    }
}
