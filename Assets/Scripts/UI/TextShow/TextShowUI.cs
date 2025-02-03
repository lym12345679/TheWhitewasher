using System;
using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;

public class TextShowUI : GeneralBox<TextShowUI, TextShowUIMessage, string>
{
    public TextShowController controller;
    public override void Close()
    {
        if (param.EndHander != null)
        {
            param.EndHander();
        }
        base.Close();
    }
    public override void GetParams(TextShowUIMessage param)
    {
        base.GetParams(param);
        controller.GetTextAsset(param.TextAsset);
        controller.SetEndHander(Close);
    }
    public void Skip()
    {
        if (param.SkipHander != null)
        {
            param.SkipHander();
        }
        Close();
    }
}

public class TextShowUIMessage
{
    public TextAsset TextAsset;
    public Action EndHander;
    public Action SkipHander;
}
