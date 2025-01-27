using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;

public class PaintBrushWasherController : MonoBehaviour
{
    public PointMod PointM
    {
        get
        {
            return SOManager.colorSO.GetPointMod(ColorMod);
        }
    }
    public ColorEnum ColorMod;
    public PorpEnum Porp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Player Enter");
            PorpsManager.Instance.AddProp(new PropClass(this.Porp, this.ColorMod));
            Destroy(this.gameObject);
        }

    }
    void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
    }
}
