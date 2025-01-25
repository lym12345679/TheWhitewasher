using UnityEngine;
using MizukiTool.AStar;

[RequireComponent(typeof(SpriteRenderer))]
public class SquareController : MonoBehaviour
{

    public PointMod ColorMod;
    private LayerMask colorLayer;
    void FixedUpdate()
    {
        this.gameObject.layer = LayerMask.NameToLayer(ColorMod.ToString());
    }
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

}
