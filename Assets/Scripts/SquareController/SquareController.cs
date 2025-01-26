using UnityEngine;
using MizukiTool.AStar;

[RequireComponent(typeof(SpriteRenderer))]
public class SquareController : MonoBehaviour
{

    public PointMod ColorMod;
    private void OnValidate()
    {
        ColorSO colorSO = Resources.Load<ColorSO>("SO/ColorSO");
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorSO.GetColor(ColorMod);
        this.gameObject.layer = LayerMask.NameToLayer(ColorMod.ToString());
    }

    void OnDrawGizmos()
    {
        Color color = Color.black;
        Gizmos.color = color;
        if (transform.parent)
        {
            SquareParentController squareParentController = transform.parent.GetComponent<SquareParentController>();
            if (squareParentController.ShowGizmos)
            {
                Gizmos.DrawWireCube(transform.position, transform.parent.localScale);
            }
        }
    }

    public void SetColorMod(PointMod colorMod)
    {
        ColorMod = colorMod;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
        //Debug.Log(ColorMod.ToString());
        //Debug.Log("Layer:" + LayerMask.NameToLayer(ColorMod.ToString()));
        this.gameObject.layer = LayerMask.NameToLayer(ColorMod.ToString());
    }
}
