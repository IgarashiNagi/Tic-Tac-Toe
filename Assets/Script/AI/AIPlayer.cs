using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    //订阅一下回合开始的消息
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

    //重载一下鼠标监听防
    public override void MouseListener()
    {
        
    }


    //获取最优行动
    public int[] GetBestMove()
    {
        PawnType[,] Board = (PawnType[,])GameState.Instance.BoardInfo.Clone();
        int BestScore = int.MinValue;
        int[] BestMove = new int[] { -1, -1 };  //默认返回个棋盘外的行动
        int MoveScore;
        foreach (int[] cell in Utils.GetSpareCell(Board)) //这一次是AI选最大，所以第一次调minimaxwithabp应该是模拟人类选最小
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
