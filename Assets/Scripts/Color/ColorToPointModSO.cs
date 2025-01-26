using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
[CreateAssetMenu(fileName = "ColorToPointModSO", menuName = "MizukiSO/ColorToPointModSO")]
public class ColorToPointModSO : ScriptableObject
{
    public List<ColorToPointMod> ColorToPointMods = new List<ColorToPointMod>();
    public PointMod GetPointMod(ColorEnum color)
    {
        foreach (var item in ColorToPointMods)
        {
            if (item.EColor == color)
            {
                return item.PointMod;
            }
        }
        return 0;
    }
}
[System.Serializable]
public class ColorToPointMod
{
    public ColorEnum EColor;
    public PointMod PointMod;
}
