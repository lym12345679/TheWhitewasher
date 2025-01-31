
using UnityEngine;
using UnityEngine.Audio;

namespace MizukiTool.Audio
{
    public class AudioMixerGroupManager
    {
        public static float DBMin = -40;
        public static float DBMax = 0;
        public static float DBRange = 40;
        public static float GetPersentageFromValume(float valume)
        {
            return (valume - DBMin) / DBRange;
        }
        public static float GetAudioMixerGroupValume(AudioMixerGroupEnum audioMixerEnum)
        {
            AudioMixerGroup entry = AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
            if (entry != null)
            {
                entry.audioMixer.GetFloat(audioMixerEnum.ToString(), out float value);
                return GetPersentageFromValume(value);
            }
            return 0;
        }
        public static AudioMixerGroup GetAudioMixerGroup(AudioMixerGroupEnum audioMixerEnum)
        {
            return AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
        }
        public static void SetAudioVolume(AudioMixerGroupEnum audioMixerEnum, float persentage)
        {
            float value = DBMin + DBRange * persentage;
            AudioMixerGroup entry = AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
            if (entry != null)
            {
                entry.audioMixer.SetFloat(audioMixerEnum.ToString(), value);
            }
        }
    }
}