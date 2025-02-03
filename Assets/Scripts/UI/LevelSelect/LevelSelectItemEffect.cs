
using System;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItemEffect : UIEffectController<Image>
{
    private PositionEffect positionUpEffect;
    private PositionEffect positionDownEffect;
    private PositionEffect currentEffect;
    public RectTransform TinyPeople;
    private Vector3 originPosition;
    private bool isSetOriginPosition = false;
    public void SetOriginPosition()
    {
        this.originPosition = TinyPeople.transform.position;
        positionDownEffect = new PositionEffect()
            .SetDuration(1f)
            .SetEndPosition(originPosition);
        positionUpEffect = new PositionEffect()
            .SetDuration(1f)
            .SetEndPosition(originPosition + new Vector3(0, 50, 0));
    }
    public void StartPositionUpEffect(Action<PositionEffect> endHandle)
    {
        if (!isSetOriginPosition)
        {
            SetOriginPosition();
            isSetOriginPosition = true;
        }
        if (currentEffect != null)
        {
            currentEffect.StartEndHander();
            currentEffect.FinishImmediately();
        }
        currentEffect = positionUpEffect
            .SetEndHandler(endHandle)
            .Copy(positionUpEffect);
        StartPositionEffect(TinyPeople, currentEffect);
    }
    public void StartPositionDownEffect(Action<PositionEffect> endHandle)
    {
        if (!isSetOriginPosition)
        {
            SetOriginPosition();
            isSetOriginPosition = true;
        }
        if (currentEffect != null)
        {
            currentEffect.StartEndHander();
            currentEffect.FinishImmediately();
        }
        currentEffect = positionDownEffect
            .SetEndHandler(endHandle)
            .Copy(positionDownEffect);
        StartPositionEffect(TinyPeople, currentEffect);
    }

}
