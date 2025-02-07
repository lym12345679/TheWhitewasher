using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static bool isMouse0ClickDown = false;
    public static bool isMouse1ClickDown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMouse0ClickDown)
        {
            isMouse0ClickDown = true;
            OnMouse0ClickDown();
            isMouse0ClickDown = false;
        }
        if (Input.GetMouseButtonDown(1) && !isMouse1ClickDown)
        {
            isMouse1ClickDown = true;
            OnMouse1ClickDown();
            isMouse1ClickDown = false;
        }
    }

    public void OnMouse0ClickDown()
    {
        Vector3 mousePosition = Input.mousePosition;
        //Debug.Log("Mouse Position: " + mousePosition);
        if (mousePosition.y < 360)
        {
            return;
        }
        Vector3 position = CheckMousePositionOnWorld();
        //Debug.Log("Mouse Position: " + position);
        if (!PorpsManager.Instance.UseProp(position))
        {
            //Debug.Log("使用失败");
        }
    }
    public void OnMouse1ClickDown()
    {
        PorpsManager.Instance.ClearCurrentProp();
    }

    public Vector3 CheckMousePositionOnWorld()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
