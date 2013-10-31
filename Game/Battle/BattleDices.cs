using UnityEngine;

/// <summary>
///     해당 플레이어의 전투시에 사용될 주사위들 관리;
/// </summary>
public class BattleDices : MonoBehaviour
{
    public GameObject[] BattleDiceObjects; // 해당하는 주사위 오브젝트들;
    public BattleDiceTotal BattleDiceTotal; // 주사위들의 합을 표시할 오브젝트;
    public BattlePlayerHealth BattlePlayerHealth; // 해당하는 플레이어의 생명 포인트 오브젝트;
    public BattlePlayerName BattlePlayerName; // 해당하는 플레이어 이름 오브젝트;
    public int DiceCount; // 유효한 주사위 갯수;
    public int DiceTotalNumber;

    /// <summary>
    ///     전체 주사위들을 비활성화;
    /// </summary>
    /// <returns></returns>
    public void DisableAllDices()
    {
        foreach (var t in BattleDiceObjects)
        {
            t.SetActive(false);
        }
    }


    /// <summary>
    ///     유효한 갯수에 해당되는 주사위들을 활성화;
    /// </summary>
    /// <param name="diceCount"></param>
    /// <returns></returns>
    public void ActiveValidDices(int diceCount)
    {
        DiceCount = diceCount;

        for (var i = 0; i < DiceCount; i++)
        {
            BattleDiceObjects[i].SetActive(true);
        }
    }


    /// <summary>
    ///     유효한 주사위들을 초기화;
    /// </summary>
    /// <returns></returns>
    public void ResetValidDices()
    {
        for (var i = 0; i < DiceCount; i++)
        {
            var dice = BattleDiceObjects[i].GetComponent<BattleDice>();
            dice.UpdateDiceLabel(0);
        }
        BattleDiceTotal.UpdateDiceTotalLabel(0);
//		battlePlayerName.UpdatePlayerName("");
        //battlePlayerHealth.UpdateHealthPoint(0);
    }


    /// <summary>
    ///     각 유효한 주사위 수를 생성;
    /// </summary>
    /// <returns></returns>
    public void RollDices()
    {
        Debug.Log("RollDices");

        ShowDicesNumber();

        //StartCoroutine("ShowDicesNumber");
        //diceTotalNumber = 0;
        //for (int i = 0; i < diceCount; i++) {
        //    BattleDice dice = BattleDiceObjects[i].GetComponent<BattleDice>();
        //    dice.GenerateDiceNumber();
        //    diceTotalNumber += dice.diceNumber;
        //}		
        //battleDiceTotal.UpdateDiceTotalLabel(diceTotalNumber);
    }

    /// <summary>
    ///     Shows the dices number.
    /// </summary>
    public void ShowDicesNumber()
    {
        DiceTotalNumber = 0;

        for (var i = 0; i < DiceCount; i++)
        {
            var dice = BattleDiceObjects[i].GetComponent<BattleDice>();
            dice.GenerateDiceNumber();
            DiceTotalNumber += dice.DiceNumber;
        }

        BattleDiceTotal.UpdateDiceTotalLabel(DiceTotalNumber);
    }
}