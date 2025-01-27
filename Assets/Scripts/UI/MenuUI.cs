using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;

public class MenuUI : GeneralBox<MenuUI, string, string>
{
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnStartBtnClicked()
    {
        GamePlayManager.LoadScene(SceneEnum.Level1);
        Close();
    }
    public void OnSettingBtnClicked()
    {
        SettingUI.Open("1");
        Close();
    }
    public void OnLevelSelectBtnClicked()
    {
        GamePlayManager.GoToLevelSelect();
        Close();
    }
    public void OnExitBtnClicked()
    {
        GamePlayManager.ExitGame();
        Close();
    }
}
