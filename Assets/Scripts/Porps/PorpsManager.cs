using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
using UnityEngine.Events;
public class PorpsManager : MonoBehaviour
{
    public static PorpsManager Instance;
    public List<PropClass> PropList = new List<PropClass>();
    public GameObject CurrentProp;
    private GameObject Player;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player is null");
        }
    }
    public void AddProp(PropClass prop)
    {
        PropList.Add(prop);
        PropsUI.Instance.AddProp(prop);
    }
    public void RemoveProp(PropClass prop)
    {
        PropList.Remove(prop);
    }
    public void SetCurrentProp(GameObject prop)
    {
        //Debug.Log("SetCurrentProp:" + prop.name);
        CurrentProp = prop;
    }
    public void ClearCurrentProp()
    {
        CurrentProp = null;
    }
    public bool UseProp(Vector3 position)
    {
        bool isUse = false;
        if (CurrentProp == null)
        {
            return false;
        }
        PropUIController propUIController = CurrentProp.GetComponent<PropUIController>();
        Point point = AstarManagerSon.Instance.GetPointOnMap(position);
        if (point != null)
        {
            Debug.Log("Clicked Point:" + point.X + "," + point.Y);
            switch (propUIController.Prop)
            {
                case PorpEnum.PaintBrushWasher:
                    {
                        PointMod[] pointMods = new PointMod[1] { point.Mod };
                        UsePaintBrushWasher(point, pointMods);
                        DestroyCurrentProp();
                        isUse = true;
                    }
                    break;
                case PorpEnum.Stainer:
                    {
                        UseStainer();
                        DestroyCurrentProp();
                        isUse = true;
                    }
                    break;
                default:
                    break;
            }
        }
        return isUse;
    }

    public void DestroyCurrentProp()
    {
        Destroy(CurrentProp);
        PropsUI.Instance.OnPropDeserve();
    }
    public void UsePaintBrushWasher(Point point, PointMod[] pointMods)
    {
        //Debug.Log("Use PaintBrushWasher");
        //AstarManagerSon.Instance.UpdateAllAstarPonitInCloseList(AstarManager.Instance.map, position, pointMods, UpdatePoint);
        Debug.Log(point.GameObject.transform.position + " (" + point.X + " ," + point.Y + ")");
        SquareController squareController = point.GameObject.GetComponent<SquareController>();
        squareController.StartBrush(point, squareController.ColorMod, CurrentProp.GetComponent<PropUIController>().ColorMod);
    }
    public void UpdatePoint(Point point)
    {
        point.Mod = CurrentProp.GetComponent<PropUIController>().PointM;
        //Debug.Log("UpdatePoint:" + point.X + "," + point.Y);
        SquareController squareController;
        PropUIController propUIController = CurrentProp.GetComponent<PropUIController>();
        if (point.GameObject.TryGetComponent<SquareController>(out squareController))
        {
            squareController.SetColorMod(propUIController.ColorMod);
        }
    }
    public void UseStainer()
    {
        Player.GetComponent<PlayerController>().SetColorMod(CurrentProp.GetComponent<PropUIController>().ColorMod);
    }
}
public class PropClass
{
    public PorpEnum Porp;
    public ColorEnum ColorMod;
    public PropClass(PorpEnum porp, ColorEnum colorMod)
    {
        Porp = porp;
        ColorMod = colorMod;
    }
}