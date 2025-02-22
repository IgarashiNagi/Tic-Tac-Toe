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

    // 初始化一下所有Panel到Set
    void Start()
    {
        GameManager.Instance.GameOverEvent += GameOver;
        foreach (Transform item in transform)
            this.UIPanelSet[item.gameObject.name] = item.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //监听一下ESC
        {
            PauseMenuHandler();
        }
    }

    //开关UI
    public void UIVisible(string uiName, bool isVisible)
    {
        foreach (GameObject item in UIPanelSet.Values)
            item.SetActive(false);

        this.UIPanelSet[uiName].SetActive(isVisible);
    }

    //主ui
    //游戏开始
    public void GameStartHandler()
    {
        UIVisible("MainPanel", false);
        GameManager.Instance.GameStart();
    }

    public void AIFirst(bool isAIFirst)
    {
        GameState.Instance.PlayerFirst = !isAIFirst;
    }

    //暂停菜单
    //监听ESC的动作
    public void PauseMenuHandler()
    {
        if (GameState.Instance.PlayState == GameStatus.InGame)
        {
            GameState.Instance.PlayState = GameStatus.NotInGame;
            UIVisible("PausePanel", true);
        }
    }
    //关掉暂停菜单
    public void ResumeHandler()
    {
        UIVisible("PausePanel", false);
        GameState.Instance.PlayState = GameStatus.InGame;
    }

    //结算菜单
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
    //重来
    public void RestartBtnHandler()
    {
        UIVisible("MainPanel", true);
        GameManager.Instance.GameRestart();
    }

    //退出
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
