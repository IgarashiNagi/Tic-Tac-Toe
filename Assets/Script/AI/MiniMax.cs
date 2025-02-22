using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MiniMax
{
    public static int MiniMaxWithABP(Player aiPlayer, PawnType[,] board, int depth = 0, int alpha = int.MinValue, int beta = int.MaxValue, bool isAI = false)
    {
        if (depth > 9) return 0;    //�����岻�ᷢ������дһ��ƽ��ռλ��

        int score = Utils.GetScore(board, aiPlayer);

        if (score == 1 || score == -1 || Utils.CheckDraw(board, aiPlayer)) //��ʤ��ƽֱ�ӷ���
            return score;

        if (isAI)
        {
            int best_score = int.MinValue;
            foreach (int[] cell in Utils.GetSpareCell(board))
            {
                board[cell[0], cell[1]] = aiPlayer.Pawn;
                best_score = Mathf.Max(best_score, MiniMaxWithABP(aiPlayer, board, depth + 1, alpha, beta, false));
                board[cell[0], cell[1]] = PawnType.Spare; //������̽

                alpha = Mathf.Max(alpha, best_score);
                if (beta <= alpha)
                    break;
            }

            return best_score;
        }
        else
        {
            int best_score = int.MaxValue;

            foreach (int[] cell in Utils.GetSpareCell(board))
            {
                board[cell[0], cell[1]] = Utils.GetOpposingPawn(aiPlayer.Pawn);
                best_score = Mathf.Min(best_score, MiniMaxWithABP(aiPlayer, board, depth + 1, alpha, beta, true));
                board[cell[0], cell[1]] = PawnType.Spare; //������̽

                beta = Mathf.Min(beta, best_score);
                if (beta <= alpha)
                    break;
            }

            return best_score;
        }
    }
}
