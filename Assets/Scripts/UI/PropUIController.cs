using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
using UnityEngine.UI;

public class PropUIController : MonoBehaviour
{
    public PorpEnum Prop;
    public PointMod ColorMod;
    public Image UIImage;
    public void SetColorMod(PointMod colorMod)
    {
        ColorMod = colorMod;
        SetColor(colorMod);
    }
    public void SetColor(PointMod colorMod)
    {
        UIImage.color = SOManager.colorSO.GetColor(colorMod);
    }
    public void OnClicked()
    {
        PorpsManager.Instance.SetCurrentProp(this.gameObject);
    }
}
