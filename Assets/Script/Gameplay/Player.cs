using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 没啥可控制的东西，就controller跟player二合一了
    public PawnType Pawn;   //己方棋子

    // Update is called once per frame
    void Update()
    {
        MouseListener();
    }

    //留给AI用的，这里操作直接写update里了
    public virtual void TurnStart()
    {

    }

    //鼠标捕获直接写Player里吧因为只有一个操作
    public virtual void MouseListener()
    {
        if (Input.GetMouseButtonDown(0) && GameState.Instance.CurrentPlayer == this && GameState.Instance.PlayState == GameStatus.InGame)    //额外判断一下是不是自己的回合
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject Parent = hit.collider.transform.parent.gameObject;
                if (Parent.CompareTag("Cell") && Parent.GetComponent<Cell>().CurrentPawn == PawnType.Spare) //有空格才允许落子
                {
                    int[] MoveIndex = { Parent.GetComponent<Cell>().Row, Parent.GetComponent<Cell>().Col };
                    GameManager.Instance.MakeMove(MoveIndex, this);
                }
            }
        }
    }
}
