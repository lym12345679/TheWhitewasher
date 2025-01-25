using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PorpSO", menuName = "ScriptableObjects/PorpSO", order = 1)]
public class PorpSO : ScriptableObject
{
    public List<PropUI> PropUI = new List<PropUI>();

    public GameObject GetPropUI(PorpEnum porp)
    {
        return PropUI.Find(x => x.Porp == porp).UIPrefeb;
    }
}
[Serializable]
public class PropUI
{
    public PorpEnum Porp;
    public GameObject UIPrefeb;
}
