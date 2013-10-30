using UnityEngine;

[System.Serializable]
public class GamePlayerCharacter {	
	[SerializeField]
	private int _experience;
	
	public int Experience {
		get {
			return _experience;
		}
		
		set {
			_experience = value;					
			
			if (_experience >= DataCenter.PlayerLevel[DataCenter.PlayerLevel.Length-1]) {
				_level = DataCenter.PlayerLevel.Length + 1;
			}
			else {
				for (var i = 0; i < DataCenter.PlayerLevel.Length; i++) {
				    if (_experience >= DataCenter.PlayerLevel[i]) continue;
				    _level = i+1;
				    break;
				}	
			}
			
		}
	}
	
	[SerializeField]
	private int _level;
	
	public int Level {
		get	{
			return _level;
		}
		set	{
			_level = value;
		}
	}
	
	public GamePlayerCharacter ()
	{
		_experience = 0;
		_level = 1;
	}
	
	private int _healthPoint;
	
	public int HealthPoint {
		get {
			return _healthPoint;
		}
		set {
			_healthPoint = value;
		}
	}
}
