using UnityEngine;
using System.Collections;

/// <summary>
/// Move dices.
/// </summary>
public class MoveDices : MonoBehaviour {
    public MoveDice[] moveDice;
    public int totalDiceNumber;

    void Awake()
    {
        moveDice = new MoveDice[DataCenter.moveAllDiceCount];

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
        for (int i = 0; i < DataCenter.moveAllDiceCount; i++)
        {
            moveDice[i] = GameObject.Find("Dice " + (i + 1)).GetComponent<MoveDice>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Resets the dices.
	/// </summary>
    public void ResetDices()
    {
        for (int i = 0; i < moveDice.Length; i++)
        {
            moveDice[i].ResetDiceNumber();
            if (DataCenter.moveDiceCount < i+1)
            {
                moveDice[i].DeactiveDice();
            }
        }

        totalDiceNumber = 0;
    }
	
	/// <summary>
	/// Generates the dice number.
	/// </summary>
    public void GenerateDiceNumber()
    {
        totalDiceNumber = 0;

        for (int i = 0; i < DataCenter.moveDiceCount; i++)
        {
            moveDice[i].GetDiceNumber();
            totalDiceNumber += moveDice[i].number;
        }
    }



}
