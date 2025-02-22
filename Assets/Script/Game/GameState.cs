using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //也搞个单例x
    private static GameState gameState;
    private GameState() { }
    public static GameState Instance
    {
        get => gameState;
    }

    private void Awake()
    {
        if (gameState != null)
            Destroy(gameObject);
        else
        {
            gameState = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameStatus PlayState = GameStatus.NotInGame;  //游玩状态
    public bool PlayerFirst = true;
    private Player currentPlayer; //焦点玩家
    private static readonly Dictionary<string, int> HistoryScore = new(); //历史计分
    private static readonly PawnType[,] boardInfo = new PawnType[3, 3];  //井字棋3*3

    //当前玩家
    public Player CurrentPlayer
    {
        get => currentPlayer;
        set => currentPlayer = value;
    }

    //当前棋盘
    public PawnType[,] BoardInfo
    {
        get => boardInfo;
    }

    //调试打印棋盘信息
    public void ShowBoard()
    {
        foreach (var item in boardInfo)
        {
            print(string.Join(",", item));
        }
    }

    //重置棋盘信息
    public void ResetBoardInfo()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                boardInfo[i, j] = PawnType.Spare;
            }
        }
    }
}
