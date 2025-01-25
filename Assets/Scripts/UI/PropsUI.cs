using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsUI : MonoBehaviour
{
    public RectTransform Content;
    public float ContentCellWidth = 100;
    void Start()
    {
        UpdateContentWidth();
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
}
