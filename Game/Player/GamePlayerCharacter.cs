using UnityEngine;
using System.Collections;

[System.Serializable]
public class GamePlayerCharacter {	
	[SerializeField]
	private int experience;
	
	public int Experience {
		get {
			return experience;
		}
		
		set {
			experience = value;					
			
			if (experience >= DataCenter.playerLevel[DataCenter.playerLevel.Length-1]) {
				level = DataCenter.playerLevel.Length + 1;
			}
			else {
				for (int i = 0; i < DataCenter.playerLevel.Length; i++) {
					if (experience < DataCenter.playerLevel[i]) {
						level = i+1;
						break;
					}
				}	
			}
			
		}
	}
	
	[SerializeField]
	private int level;
	
	public int Level {
		get	{
			return level;
		}
		set	{
			level = value;
		}
	}
	
	public GamePlayerCharacter ()
	{
		experience = 0;
		level = 1;
	}
	
	private int healthPoint;
	
	public int HealthPoint {
		get {
			return healthPoint;
		}
		set {
			healthPoint = value;
		}
	}
}
