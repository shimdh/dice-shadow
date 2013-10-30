using UnityEngine;

/// <summary>
/// Move dices.
/// </summary>
public class MoveDicesManager : MonoBehaviour {
    public MoveDice[] MoveDices;
    public int TotalDiceNumber;

    void Awake()
    {
        MoveDices = new MoveDice[DataCenter.MoveAllDiceCount];

        GetObjects();
    }

	// Use this for initialization
	void Start () {

        ResetDices();
	}
	
	/// <summary>
	/// Gets the objects.
	/// </summary>
    private void GetObjects()
    {
        for (var i = 0; i < DataCenter.MoveAllDiceCount; i++)
        {
            MoveDices[i] = GameObject.Find("Dice " + (i + 1)).GetComponent<MoveDice>();
        }
    }
	
	/// <summary>
	/// Resets the dices.
	/// </summary>
    public void ResetDices()
    {
        for (var i = 0; i < MoveDices.Length; i++)
        {
            MoveDices[i].ResetDiceNumber();
            if (DataCenter.MoveDiceCount < i+1)
            {
                MoveDices[i].DeactiveDice();
            }
        }

        TotalDiceNumber = 0;
    }
	
	/// <summary>
	/// Generates the dice number.
	/// </summary>
    public void GenerateDiceNumber()
    {
        TotalDiceNumber = 0;

        for (var i = 0; i < DataCenter.MoveDiceCount; i++)
        {
            MoveDices[i].GetDiceNumber();
            TotalDiceNumber += MoveDices[i].Number;
        }
    }



}
