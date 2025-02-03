using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGManager : MonoBehaviour
{
    [HideInInspector]
    public CGManager Instance;
    void Awake()
    {
        Instance = this;
        LoadCGMessage();
    }

    public List<CGMessage> CGMessageList = new List<CGMessage>();
    private Stack<CGMessage> cgMessageStack = new Stack<CGMessage>();

    void Start()
    {
        StartCG();
    }
    public void StartCG()
    {
        if (cgMessageStack.Count > 0)
        {
            CGMessage cgMessage = cgMessageStack.Pop();
            CGUI.Open(cgMessage);
        }
        else
        {
            Debug.Log("CG Over");
        }
    }
    public void LoadCGMessage()
    {
        cgMessageStack.Clear();
        for (int i = CGMessageList.Count - 1; i >= 0; i--)
        {
            CGMessageList[i].endHander = StartCG;
            cgMessageStack.Push(CGMessageList[i]);
        }
    }
}
