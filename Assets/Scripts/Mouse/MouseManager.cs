using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool IsMouseClickDown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseClickDown)
        {
            IsMouseClickDown = true;
            OnMouseClickDown();
            IsMouseClickDown = false;
        }
    }

    public void OnMouseClickDown()
    {
        Vector3 position = CheckMousePositionOnWorld();
        Debug.Log("Mouse Position: " + position);
    }

    public Vector3 CheckMousePositionOnWorld()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
