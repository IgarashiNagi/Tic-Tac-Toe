using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //Ҳ�������x
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

    public GameStatus PlayState = GameStatus.NotInGame;  //����״̬
    public bool PlayerFirst = true;
    private Player currentPlayer; //�������
    private static readonly Dictionary<string, int> HistoryScore = new(); //��ʷ�Ʒ�
    private static readonly PawnType[,] boardInfo = new PawnType[3, 3];  //������3*3

    //��ǰ���
    public Player CurrentPlayer
    {
        get => currentPlayer;
        set => currentPlayer = value;
    }

    //��ǰ����
    public PawnType[,] BoardInfo
    {
        get => boardInfo;
    }

    //���Դ�ӡ������Ϣ
    public void ShowBoard()
    {
        foreach (var item in boardInfo)
        {
            print(string.Join(",", item));
        }
    }

    //����������Ϣ
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
