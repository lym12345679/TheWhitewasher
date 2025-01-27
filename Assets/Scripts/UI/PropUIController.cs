using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
using UnityEngine.UI;

public class PropUIController : MonoBehaviour
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
