using UnityEngine;
public static class SOManager
{
    public static ColorSO colorSO = Resources.Load<ColorSO>("SO/ColorSO");
    public static PorpSO porpSO = Resources.Load<PorpSO>("SO/PorpSO");
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
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

}
