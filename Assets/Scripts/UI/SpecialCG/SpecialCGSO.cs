using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpecialCGSO", menuName = "ScriptableObjects/SpecialCGSO")]
public class SpecialCGSO : ScriptableObject
{
    public List<SpecialCGMessage> SpecialCGList = new List<SpecialCGMessage>();
    void OnValidate()
    {
        for (int i = 0; i < SpecialCGList.Count; i++)
        {
            SpecialCGList[i].Name = SpecialCGList[i].ShownItemEnum.ToString();
        }
    }
    public SpecialCGMessage GetSpecialCGMessageList(CGShownItemEnum shownItemEnum)
    {
        return SpecialCGList.Find(x => x.ShownItemEnum == shownItemEnum);
    }
}
