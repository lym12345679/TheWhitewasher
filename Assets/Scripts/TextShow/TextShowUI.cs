using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;

public class TextShowUI : GeneralBox<TextShowUI, TextAsset, string>
{
    public TextShowController controller;
    public override void Close()
    {
        base.Close();
    }
    public override void GetParams(TextAsset param)
    {
        base.GetParams(param);
        controller.GetTextAsset(param);
    }

}
