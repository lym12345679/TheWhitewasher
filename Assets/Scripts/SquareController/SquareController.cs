using UnityEngine;
using MizukiTool.AStar;
using MizukiTool.UIEffect;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class SquareController : MonoBehaviour
{
    public bool isFading = false;
    public bool isAskNeighbour = false;
    public SpriteRenderer FadeTarget;
    public SquareEffect selfSquareEffect;
    public SquareCorrectPositionMod SquareCorrectPositionMod = SquareCorrectPositionMod.Used;
    void Start()
    {

        this.gameObject.layer = LayerMask.NameToLayer(MPoint.ToString());
    }
    public PointMod MPoint
    {
        get
        {
            return SOManager.colorSO.GetPointMod(ColorMod);
        }
    }
    public ColorEnum ColorMod;
    private void OnValidate()
    {
        FadeTarget.color = SOManager.colorSO.GetColor(ColorMod);
        if (SquareCorrectPositionMod == SquareCorrectPositionMod.Start)
        {
            CorrectPosition();
            SquareCorrectPositionMod = SquareCorrectPositionMod.Used;
        }
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
        FadeTarget.color = SOManager.colorSO.GetColor(ColorMod);
        ChanngeLayer();
    }

    public void SetColorMod(ColorEnum colorMod)
    {
        ColorMod = colorMod;
        ApplyColorMod();
    }

    public void ChanngeLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer(MPoint.ToString());
    }


    public void StartBrush(Point point, ColorEnum from, ColorEnum to)
    {
        ChangeSelfColor(point, from, to);
    }
    //改变邻居的颜色
    public bool ChangeNeighbourColor(Point asker, ColorEnum from, ColorEnum to)
    {
        //Debug.Log("ChangeNeighbourColor:" + asker.X + "," + asker.Y);
        List<Point> points = new List<Point>();
        points = AstarManagerSon.Instance.GetNeighbourPoints(this.transform.position);
        foreach (var point in points)
        {
            if (point.GameObject != null && point != asker)
            {
                SquareController squareController = point.GameObject.GetComponent<SquareController>();
                if (squareController.GetIsFading())
                {
                    continue;
                }
                if (squareController.ChangeSelfColor(point, from, to))
                {
                    //Debug.Log("ChangeNeighbourColor");
                }
            }
        }
        if (from == to)
        {
            return false;
        }
        from = to;
        return true;
    }
    //改变自己的颜色
    public bool ChangeSelfColor(Point point, ColorEnum from, ColorEnum to)
    {
        if (ColorMod == from)
        {
            ColorMod = to;
            SetIsFading(true);
            isAskNeighbour = false;
            Debug.Log("ChangeSelfColor:" + point.X + "," + point.Y);
            selfSquareEffect.StartFadeEffect(SOManager.colorSO.GetColor(to),
                (float t) =>
                {
                    // 1/FadeSpradeSpeed%的时候询问邻居
                    if (t > (1 / StaticDatas.FadeSpradeSpeed) && !isAskNeighbour)
                    {
                        //Debug.Log("ChangeNeighbourColor");
                        this.isAskNeighbour = true;
                        this.ChangeNeighbourColor(point, from, to);
                    }
                    return t;
                },
                (FadeEffectGO<SpriteRenderer> fadeEffect) =>
                {
                    SetIsFading(false);
                });
            ChanngeLayer();
            return true;
        }
        return false;
    }
    public void SetIsFading(bool b)
    {
        isFading = b;
    }
    public bool GetIsFading()
    {
        return isFading;
    }
    public void CorrectPosition()
    {
        if (Mathf.Abs(transform.localPosition.x - (int)transform.localPosition.x) > 0.01)
        {
            Debug.Log("Roundx");
            transform.localPosition = new Vector3((int)(transform.localPosition.x), (int)transform.localPosition.y, transform.localPosition.z);
        }
        if (Mathf.Abs(transform.localPosition.y - (int)transform.localPosition.y) > 0.01)
        {
            Debug.Log("Roundy");
            transform.localPosition = new Vector3((int)transform.localPosition.x, (int)(transform.localPosition.y), transform.localPosition.z);
        }
    }
}

public enum SquareCorrectPositionMod
{
    Used,
    Start,

}
