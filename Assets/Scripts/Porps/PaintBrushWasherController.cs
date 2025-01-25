using System.Collections;
using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;

public class PaintBrushWasherController : MonoBehaviour
{
    public PointMod ColorMod;
    public PorpEnum Porp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player Enter");
        }
        PorpsManager.Instance.AddProp(new PropClass() { Porp = Porp, ColorMod = ColorMod });
        Destroy(this.gameObject);
    }
    void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
    }
}
