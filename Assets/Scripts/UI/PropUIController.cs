using MizukiTool.AStar;
using MizukiTool.UIEffect;
using UnityEngine;
using UnityEngine.UI;

public class PropUIController : UIEffectController<Image>
{
    public PorpEnum Prop;
    public PointMod PointM
    {
        get
        {
            return SOManager.colorSO.GetPointMod(ColorMod);
        }
    }
    public ColorEnum ColorMod;
    public Image UIImage;
    private bool isSelected;
    private bool isScaling = false;
    private bool isScalingBigger = false;
    private ScaleEffect scaleEffectBigger;
    private ScaleEffect scaleEffectBack;
    private ScaleEffect currentScaleBiggerEffect;
    private ScaleEffect currentScaleBackEffect;
    private GameObject currentProp
    {
        get
        {
            return PorpsManager.Instance.CurrentProp;
        }
    }
    void Start()
    {
        scaleEffectBigger = new ScaleEffect()
            .SetDuration(0.3f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(2f, 2f, 2f))
            .SetEndHandler((ScaleEffect s) =>
            {
                isScaling = false;
                isScalingBigger = true;
            });
        scaleEffectBack = new ScaleEffect()
            .SetDuration(0.3f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(1f, 1f, 1f))
            .SetEndHandler((ScaleEffect s) =>
            {
                isScaling = false;
                isScalingBigger = false;
            });
    }
    void Update()
    {
        UpdateEffect();
    }
    void UpdateEffect()
    {
        if (Prop != PorpEnum.Blank)
        {
            if (currentProp != null && currentProp == this.gameObject)
            {
                isSelected = true;
                if (currentScaleBackEffect != null)
                {
                    currentScaleBackEffect.StartEndHandler();
                    currentScaleBackEffect.FinishImmediately();
                    currentScaleBackEffect = null;
                }
                AmplificationSelf();
            }
            else
            {
                isSelected = false;
                if (currentScaleBiggerEffect != null)
                {
                    currentScaleBiggerEffect.StartEndHandler();
                    currentScaleBiggerEffect.FinishImmediately();
                    currentScaleBiggerEffect = null;
                }
                ReduceSelf();
            }
        }
    }
    public void AmplificationSelf()
    {
        if (isScaling || isScalingBigger)
        {
            return;
        }
        Debug.Log("AmplificationSelf:" + "isScaling:" + isScaling + " isScalingBigger:" + isScalingBigger);
        isScaling = true;
        currentScaleBiggerEffect = scaleEffectBigger.Copy(scaleEffectBigger);
        StartScaleEffect(transform, currentScaleBiggerEffect);
    }
    public void ReduceSelf()
    {
        if (isScaling || isSelected || !isScalingBigger)
        {
            return;
        }
        isScaling = true;
        currentScaleBackEffect = scaleEffectBack.Copy(scaleEffectBack);
        StartScaleEffect(transform, currentScaleBackEffect);
    }
    public void SetColorMod(ColorEnum colorMod)
    {
        ColorMod = colorMod;
        SetColor(colorMod);
    }
    public void SetColor(ColorEnum colorMod)
    {
        UIImage.color = SOManager.colorSO.GetColor(colorMod);
    }
    public void OnClicked()
    {
        PorpsManager.Instance.SetCurrentProp(this.gameObject);
    }

}
