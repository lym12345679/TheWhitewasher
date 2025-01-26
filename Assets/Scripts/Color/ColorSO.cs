using UnityEngine;
using MizukiTool.AStar;
using System.Collections.Generic;
using System;
[CreateAssetMenu(fileName = "ColorSO", menuName = "MizukiSO/ColorSO")]

public class ColorSO : ScriptableObject
{
    public List<PointModWithColor> pointModWithColors = new List<PointModWithColor>();
    public Color GetColor(ColorEnum color)
    {
        foreach (var item in pointModWithColors)
        {
            if (item.eColor == color)
            {
                return item.color;
            }
        }
        return Color.white;
    }
}
[Serializable]
public class PointModWithColor
{
    public ColorEnum eColor;
    public Color color;
}

