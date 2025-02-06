using MizukiTool.AStar;
using MizukiTool.Audio;
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
    private Material OriginMaterial;
    public Material SelectedMaterial;
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
    public AudioEnum SelectPropAudio;
    void Awake()
    {
        scaleEffectBigger = new ScaleEffect()
            .SetDuration(0.2f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(1.75f, 1.75f, 1.75f));
        scaleEffectBack = new ScaleEffect()
            .SetDuration(0.2f)
            .SetEffectMode(ScaleEffectMode.Once)
            .SetEndScale(new Vector3(1f, 1f, 1f));
    }
    void Start()
    {
        OriginMaterial = UIImage.material;
        //SetColor(ColorMod);
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

    }
    public void SetColorToGray()
    {
        UIImage.material = SelectedMaterial;
        SelectedImage.material = SelectedMaterial;
    }
    public void ResetColor()
    {
        UIImage.material = OriginMaterial;
        SelectedImage.material = OriginMaterial;
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
        StartScaleEffect(UIImage.transform, currentScaleBiggerEffect);
    }
    public void ReduceSelf()
    {
        if (currentScaleBackEffect != null)
        {
            currentScaleBackEffect.FinishImmediately();
            currentScaleBackEffect = null;
        }
        if (!isScalingBigger || isSelected)
        {
            return;
        }
        isScalingBigger = false;
        currentScaleBackEffect = scaleEffectBack.Copy(scaleEffectBack);
        StartScaleEffect(UIImage.transform, currentScaleBackEffect);
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

        if (KeyboardSet.IsKeyUp(KeyEnum.Click2) || Prop == PorpEnum.Blank)
        {
            return;
        }
        AudioUtil.Play(SelectPropAudio, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        if (isSelected == false)
        {
            isSelected = true;
            PorpsManager.Instance.SetCurrentProp(this.gameObject);
            PorpsManager.Instance.CurrentPropClearOrChangeAction = () =>
            {
                isSelected = false;
                ReduceSelf();
                ResetColor();
            };
            AmplificateSelf();
            SetColorToGray();
        }
        else
        {
            PorpsManager.Instance.ClearCurrentProp();
        }

    }

}
