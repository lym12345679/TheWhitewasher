using System;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCGUI : GeneralBox<SpecialCGUI, CGGroup, string>
{
    public CGUIEffect effect;
    public Image TargetImage;
    private TextShowUI textShowUI;
    private Stack<CGMessage> cgMessageStack = new Stack<CGMessage>();
    private bool IsTextShowUIOpened = false;
    public override void Close()
    {
        Debug.Log("SpecialCG Over");
        effect.StartFadeOut((FadeEffect<Image> e) =>
        {
            if (param.EndHander != null)
            {
                param.EndHander();
            }
            base.Close();
        });
    }
    public override void GetParams(CGGroup param)
    {
        base.GetParams(param);
        effect.TargetImage = TargetImage;
    }

    public void StartTextShow(CGShownItemEnum shownItemEnum)
    {
        IsTextShowUIOpened = true;
        SpecialCGMessage specialCGMessage = SOManager.specialCGSO.GetSpecialCGMessageList(shownItemEnum);
        effect.StartFadeIn(
            (FadeEffect<Image> e) =>
            {
                GameObject go = TextShowUI.Open(new TextShowUIMessage()
                {
                    TextAsset = specialCGMessage.TextAsset,
                    //EndHander = TryToSetNextCG,
                    EndHander = () => { IsTextShowUIOpened = false; },
                    SkipHander = SkipCG

                });
                textShowUI = go.GetComponent<TextShowUI>();
            });
    }

    public void OnFirstUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }

        StartTextShow(CGShownItemEnum.油画板);
    }
    public void OnSecondUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }

        StartTextShow(CGShownItemEnum.咖啡黄昏);
    }
    public void OnThirdUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }
        StartTextShow(CGShownItemEnum.玻璃画);
    }
    public void OnFourthUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }
        StartTextShow(CGShownItemEnum.银河);
    }
    public void OnFifthUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }
        StartTextShow(CGShownItemEnum.秃头铅笔);
    }
    public void OnSixthUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }
        StartTextShow(CGShownItemEnum.写生册);
    }
    public void OnSeventhUIClicked()
    {
        if (IsTextShowUIOpened)
        {
            return;
        }
        StartTextShow(CGShownItemEnum.废稿);
    }
    public void SkipCG()
    {
        cgMessageStack.Clear();
    }
    public void StartCGBGM(AudioEnum e)
    {
        if (!AudioUtil.CheckEnumInLoopAudio(e))
        {
            AudioUtil.ReturnAllLoopAudio();
            AudioUtil.Play(e, AudioMixerGroupEnum.BGM, AudioPlayMod.Loop);
        }
    }
    public void OnGoToMenuBtmClicked()
    {
        GamePlayManager.GoToMenu();
    }
}
[Serializable]
public class SpecialCGMessage
{
    [HideInInspector]
    public string Name;
    public CGShownItemEnum ShownItemEnum;
    public TextAsset TextAsset;

}