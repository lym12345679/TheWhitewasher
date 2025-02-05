using System;
using MizukiTool.UIEffect;
using UnityEngine;

public class SquareEffect : GoEffectController<SpriteRenderer>
{
    public SpriteRenderer SquareTarget;
    public SpriteRenderer PlaneTarget;
    private FadeEffectGO<SpriteRenderer> fadeMode;
    private FadeEffectGO<SpriteRenderer> fadeInEffect;
    private FadeEffectGO<SpriteRenderer> fadeOutEffect;
    private FadeEffectGO<SpriteRenderer> currentEffect;
    private bool isInit = false;
    private void SetFadeEffect()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;
        fadeMode = new FadeEffectGO<SpriteRenderer>(transform.GetComponent<SpriteRenderer>())
            .SetFadeMode(FadeMode.Once)
            .SetFadeTime(StaticDatas.ColorFadeTime)
            .SetFadeColor(Color.black);
        fadeInEffect = new FadeEffectGO<SpriteRenderer>(PlaneTarget)
            .SetFadeMode(FadeMode.Once)
            .SetFadeTime(StaticDatas.ColorFadeTime)
            .SetFadeColor(new Color(1, 1, 1, 1));
        fadeOutEffect = new FadeEffectGO<SpriteRenderer>(PlaneTarget)
            .SetFadeMode(FadeMode.Once)
            .SetFadeTime(StaticDatas.ColorFadeTime)
            .SetFadeColor(new Color(1, 1, 1, 0));
    }
    private void Start()
    {

    }
    private FadeEffectGO<SpriteRenderer> SetFadeColor(Color color, FadeEffectGO<SpriteRenderer>.PercentageHandler hander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        SetFadeEffect();
        return fadeMode
            .SetFadeColor(color)
            .SetOriginalColor(SquareTarget.color)
            .SetPersentageHander(hander)
            .SetEndHander(endHander);
    }
    public void StartFadeEffect(Color to, FadeEffectGO<SpriteRenderer>.PercentageHandler persentageHander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        SetFadeEffect();
        StartFade(SquareTarget, SetFadeColor(to, persentageHander, endHander));
    }
    public void PlaneFadeIn()
    {
        SetFadeEffect();
        StopCurrentEffect();
        currentEffect = fadeInEffect.Copy(fadeInEffect);
        StartFade(PlaneTarget, currentEffect);
    }
    public void PlaneFadeOut()
    {
        SetFadeEffect();
        StopCurrentEffect();
        currentEffect = fadeOutEffect.Copy(fadeOutEffect);
        StartFade(PlaneTarget, currentEffect);
    }
    private void StopCurrentEffect()
    {
        if (currentEffect != null)
        {
            currentEffect.StartEndHander();
            currentEffect.FinishImmediately();
            currentEffect = null;
        }
    }
}
