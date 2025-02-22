using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private GameObject[,] Cells = new GameObject[3, 3]; //�������и���

    private void Start()    //���Ӳ��࣬�Ž�һ��ûչƽ�Ķ�ά��������ѭ��������ֱ�Ӹ�ֵ
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


    //��ʾ��ʤ����
    public void ShowWin()
    {

    }

    //ִ����ʾ
    public void MakeMove(int[] cell, PawnType pawn)
    {
        this.Cells[cell[0], cell[1]].transform.Find(Enum.GetName(typeof(PawnType), pawn))?.gameObject.SetActive(true);
        this.Cells[cell[0], cell[1]].GetComponent<Cell>().SetPawn(pawn);
    }

    //��������
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
