using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private GameObject[,] Cells = new GameObject[3, 3]; //保存所有格子

    private void Start()    //格子不多，放进一个没展平的二维数组里用循环还不如直接赋值
    {
        int i = 0, j = 0;
        foreach (Transform item in transform)
        {
            this.Cells[i, j] = item.gameObject;
            ++j;
            if (j >= 3)
            {
                ++i;
                j = 0;
            }
        }
    }


    //显示获胜棋盘
    public void ShowWin()
    {

    }

    //执棋显示
    public void MakeMove(int[] cell, PawnType pawn)
    {
        this.Cells[cell[0], cell[1]].transform.Find(Enum.GetName(typeof(PawnType), pawn))?.gameObject.SetActive(true);
        this.Cells[cell[0], cell[1]].GetComponent<Cell>().SetPawn(pawn);
    }

    //重置棋盘
    public void ResetBoard()
    {
        foreach (var item in this.Cells)
        {
            item.transform.Find("X")?.gameObject.SetActive(false);
            item.transform.Find("O")?.gameObject.SetActive(false);
            item.GetComponent<Cell>().SetPawn(PawnType.Spare);
        }
    }
}
