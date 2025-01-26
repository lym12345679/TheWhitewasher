using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;

public class PropsUI : MonoBehaviour
{
    public RectTransform Content;
    public float ContentCellWidth = 100;
    void Start()
    {
        UpdateContentWidth();
        AddProp(new PropClass(PorpEnum.PaintBrushWasher, PointMod.None));
        AddProp(new PropClass(PorpEnum.Stainer, PointMod.None));
        ClearProp();
    }
    public void UpdateContentWidth()
    {
        Content.sizeDelta = new Vector2(ContentCellWidth * Content.childCount, Content.sizeDelta.y);
    }
    public void AddProp(PropClass propClass)
    {
        GameObject propUI = Instantiate(SOManager.porpSO.GetPropUI(propClass.Porp), Content);
        propUI.GetComponent<PropUIController>().SetColorMod(propClass.ColorMod);
        UpdateContentWidth();
    }
    public void ClearProp()
    {
        RectTransform[] children = Content.GetComponentsInChildren<RectTransform>();
        foreach (var child in children)
        {
            if (child != Content)
            {
                Destroy(child.gameObject);
            }
        }
        UpdateContentWidth();
    }
}
