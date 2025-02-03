using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShowTest : MonoBehaviour
{
    public TextAsset SelfTextAsset;
    void Start()
    {
        TextShowUI.Open(SelfTextAsset);
    }
}
