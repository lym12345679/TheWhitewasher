
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CGGroupsSO", menuName = "CGGroupsSO")]
public class CGGroupsSO : ScriptableObject
{
    public List<CGMessage> CGList = new List<CGMessage>();
}

public enum CGEnum
{
    Begin,
    Dialog,
    End,
    Test,
    None,
}
