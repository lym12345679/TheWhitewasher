using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.SaveData();
        SaveSystem.LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
