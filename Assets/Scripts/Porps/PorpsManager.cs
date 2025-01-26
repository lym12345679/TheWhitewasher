using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
using UnityEngine.Events;
public class PorpsManager : MonoBehaviour
{
    public static PorpsManager Instance;
    public UnityEvent<PropClass> OnAddProp;
    public List<PropClass> PropList = new List<PropClass>();
    public GameObject CurrentProp;
    public PropsUI PropsUIScript;
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
        OnAddProp.Invoke(prop);
    }
    public void RemoveProp(PropClass prop)
    {
        PropList.Remove(prop);
    }
    public void SetCurrentProp(GameObject prop)
    {
        Debug.Log("SetCurrentProp:" + prop.name);
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
        Point point = AstarManagerSon.Instance.map.GetPointOnMap(position);
        if (point != null)
        {
            Debug.Log("Clicked Point:" + point.X + "," + point.Y);
            switch (propUIController.Prop)
            {
                case PorpEnum.PaintBrushWasher:
                    {

                        PointMod[] pointMods = new PointMod[1] { point.Mod };
                        UsePaintBrushWasher(position, pointMods);
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
        PropsUIScript.UpdateContentWidth();
    }
    public void UsePaintBrushWasher(Vector3 position, PointMod[] pointMods)
    {
        //Debug.Log("Use PaintBrushWasher");

        AstarManagerSon.Instance.UpdateAllAstarPonitInCloseList(AstarManager.Instance.map, position, pointMods, UpdatePoint);
    }
    public void UpdatePoint(Point point)
    {
        point.Mod = CurrentProp.GetComponent<PropUIController>().ColorMod;
        //Debug.Log("UpdatePoint:" + point.X + "," + point.Y);
        SquareController squareController;
        PropUIController propUIController = CurrentProp.GetComponent<PropUIController>();
        if (point.gameObject.TryGetComponent<SquareController>(out squareController))
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
    public PointMod ColorMod;
    public PropClass(PorpEnum porp, PointMod colorMod)
    {
        Porp = porp;
        ColorMod = colorMod;
    }
}