using UnityEngine;

namespace MizukiTool.Audio
{
    public static class AudioSOManager
    {
        public static AudioSO audioSO = Resources.Load<AudioSO>("SO/AudioSO");
        public static AudioMixerGroupSO audioMixerGroupSO = Resources.Load<AudioMixerGroupSO>("SO/AudioMixerSO");
    }
    public enum AudioEnum
    {
        BGM_Arknight_Babel1,
        BGM_Arknight_Babel2,
        BGM_Menu_Star,
        BGM_Menu_End,
        BGM_Level1,
        BGM_Level2,
        BGM_CG_End,
        SE_Button_Clicked,
        SE_Button_Hover,
        SE_Player_Fail,
        SE_Player_Jump,
        SE_Player_Land,
        SE_Word_Shown,
        Se_Square_Paint,
        SE_Prop_Get,
        SE_Prop_Use1,
        SE_Prop_Use2,
        SE_Prop_FailUse,
        SE_Prop_Cancel,
        SE_Prop_Select,
        SE_LevelSelect_Button_Clicked,
        SE_SceneChange,
        SE_Player_Win,
    }
}