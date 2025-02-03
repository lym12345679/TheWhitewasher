using System.Collections.Generic;
using UnityEngine;
public static class SOManager
{
    public static ColorSO colorSO = Resources.Load<ColorSO>("SO/ColorSO");
    public static PorpSO porpSO = Resources.Load<PorpSO>("SO/PorpSO");
    public static LevelSelectItemMessageSO levelSelectItemMessageSO = Resources.Load<LevelSelectItemMessageSO>("SO/LevelSelectItemMessageSO");
    public static Dictionary<CGEnum, CGGroupsSO> CGGroups = new Dictionary<CGEnum, CGGroupsSO>(){
        {CGEnum.Begin, Resources.Load<CGGroupsSO>("SO/CGSO/BeginCG")},
        {CGEnum.Dialoge, Resources.Load<CGGroupsSO>("SO/CGSO/EndCG")},
        {CGEnum.End, Resources.Load<CGGroupsSO>("SO/CGSO/DialogCG")},
        {CGEnum.Test, Resources.Load<CGGroupsSO>("SO/CGSO/TestCG")},
    };
    public static List<CGMessage> GetCGMessageList(CGEnum cgEnum)
    {
        return CGGroups[cgEnum].CGList;
    }
    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnLoad()
    {
        Debug.Log("SOManager InitializeOnLoad");
        colorSO = Resources.Load<ColorSO>("SO/ColorSO");
        porpSO = Resources.Load<PorpSO>("SO/PorpSO");
        foreach (var porp in porpSO.PropUI)
        {
            GameObject prefab = porp.UIPrefeb;
        }
    }
*/
}
