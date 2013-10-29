using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Monster card.
/// </summary>
public class MonsterCard {
    public string monsterName;

    public int defenceDiceCount = 1;
    public int healthPoint = 1;
	public int experiencePoint = 2;

    public MonsterCard()
    {

    }
	
	/// <summary>
	/// Inits the dice count.
	/// </summary>
    public void InitDiceCount()
    {
//        defenceDiceCount = Random.Range(1, 4);
    }
}
