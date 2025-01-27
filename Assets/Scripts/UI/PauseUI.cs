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
        Debug.Log("Go to level");
        Close();
    }
    public void OnGoToMenuBtnClicked()
    {
        Debug.Log("Go to menu");
        Close();
    }

}

