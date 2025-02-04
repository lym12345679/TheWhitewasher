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
    private void SetFadeEffect()
    {
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
        SetFadeEffect();
    }
    private FadeEffectGO<SpriteRenderer> SetFadeColor(Color color, FadeEffectGO<SpriteRenderer>.PercentageHandler hander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        return fadeMode
            .SetFadeColor(color)
            .SetOriginalColor(SquareTarget.color)
            .SetPersentageHander(hander)
            .SetEndHander(endHander);
    }
    public void StartFadeEffect(Color to, FadeEffectGO<SpriteRenderer>.PercentageHandler persentageHander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        StartFade(SquareTarget, SetFadeColor(to, persentageHander, endHander));
    }
    public void PlaneFadeIn()
    {
        StopCurrentEffect();
        currentEffect = fadeInEffect.Copy(fadeInEffect);
        StartFade(PlaneTarget, currentEffect);
    }
    public void PlaneFadeOut()
    {
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
