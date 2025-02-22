using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private PawnType currentPawn = PawnType.Spare;
    public int Row = 0;
    public int Col = 0;
    public PawnType CurrentPawn
    {
        get
        {
            return currentPawn;
        }
    }
    //∆Â≈Ãœ‘ æ
    public void SetPawn(PawnType pawn)
    {
        this.currentPawn = pawn;
    }

    public void ShowWin()
    {

    }

    public void ResetCell()
    {
        
    }
}
