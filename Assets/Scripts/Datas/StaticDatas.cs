public static class StaticDatas
{
    public static SceneEnum CurrentScene;
    public static int TopUnlockedLevel = 3;
    public static bool IsPlayerFinishAllLevel = false;
    public static float BGMMusicVolume = 0.5f;
    public static float SoundEffectVolume = 0.5f;
    /// <summary>
    /// 单个方块的染色时间
    /// </summary> 
    public static float ColorFadeTime = 1f;
    /// <summary>
    /// 染色的传播速率 例如2f表示在ColorFadeTime内颜色会向外传播2个方块
    /// </summary>
    public static float FadeSpradeSpeed = 6f;
}
