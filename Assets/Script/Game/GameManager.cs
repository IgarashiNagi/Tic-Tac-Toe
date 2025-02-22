using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�������
    private static GameManager gameManager;
    private GameManager() { }
    public static GameManager Instance
    {
        get => gameManager;
    }
    private void Awake()
    {
        if (gameManager != null)
            Destroy(gameObject);
        else
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private readonly Player[] Players = new Player[2];    //����б�
    public delegate void TurnStartHnadler();
    public event TurnStartHnadler TurnStartEvent;
    public delegate void GameOverHnadler();
    public event GameOverHnadler GameOverEvent;
    public BoardManager BoardInstance;  //����

    // Start is called before the first frame update
    void Start()
    {
        GameInit();
    }

    public void GameInit()
    {
        //��һ������ʵ��
        this.BoardInstance = GameObject.Find("Board").GetComponent<BoardManager>();
        //������player
        this.Players[0] = GameObject.Find("Players").AddComponent<Player>();
        this.Players[1] = GameObject.Find("Players").AddComponent<AIPlayer>();
        //GameStart();
    }

    public void GameStart()
    {
        this.Players[0].Pawn = GameState.Instance.PlayerFirst ? PawnType.X : PawnType.O;
        this.Players[1].Pawn = GameState.Instance.PlayerFirst ? PawnType.O : PawnType.X;
        GameState.Instance.PlayState = GameStatus.InGame;
        GameState.Instance.CurrentPlayer = GameState.Instance.PlayerFirst ? this.Players[0] : this.Players[1];
        TurnStart();
    }

    public void GameRestart()
    {
        this.BoardInstance.ResetBoard();
        GameState.Instance.ResetBoardInfo();
        //GameStart();
    }

    //��Ϸ����ж�
    public bool CheckWin()
    {
        int Score = Utils.GetScore(GameState.Instance.BoardInfo, this.Players[0]);
        if (Score == 1 || Score == -1)
        {
            GameState.Instance.PlayState = Score == 1 ? GameStatus.PlayerWin : GameStatus.AIWin;
            //GameState.Instance.ShowBoard();
            return true;
        }
        return false;

    }

    public bool CheckDraw()
    {
        bool IsDraw = Utils.CheckDraw(GameState.Instance.BoardInfo, this.Players[0]);
        if (IsDraw)
            GameState.Instance.PlayState = GameStatus.Draw;
        return IsDraw;
    }

    //��Ϸ����
    public void GameOver()
    {
        //û����Ӯ����
        //print(GameState.Instance.PlayState);
        this.GameOverEvent?.Invoke();
    }
    
    //��һ�»غϿ�ʼ�¼�
    public void TurnStart()
    {
        this.TurnStartEvent?.Invoke();
    }

    //�غϽ���
    public void TurnEnd()
    {
        if (CheckWin() || CheckDraw())
        {
            GameOver();  //ʤ��ƽֱ�ӽ���
            return;
        }
        GameState.Instance.CurrentPlayer = GetNextPlayer(); //�����
        TurnStart();    //����һ�غ�
    }

    //ִ��
    public void MakeMove(int[] moveIndex, Player player)
    {
        if (GameState.Instance.PlayState != GameStatus.InGame) return;
        if (moveIndex[0] < 0) return;   //ai�����һ��
        GameState.Instance.BoardInfo[moveIndex[0], moveIndex[1]] = player.Pawn;
        BoardInstance.MakeMove(moveIndex, player.Pawn);
        TurnEnd();
    }

    public Player GetNextPlayer() => GameState.Instance.CurrentPlayer == this.Players[0] ? this.Players[1] : this.Players[0];
}
