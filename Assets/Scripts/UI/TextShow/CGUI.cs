using System;
using MizukiTool.Box;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class CGUI : GeneralBox<CGUI, CGMessage, string>
{
    public CGUIEffect effect;
    public Image TargetImage;
    public override void Close()
    {
        effect.StartFadeOut((FadeEffect<Image> e) =>
        {
            if (param.endHander != null)
            {
                param.endHander();
            }
            base.Close();
        });
    }
    public override void GetParams(CGMessage param)
    {
        base.GetParams(param);
        TargetImage.sprite = param.CGSprite;
        TargetImage.color = new Color(1, 1, 1, 0);
        effect.TargetImage = TargetImage;

        Action endHander = () =>
        {
            Close();
        };
        effect.StartFadeIn(
            (FadeEffect<Image> e) =>
            {
                TextShowUI.Open(new TextShowUIMessage()
                {
                    TextAsset = param.TextAsset,
                    endHander = endHander
                });
            }
        );
    }
}
[Serializable]
public class CGMessage
{
    public Sprite CGSprite;
    public TextAsset TextAsset;
    public Action endHander;
}
