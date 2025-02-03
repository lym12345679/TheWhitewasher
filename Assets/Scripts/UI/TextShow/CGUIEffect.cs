using System;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class CGUIEffect : UIEffectController<Image>
{
    [HideInInspector]
    public Image TargetImage;
    private FadeEffect<Image> fadeInEffect;
    private FadeEffect<Image> fadeOutEffect;
    void Awake()
    {
        fadeInEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(1, 1, 1, 1))
            .SetFadeTime(1);
        fadeOutEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(1, 1, 1, 0))
            .SetFadeTime(1);
    }
    void Start()
    {

    }

    public void StartFadeIn(Action<FadeEffect<Image>> endHander = null)
    {

        FadeEffect<Image> fadeEffect = fadeInEffect.SetEndHander(endHander);
        StartFade(TargetImage, fadeEffect);
    }
    public void StartFadeOut(Action<FadeEffect<Image>> endHander = null)
    {
        FadeEffect<Image> fadeEffect = fadeOutEffect.SetEndHander(endHander);
        StartFade(TargetImage, fadeEffect);
    }

}
