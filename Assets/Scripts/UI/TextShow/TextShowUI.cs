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
        if (param.endHander != null)
        {
            param.endHander();
        }
        base.Close();
    }
    public override void GetParams(TextShowUIMessage param)
    {
        base.GetParams(param);
        controller.GetTextAsset(param.TextAsset);
        controller.SetEndHander(Close);
    }
}

public class TextShowUIMessage
{
    public TextAsset TextAsset;
    public Action endHander;
}
