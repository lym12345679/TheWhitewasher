using System.Collections.Generic;
using UnityEngine;
public static class SOManager
{
    public static ColorSO colorSO = Resources.Load<ColorSO>("SO/ColorSO");
    public static PorpSO porpSO = Resources.Load<PorpSO>("SO/PorpSO");
    public static LevelSelectItemMessageSO levelSelectItemMessageSO = Resources.Load<LevelSelectItemMessageSO>("SO/LevelSelectItemMessageSO");
    public static GameCharacterSpriteSO gameCharacterSpriteSO = Resources.Load<GameCharacterSpriteSO>("SO/GameCharacterSpriteSO");
    public static CGShownItemSO cgShownItemSO = Resources.Load<CGShownItemSO>("SO/CGShownItemSO");
    public static SpecialCGSO specialCGSO = Resources.Load<SpecialCGSO>("SO/SpecialCG/SpecialCGSO");
    public static Dictionary<CGEnum, CGGroupsSO> CGGroups = new Dictionary<CGEnum, CGGroupsSO>(){
        {CGEnum.Begin, Resources.Load<CGGroupsSO>("SO/CGSO/BeginCG")},
        {CGEnum.Dialog, Resources.Load<CGGroupsSO>("SO/CGSO/DialogCG")},
        {CGEnum.End, Resources.Load<CGGroupsSO>("SO/CGSO/EndCG")},
        {CGEnum.Test, Resources.Load<CGGroupsSO>("SO/CGSO/TestCG")},
    };
    public static List<CGMessage> GetCGMessageList(CGEnum cgEnum)
    {
        //Debug.Log("GetCGMessageList:" + cgEnum.ToString());
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
