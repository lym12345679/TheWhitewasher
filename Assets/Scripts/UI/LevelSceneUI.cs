using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEngine;

public class LevelSceneUI : GeneralBox<LevelSceneUI, string, string>
{
    public static LevelSceneUI Instance;
    public void Awake()
    {
        Instance = this;
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
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
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToMenu();
        }));
    }


}
