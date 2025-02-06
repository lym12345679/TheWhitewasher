using System;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class CGUI : GeneralBox<CGUI, CGGroup, string>
{
    public CGUIEffect effect;
    public Image TargetImage;
    private TextShowUI textShowUI;
    private Stack<CGMessage> cgMessageStack = new Stack<CGMessage>();
    private bool isFirstOpen = true;
    public override void Close()
    {
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
        LoadCGMessage(param.CGEnum);
        effect.TargetImage = TargetImage;
        StartCG();
    }
    public void StartCG()
    {
        if (cgMessageStack.Count > 0)
        {
            CGMessage cgMessage = cgMessageStack.Pop();
            TargetImage.sprite = cgMessage.CGSprite;
            TargetImage.color = new Color(0, 0, 0, 1);
            StartCGBGM(cgMessage.BGMEnum);
            if (param.CGEnum == CGEnum.Begin && !isFirstOpen)
            {
                effect.QuicklyFadeIn((FadeEffect<Image> e) =>
                {
                    GameObject go = TextShowUI.Open(new TextShowUIMessage()
                    {
                        TextAsset = cgMessage.TextAsset,
                        EndHander = TryToSetNextCG,
                        SkipHander = SkipCG
                    });
                    textShowUI = go.GetComponent<TextShowUI>();
                });

            }
            else
            {
                effect.StartFadeIn(
                (FadeEffect<Image> e) =>
                {
                    GameObject go = TextShowUI.Open(new TextShowUIMessage()
                    {
                        TextAsset = cgMessage.TextAsset,
                        EndHander = TryToSetNextCG,
                        SkipHander = SkipCG
                    });
                    textShowUI = go.GetComponent<TextShowUI>();
                }
            );
                isFirstOpen = false;
            }

        }
        else
        {
            Debug.Log("CG Over");
        }
    }
    public void LoadCGMessage(CGEnum cGEnum)
    {
        cgMessageStack.Clear();
        List<CGMessage> CGMessageList = SOManager.GetCGMessageList(cGEnum);
        for (int i = CGMessageList.Count - 1; i >= 0; i--)
        {
            cgMessageStack.Push(CGMessageList[i]);
        }
    }
    public void TryToSetNextCG()
    {
        if (cgMessageStack.Count > 0)
        {
            StartCG();
        }
        else
        {
            Close();
        }
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
}
[Serializable]
public class CGMessage
{
    public Sprite CGSprite;
    public TextAsset TextAsset;
    public AudioEnum BGMEnum;
}

public class CGGroup
{
    public CGEnum CGEnum;
    public Action EndHander;
    public Action SkipHander;
}
