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
    public Image SelectedImage;
    private Color SelectedImageOriginColor;
    private float grayColorFixer = 0.5f;
    private bool isSelected;
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

    void Awake()
    {
        scaleEffectBigger = new ScaleEffect()
            .SetDuration(0.2f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(2f, 2f, 2f));
        scaleEffectBack = new ScaleEffect()
            .SetDuration(0.2f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(1f, 1f, 1f));
        if (SelectedImage != null)
        { SelectedImageOriginColor = SelectedImage.color; }
    }
    void Update()
    {
        UpdateEffect();
    }
    public void OnPointerEnter()
    {
        AmplificateSelf();
    }
    public void OnPointerExit()
    {
        ReduceSelf();
    }
    void UpdateEffect()
    {
        if (Prop != PorpEnum.Blank)
        {
            if (currentProp != null && currentProp == this.gameObject)
            {
                if (isSelected == false)
                {
                    isSelected = true;
                    SetColorToGray();
                }
            }
            else
            {
                isSelected = false;
                ResetColor();
            }
        }
    }
    public void SetColorToGray()
    {
        Color color1 = UIImage.color;
        UIImage.color = new Color(
                Mathf.Max(color1.r - grayColorFixer, 0),
                Mathf.Max(color1.g - grayColorFixer, 0),
                Mathf.Max(color1.b - grayColorFixer, 0), color1.a);
        Color color2 = SelectedImage.color;
        SelectedImage.color = new Color(
                Mathf.Max(color2.r - grayColorFixer, 0),
                Mathf.Max(color2.g - grayColorFixer, 0),
                Mathf.Max(color2.b - grayColorFixer, 0), color2.a);
    }
    public void ResetColor()
    {

        Color color = SOManager.colorSO.GetColor(ColorMod);
        UIImage.color = color;
        SelectedImage.color = SelectedImageOriginColor;
    }
    public void AmplificateSelf()
    {
        if (currentScaleBiggerEffect != null)
        {
            currentScaleBiggerEffect.FinishImmediately();
            currentScaleBiggerEffect = null;
        }
        if (isScalingBigger)
        {
            return;
        }
        isScalingBigger = true;
        currentScaleBiggerEffect = scaleEffectBigger.Copy(scaleEffectBigger);
        StartScaleEffect(this.transform, currentScaleBiggerEffect);
    }
    public void ReduceSelf()
    {
        if (currentScaleBackEffect != null)
        {
            currentScaleBackEffect.FinishImmediately();
            currentScaleBackEffect = null;
        }
        if (!isScalingBigger)
        {
            return;
        }
        isScalingBigger = false;
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
