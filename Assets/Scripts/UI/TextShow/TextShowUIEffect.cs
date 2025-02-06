using System;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class TextShowUIEffect : UIEffectController<Image>
{
    private FadeEffect<Image> backgroundFadeInEffect;
    private FadeEffect<Image> backgroundFadeOutEffect;
    private FadeEffect<Image> currentBackgroundFadeEffect;
    private FadeEffect<Image> shownItemFadeInEffect;
    private FadeEffect<Image> shownItemFadeOutEffect;
    private FadeEffect<Image> currentShownItemFadeEffect;
    void Awake()
    {
        backgroundFadeInEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(0, 0, 0, 200f / 255f))
            .SetFadeTime(1);
        backgroundFadeOutEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(0, 0, 0, 0))
            .SetFadeTime(1);
        shownItemFadeInEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(1, 1, 1, 1))
            .SetFadeTime(1);
        shownItemFadeOutEffect = new FadeEffect<Image>()
            .SetFadeColor(new Color(1, 1, 1, 0))
            .SetFadeTime(1);
    }
    public void StartbackgroundFadeIn(Image targetImage, Action<FadeEffect<Image>> endHander = null)
    {
        if (currentBackgroundFadeEffect != null)
        {
            currentBackgroundFadeEffect.StartEndHander();
            currentShownItemFadeEffect.FinishImmediately();
            currentBackgroundFadeEffect = null;
        }
        currentBackgroundFadeEffect = backgroundFadeInEffect.Copy(backgroundFadeInEffect).SetEndHander(endHander);
        StartFade(targetImage, currentBackgroundFadeEffect);
    }
    public void StartbackgroundFadeOut(Image targetImage, Action<FadeEffect<Image>> endHander = null)
    {
        if (currentBackgroundFadeEffect != null)
        {
            currentBackgroundFadeEffect.StartEndHander();
            currentShownItemFadeEffect.FinishImmediately();
            currentBackgroundFadeEffect = null;
        }
        currentBackgroundFadeEffect = backgroundFadeOutEffect.Copy(backgroundFadeOutEffect).SetEndHander(endHander);
        StartFade(targetImage, currentBackgroundFadeEffect);
    }
    public void StartShownItemFadeIn(Image targetImage, Action<FadeEffect<Image>> endHander = null)
    {
        if (currentShownItemFadeEffect != null)
        {
            currentShownItemFadeEffect.StartEndHander();
            currentShownItemFadeEffect.FinishImmediately();
            currentShownItemFadeEffect = null;
        }
        targetImage.color = new Color(1, 1, 1, 0);
        currentShownItemFadeEffect = shownItemFadeInEffect.Copy(shownItemFadeInEffect).SetEndHander(endHander);
        StartFade(targetImage, currentShownItemFadeEffect);
    }
    public void StartShownItemFadeOut(Image targetImage, Action<FadeEffect<Image>> endHander = null)
    {
        if (currentShownItemFadeEffect != null)
        {
            currentShownItemFadeEffect.StartEndHander();
            currentShownItemFadeEffect.FinishImmediately();
            currentShownItemFadeEffect = null;
        }
        currentShownItemFadeEffect = shownItemFadeOutEffect.Copy(shownItemFadeOutEffect).SetEndHander(endHander);
        StartFade(targetImage, currentShownItemFadeEffect);
    }

}
