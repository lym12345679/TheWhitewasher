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
    }


    public CGEnum CGEnum;

    void Start()
    {
        CGUI.Open(new CGGroup()
        {
            CGEnum = CGEnum,
            EndHander = () =>
            {
                Debug.Log("CG Over");
            }
        });
        /*SpecialCGUI.Open(new CGGroup()
        {
            CGEnum = CGEnum,
            EndHander = () =>
            {
                Debug.Log("SpecialCG Over");
            }
        });*/
    }
    /*public void StartCG()
    {
        if (cgMessageStack.Count > 0)
        {
            CGGroup cgGroup = new CGGroup()
            {
                CGMessageStack = cgMessageStack,

            };
            cgGroup.SkipHander = StartCG;
            CGUI.Open(cgGroup);
        }
        else
        {
            Debug.Log("CG Over");
        }
    }*/

}
