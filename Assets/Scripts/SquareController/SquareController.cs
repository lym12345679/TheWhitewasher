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
    public SpriteRenderer SquareTarget2;
    public SquareEffect selfSquareEffect;
    public SquareCorrectPositionMod SquareCorrectPositionMod = SquareCorrectPositionMod.Used;
    public ColorEnum ColorMod;
    public PointMod MPoint
    {
        get
        {
            return SOManager.colorSO.GetPointMod(ColorMod);
        }
    }

    void Awake()
    {
        FadeTarget = GetComponent<SpriteRenderer>();
        selfSquareEffect = GetComponent<SquareEffect>();
    }
    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer(MPoint.ToString());
        SetTPFDMod(AstarManagerSon.IsTPFDUsed);
    }


    #region OnValidate
    [Header("拼写设置颜色")]
    public string ColorEnumSearch;
    private void OnValidate()
    {
        CheckColorEnumSearch();
        Color color = SOManager.colorSO.GetColor(ColorMod);
        FadeTarget.color = color;
        SquareTarget2.color = new Color(color.r, color.g, color.b, 0);
        if (SquareCorrectPositionMod == SquareCorrectPositionMod.Start)
        {
            CorrectPosition();
            SquareCorrectPositionMod = SquareCorrectPositionMod.Used;
        }
        this.gameObject.name = "Go_Plane_" + transform.localPosition.x + "_" + transform.localPosition.y;

    }
    private void CheckColorEnumSearch()
    {
        if (ColorEnumSearch.Length > 2)
        {
            ColorEnum colorEnum;
            if (System.Enum.TryParse(ColorEnumSearch, out colorEnum))
            {
                ColorMod = colorEnum;
                ColorEnumSearch = "";
            }


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
    #endregion
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
        List<Point> points = AstarManagerSon.Instance.GetNeighbourPoints(this.transform.position); ;

        foreach (var point in points)
        {
            if (point.GameObject != null && point != asker)
            {
                SquareController squareController = point.GetMainCompoment<SquareController>();
                if (squareController.GetIsFading())
                {
                    CheckNeighbourPoint();
                    continue;
                }
                if (squareController.ChangeSelfColor(point, from, to))
                {
                    //Debug.Log("ChangeNeighbourColor");
                }
                CheckNeighbourPoint();
            }
        }
        if (from == to)
        {
            return false;
        }
        return true;
    }
    //改变自己的颜色
    public bool ChangeSelfColor(Point point, ColorEnum from, ColorEnum to)
    {
        if (ColorMod == from)
        {
            Point p = AstarManagerSon.Instance.GetPointOnMap(transform.position);

            ColorMod = to;
            p.Mod = MPoint;
            SetIsFading(true);
            isAskNeighbour = false;
            ChanngeLayer();

            //Debug.Log("ChangeSelfColor:" + point.X + "," + point.Y);
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

            return true;
        }
        else
        {
            CheckNeighbourPoint();
        }

        return false;
    }
    public void CheckNeighbourPoint()
    {
        Point upPoint = AstarManagerSon.Instance.GetPointOnMap(transform.position + new Vector3(0, 1, 0));
        if (ColorMod != PlayerController.Instance.ColorMod)
        {
            selfSquareEffect.PlaneFadeOut();
            return;
        }
        if (upPoint == null)
        {
            return;
        }
        if (upPoint.GameObject == null)
        {
            return;
        }
        if (ColorMod != upPoint.GetMainCompoment<SquareController>().ColorMod)
        {
            selfSquareEffect.PlaneFadeIn();
        }
        else
        {
            selfSquareEffect.PlaneFadeOut();
        }
    }
    public void SetIsFading(bool b)
    {
        isFading = b;
    }
    public bool GetIsFading()
    {
        return isFading;
    }
    public void SetTPFDMod(bool b)
    {
        SquareTarget2.gameObject.SetActive(b);
    }
}

public enum SquareCorrectPositionMod
{
    Used,
    Start,

}
