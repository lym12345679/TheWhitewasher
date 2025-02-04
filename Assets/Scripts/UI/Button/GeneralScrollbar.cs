using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralScrollbar : MonoBehaviour
{
    public RectTransform Penel;
    public RectTransform Content;
    public Scrollbar TheScrollbar;
    private float penelWidth;
    private Vector3 PenelOriginPos;
    private Vector3 ContentOriginPos;
    private bool isInit = false;
    public void Init()
    {
        penelWidth = Penel.rect.width;
        //Debug.Log(Penel.localPosition + " " + Content.localPosition);
        PenelOriginPos = Penel.transform.position;
        ContentOriginPos = Content.transform.position;
    }
    public void UpdateScrollbar()
    {
        if (!isInit)
        {
            Init();
            isInit = true;
        }
        //Debug.Log("UpdateScrollbar: " + TheScrollbar.value);
        float persentage = TheScrollbar.value;
        float offset = penelWidth * (1 - persentage);
        Penel.transform.position = PenelOriginPos - new Vector3(offset, 0, 0);
        Content.transform.position = ContentOriginPos;
    }

}
