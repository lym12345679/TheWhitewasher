using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;

public class PauseUI : GeneralBox<PauseUI, string, string>
{
    void Start()
    {
        GamePlayManager.PauseGame();
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnContinueBtnClicked()
    {
        GamePlayManager.ContinueGame();
        Close();
    }
    public void OnGoToLevelBtnClicked()
    {
        GamePlayManager.ContinueGame();
        GamePlayManager.GoToLevelSelect();
        Close();
    }
    public void OnGoToMenuBtnClicked()
    {
        GamePlayManager.ContinueGame();
        GamePlayManager.GoToMenu();
        Close();
    }

}

