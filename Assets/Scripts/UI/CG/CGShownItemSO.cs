

using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CGShownItemSO", menuName = "ScriptableObjects/CGShownItemSO")]
public class CGShownItemSO : ScriptableObject
{
    public List<CGShownItem> CGShownItemList = new List<CGShownItem>();
    private void OnValidate()
    {
        foreach (var item in CGShownItemList)
        {
            item.Name = item.ShownItemEnum.ToString();
        }
    }
    public Sprite GetSprite(CGShownItemEnum shownItemEnum)
    {
        foreach (var item in CGShownItemList)
        {
            if (item.ShownItemEnum == shownItemEnum)
            {
                return item.ShownItemSprite;
            }
        }
        return null;
    }
    public Sprite GetPixelSprite(CGShownItemEnum shownItemEnum)
    {
        foreach (var item in CGShownItemList)
        {
            if (item.ShownItemEnum == shownItemEnum)
            {
                return item.PixelSprite;
            }
        }
        return null;
    }
}
[Serializable]
public class CGShownItem
{
    [HideInInspector]
    public string Name;

    public CGShownItemEnum ShownItemEnum;
    public Sprite ShownItemSprite;
    public Sprite PixelSprite;
}
public enum CGShownItemEnum
{
    None,
    银河,
    写生册,
    秃头铅笔,
    油画板,
    玻璃画,
    咖啡黄昏,
    废稿
}
