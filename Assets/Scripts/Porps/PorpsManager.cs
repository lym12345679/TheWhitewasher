using System.Collections.Generic;
using MizukiTool.AStar;
using UnityEngine;
using UnityEngine.Events;
public class PorpsManager : MonoBehaviour
{
    public static PorpsManager Instance;
    public UnityEvent<PropClass> OnAddProp;
    public List<PropClass> PropList = new List<PropClass>();
    public PropClass CurrentProp;
    void Awake()
    {
        Instance = this;
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
    public void SetCurrentProp(PropClass prop)
    {
        Debug.Log("SetCurrentProp:" + prop.Porp.ToString());
        CurrentProp = prop;
    }
    public void ClearCurrentProp()
    {
        CurrentProp = null;
    }
}
public class PropClass
{
    public PorpEnum Porp;
    public PointMod ColorMod;
}