using System;
using MizukiTool.UIEffect;
using UnityEngine;

public class SquareEffect : GoEffectController<SpriteRenderer>
{
    public SpriteRenderer FadeTarget;
    private FadeEffectGO<SpriteRenderer> fadeMode;
    private void SetFadeEffect()
    {
        fadeMode = new FadeEffectGO<SpriteRenderer>(transform.GetComponent<SpriteRenderer>())
            .SetFadeMode(FadeMode.Once)
            .SetFadeTime(1f)
            .SetFadeColor(Color.black);
    }
    private void Start()
    {
        SetFadeEffect();
    }
    private FadeEffectGO<SpriteRenderer> SetFadeColor(Color color, FadeEffectGO<SpriteRenderer>.PercentageHandler hander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        return fadeMode
            .SetFadeColor(color)
            .SetOriginalColor(FadeTarget.color)
            .SetPersentageHander(hander)
            .SetEndHander(endHander);
    }
    public void StartFadeEffect(Color to, FadeEffectGO<SpriteRenderer>.PercentageHandler persentageHander, Action<FadeEffectGO<SpriteRenderer>> endHander)
    {
        StartFade(FadeTarget, SetFadeColor(to, persentageHander, endHander));
    }
}
