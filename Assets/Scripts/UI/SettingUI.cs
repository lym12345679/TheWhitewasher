using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;

public class SettingUI : GeneralBox<SettingUI, string, string>
{
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnReturnBtnClicked()
    {
        Close();
    }
}
