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
    public Image BackImage;
    private TextShowUI textShowUI;
    private Stack<CGMessage> cgMessageStack = new Stack<CGMessage>();
    public override void Close()
    {
        BackImage.color = new Color(1, 1, 1, 0);
        if (param.EndHander != null)
        {
            param.EndHander();
        }
        effect.StartFadeOut((FadeEffect<Image> e) =>
        {
            base.Close();
        });
    }
    public override void GetParams(CGGroup param)
    {
        base.GetParams(param);
        LoadCGMessage(param.CGEnum);
        DialogCGCorrect();
        effect.TargetImage = TargetImage;
        StartCG();
    }
    public void StartCG()
    {
        if (cgMessageStack.Count > 0)
        {
            CGMessage cgMessage = cgMessageStack.Pop();
            TargetImage.sprite = cgMessage.CGSprite;
            TargetImage.color = new Color(1, 1, 1, 0);
            StartCGBGM(cgMessage.BGMEnum);
            effect.StartFadeIn(
            (FadeEffect<Image> e) =>
                {
                    GameObject go = TextShowUI.Open(new TextShowUIMessage()
                    {
                        TextAsset = cgMessage.TextAsset,
                        EndHander = TryToSetNextCG,
                        SkipHander = SkipCG,

                    });
                    textShowUI = go.GetComponent<TextShowUI>();
                    DilaogCGTextShowUICorrect(textShowUI.GetComponent<TextShowController>());
                    BackImage.sprite = cgMessage.CGSprite;
                    BackImage.color = new Color(1, 1, 1, 1);
                    //BackImage.sprite = cgMessage.CGSprite;
                }
            );


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
    private void DialogCGCorrect()
    {
        if (param.CGEnum == CGEnum.Dialog)
        {
            Debug.Log("Begin");
            RectTransform rectTransform = TargetImage.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(0, 0); // Adjust the padding from the left and bottom
            rectTransform.offsetMax = new Vector2(0, 0); // Adjust the padding from the right and top
        }
    }
    private void DilaogCGTextShowUICorrect(TextShowController textShowController)
    {
        if (param.CGEnum == CGEnum.Dialog)
        {
            RectTransform rectTransform = textShowController.Panel.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(-100, -240, 0);
            RectTransform leftTransform = textShowController.LeftImg.GetComponent<RectTransform>();
            leftTransform.localPosition = new Vector3(-580, -230, 0);
            //textShowController.Panel.position = new Vector3(textShowController.Panel.position.x, -260, 0);
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
