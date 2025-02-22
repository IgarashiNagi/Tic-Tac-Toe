using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button RestartBtn;
    private readonly Dictionary<string, GameObject> UIPanelSet = new();

    void OnDestroy()
    {
        GameManager.Instance.GameOverEvent -= GameOver;
    }

    // ��ʼ��һ������Panel��Set
    void Start()
    {
        GameManager.Instance.GameOverEvent += GameOver;
        foreach (Transform item in transform)
            this.UIPanelSet[item.gameObject.name] = item.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //����һ��ESC
        {
            PauseMenuHandler();
        }
    }

    //����UI
    public void UIVisible(string uiName, bool isVisible)
    {
        foreach (GameObject item in UIPanelSet.Values)
            item.SetActive(false);

        this.UIPanelSet[uiName].SetActive(isVisible);
    }

    //��ui
    //��Ϸ��ʼ
    public void GameStartHandler()
    {
        UIVisible("MainPanel", false);
        GameManager.Instance.GameStart();
    }

    public void AIFirst(bool isAIFirst)
    {
        GameState.Instance.PlayerFirst = !isAIFirst;
    }

    //��ͣ�˵�
    //����ESC�Ķ���
    public void PauseMenuHandler()
    {
        if (GameState.Instance.PlayState == GameStatus.InGame)
        {
            GameState.Instance.PlayState = GameStatus.NotInGame;
            UIVisible("PausePanel", true);
        }
    }
    //�ص���ͣ�˵�
    public void ResumeHandler()
    {
        UIVisible("PausePanel", false);
        GameState.Instance.PlayState = GameStatus.InGame;
    }

    //����˵�
    public void GameOver()
    {
        UIVisible("SettlePanel", true);

        TextMeshProUGUI ResultUI = UIPanelSet["SettlePanel"].transform.Find("Result").GetComponent<TextMeshProUGUI>();
        switch (GameState.Instance.PlayState)
        {
            case GameStatus.Draw:
                ResultUI.text = "DRAW";
                break;
            case GameStatus.PlayerWin:
                ResultUI.text = "YOU WIN!!!";
                break;
            case GameStatus.AIWin:
                ResultUI.text = "YOU LOSE";
                break;
            default:
                break;
        }
    }
    //����
    public void RestartBtnHandler()
    {
        UIVisible("MainPanel", true);
        GameManager.Instance.GameRestart();
    }

    //�˳�
    public void QuitBtnHandler()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        if (!Application.isEditor)
            Application.Quit();
    }
}
