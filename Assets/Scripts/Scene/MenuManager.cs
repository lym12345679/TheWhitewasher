using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenuUI.Open("1");
        SaveSystem.LoadData();
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.Out));
    }

}
