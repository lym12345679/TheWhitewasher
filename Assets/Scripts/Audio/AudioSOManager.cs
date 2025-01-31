using UnityEngine;

namespace MizukiTool.Audio
{
    public static class AudioSOManager
    {
        public static AudioSO audioSO = Resources.Load<AudioSO>("SO/AudioSO");
        public static AudioMixerGroupSO audioMixerGroupSO = Resources.Load<AudioMixerGroupSO>("SO/AudioMixerSO");
    }
}