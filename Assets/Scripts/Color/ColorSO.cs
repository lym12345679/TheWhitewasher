using UnityEngine;
using MizukiTool.AStar;
using System.Collections.Generic;
using System;
[CreateAssetMenu(fileName = "ColorSO", menuName = "MizukiSO/ColorSO")]

public class ColorSO : ScriptableObject
{

    public List<PointModWithColor> pointModWithColors = new List<PointModWithColor>();
    /*public string Search;
    public List<PointModWithColor> SearchResult = new List<PointModWithColor>();*/
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
    public PointMod GetPointMod(ColorEnum color)
    {
        foreach (var item in pointModWithColors)
        {
            if (item.eColor == color)
            {
                return item.pointMod;
            }
        }
        return PointMod.None;
    }
    private void OnValidate()
    {
        //SearchResult.Clear();
        foreach (var item in pointModWithColors)
        {
            item.Name = item.eColor.ToString() + "_" + item.pointMod.ToString();
        }
        /*if (string.IsNullOrEmpty(Search))
        {
            SearchResult.AddRange(pointModWithColors);
        }
        else
        {
            foreach (var item in pointModWithColors)
            {
                if (item.eColor.ToString().Contains(Search))
                {
                    SearchResult.Add(item);
                }
            }
        }*/
    }
}
[Serializable]
public class PointModWithColor
{
    [HideInInspector]
    public string Name;
    public ColorEnum eColor;
    public Color color;
    public PointMod pointMod;
}

