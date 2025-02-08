using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MizukiTool.Box
{
    public enum BoxEnum
    {
        MessageBox,
        PauseUI,
        SettingUI,
        LevelSelectUI,
        MenuUI,
        LevelSceneUI,
        SceneChangeUI,
        TextShowUI,
        CGUI,
        SpecialCGUI
    }
    public static class BoxDict
    {
        //用字典存储所有的UIEnum和类型
        public static Dictionary<System.Type, BoxEnum> boxTypeDic = new Dictionary<System.Type, BoxEnum>
        {
            {typeof(MessageBox),BoxEnum.MessageBox},
            {typeof(PauseUI),BoxEnum.PauseUI},
            {typeof(SettingUI),BoxEnum.SettingUI},
            {typeof(LevelSelectUI),BoxEnum.LevelSelectUI},
            {typeof(MenuUI),BoxEnum.MenuUI},
            {typeof(LevelSceneUI),BoxEnum.LevelSceneUI},
            {typeof(SceneChangeUI),BoxEnum.SceneChangeUI},
            {typeof(TextShowUI),BoxEnum.TextShowUI},
            {typeof(CGUI),BoxEnum.CGUI},
            {typeof(SpecialCGUI),BoxEnum.SpecialCGUI}
        };
        //用字典存储所有的UI预制体路径
        public static Dictionary<BoxEnum, string> BoxPathDic = new Dictionary<BoxEnum, string>{
            { BoxEnum.MessageBox, "Prefeb/UIPrefeb/MessageBox" },
            { BoxEnum.PauseUI, "Prefeb/UIPrefeb/PauseUI" },
            { BoxEnum.SettingUI, "Prefeb/UIPrefeb/SettingUI" },
            { BoxEnum.LevelSelectUI, "Prefeb/UIPrefeb/LevelSelectUI" },
            { BoxEnum.MenuUI, "Prefeb/UIPrefeb/MenuUI" },
            { BoxEnum.LevelSceneUI, "Prefeb/UIPrefeb/LevelScene/LevelSceneUI" },
            { BoxEnum.SceneChangeUI, "Prefeb/UIPrefeb/SceneChangeUI" },
            { BoxEnum.TextShowUI, "Prefeb/UIPrefeb/TextShow/TextShowUI" },
            { BoxEnum.CGUI, "Prefeb/UIPrefeb/TextShow/CGUI" },
            { BoxEnum.SpecialCGUI, "Prefeb/UIPrefeb/TextShow/SpecialCGUI" }
        };
    }
}

