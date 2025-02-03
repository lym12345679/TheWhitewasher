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
        BGM_Test1,
        BGM_Test2,
        BGM_Test3,
        BGM_Test4,
        BGM_Test5,
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
        SE_Prop_Cancel

    }
}