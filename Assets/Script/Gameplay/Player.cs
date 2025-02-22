using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ûɶ�ɿ��ƵĶ�������controller��player����һ��
    public PawnType Pawn;   //��������

    // Update is called once per frame
    void Update()
    {
        MouseListener();
    }

    //����AI�õģ��������ֱ��дupdate����
    public virtual void TurnStart()
    {

    }

    //��겶��ֱ��дPlayer�����Ϊֻ��һ������
    public virtual void MouseListener()
    {
        if (Input.GetMouseButtonDown(0) && GameState.Instance.CurrentPlayer == this && GameState.Instance.PlayState == GameStatus.InGame)    //�����ж�һ���ǲ����Լ��Ļغ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject Parent = hit.collider.transform.parent.gameObject;
                if (Parent.CompareTag("Cell") && Parent.GetComponent<Cell>().CurrentPawn == PawnType.Spare) //�пո����������
                {
                    int[] MoveIndex = { Parent.GetComponent<Cell>().Row, Parent.GetComponent<Cell>().Col };
                    GameManager.Instance.MakeMove(MoveIndex, this);
                }
            }
        }
    }
}
