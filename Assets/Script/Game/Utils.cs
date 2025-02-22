using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//һЩ��̬���߷�����
public static class Utils
{
    // ��ȡ����������
    public static PawnType GetOpposingPawn(PawnType pawn)
    {
        return pawn == PawnType.X ? PawnType.O : PawnType.X;
    }

    //��ȡ�������ӵĸ���λ��
    public static List<int[]> GetSpareCell(PawnType[,] board)
    {
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        List<int[]> result = new();

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                if (board[i, j] == PawnType.Spare)
                {
                    int[] cell = new int[2];
                    cell[0] = i;
                    cell[1] = j;
                    result.Add(cell);
                }
        return result;
    }

    //���ƽ��
    public static bool CheckDraw(PawnType[,] board, Player player)
    {
        if (!Array.Exists<PawnType>(board.Cast<PawnType>().ToArray(), element => element == PawnType.Spare) && Utils.GetScore(board, player) == 0)
            return true;
        return false;
    }


    //��ȡ�÷�/�ж�ʤ��
    public static int GetScore(PawnType[,] board, Player player)
    {
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        List<int[]> result = new();

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if (board[i, j] != PawnType.Spare)
                {
                    if (i - 1 >= 0 && i + 1 < rows && board[i - 1, j] == board[i, j] && board[i + 1, j] == board[i, j])
                        return board[i, j] == player.Pawn ? 1 : -1;
                    if (j - 1 >= 0 && j + 1 < cols && board[i, j - 1] == board[i, j] && board[i, j + 1] == board[i, j])
                        return board[i, j] == player.Pawn ? 1 : -1;
                    if (i - 1 >= 0 && i + 1 < rows && j - 1 >= 0 && j + 1 < cols && board[i - 1, j - 1] == board[i, j] && board[i + 1, j + 1] == board[i, j])
                        return board[i, j] == player.Pawn ? 1 : -1;
                    if (i - 1 >= 0 && i + 1 < rows && j - 1 >= 0 && j + 1 < cols && board[i - 1, j + 1] == board[i, j] && board[i + 1, j - 1] == board[i, j])
                        return board[i, j] == player.Pawn ? 1 : -1;
                }
            }
        return 0;
    }

}
