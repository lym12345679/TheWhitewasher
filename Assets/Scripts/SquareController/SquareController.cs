using UnityEngine;
using MizukiTool.AStar;

[RequireComponent(typeof(SpriteRenderer))]
public class SquareController : MonoBehaviour
{
    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer(ColorMod.ToString());
    }
    public PointMod ColorMod;
    private void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
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
    public void ApplyColorMod()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
        this.gameObject.layer = LayerMask.NameToLayer(ColorMod.ToString());
    }
    public void SetColorMod(PointMod colorMod)
    {
        ColorMod = colorMod;
        ApplyColorMod();
    }
}
