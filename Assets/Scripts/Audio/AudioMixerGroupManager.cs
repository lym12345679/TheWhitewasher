using UnityEngine.Audio;

namespace MizukiTool.Audio
{
    public class AudioMixerGroupManager
    {
        public static float DBMin = -80;
        public static float DBMax = 0;
        public static float DBRange = 80;
        public static AudioMixerGroup GetAudioMixerGroup(AudioMixerGroupEnum audioMixerEnum)
        {
            return AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
        }
        public static void SetAudioVolume(AudioMixerGroupEnum audioMixerEnum, float volume)
        {
            AudioMixerGroup entry = AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
            if (entry != null)
            {
                entry.audioMixer.SetFloat("Vol_" + audioMixerEnum.ToString(), volume);
            }
        }
    }
}