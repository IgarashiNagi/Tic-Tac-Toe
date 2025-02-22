using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    //����һ�»غϿ�ʼ����Ϣ
    private void OnEnable()
    {
        GameManager.Instance.TurnStartEvent += TurnStart;
    }
    private void OnDisable()
    {
        GameManager.Instance.TurnStartEvent -= TurnStart;
    }

    public override void TurnStart()
    {
        if (GameState.Instance.CurrentPlayer == this)
        {
            int[] Move = GetBestMove();
            GameManager.Instance.MakeMove(Move, this);
        }
    }

    //����һ����������
    public override void MouseListener()
    {
        
    }


    //��ȡ�����ж�
    public int[] GetBestMove()
    {
        PawnType[,] Board = (PawnType[,])GameState.Instance.BoardInfo.Clone();
        int BestScore = int.MinValue;
        int[] BestMove = new int[] { -1, -1 };  //Ĭ�Ϸ��ظ���������ж�
        int MoveScore;
        foreach (int[] cell in Utils.GetSpareCell(Board)) //��һ����AIѡ������Ե�һ�ε�minimaxwithabpӦ����ģ������ѡ��С
        {
            Board[cell[0], cell[1]] = this.Pawn;
            MoveScore = MiniMax.MiniMaxWithABP(this, Board);
            Board[cell[0], cell[1]] = PawnType.Spare;

            if (MoveScore > BestScore)
            {
                BestScore = MoveScore;
                BestMove = cell;
            }
        }
        return BestMove;
    }
}
