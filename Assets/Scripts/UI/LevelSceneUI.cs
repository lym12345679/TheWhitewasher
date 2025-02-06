
using MizukiTool.Box;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneUI : GeneralBox<LevelSceneUI, LevelSceneMessage, string>
{
    public static LevelSceneUI Instance;
    public Image BottomBackground;
    public RectTransform LevelMessageContent;
    public GameObject LevelMessageItem;
    public void Awake()
    {
        Instance = this;
    }
    public override void GetParams(LevelSceneMessage param)
    {
        this.param = param;
        BottomBackground.sprite = param.BottomBackground;
        SetLevelMessageItem(param.currentLevel);
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    private void SetLevelMessageItem(int level)
    {
        Debug.Log("level:" + level);
        for (int i = 0; i < 8; i++)
        {
            GameObject go = Instantiate(LevelMessageItem, LevelMessageContent);
            if (i < level)
            {
                go.GetComponent<LevelMessageItem>().SetUnlocked();
            }
            else
            {
                go.GetComponent<LevelMessageItem>().SetLocked();
            }
        }
    }

    public void OnPauseBtnClicked()
    {
        PauseUI.Open("1");
    }
    public void OnRestartBtnClicked()
    {
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.ResetGame();
        }));
    }
    public void OnGoToMenuBtnClicked()
    {
        AstarManagerSon.Instance.SetTPFD();
        /*SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToMenu();
        }));*/
    }
}
public class LevelSceneMessage
{
    public Sprite BottomBackground;
    public int currentLevel;
}
