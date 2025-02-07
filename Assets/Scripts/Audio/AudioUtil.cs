using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MizukiTool.Audio
{
    public static class AudioUtil
    {
        public static void Play(AudioEnum audioEnum, AudioMixerGroupEnum audioMixerGroupEnum, AudioPlayMod audioPlayMod, Action<AudioSource> endEventHander = null, Action<AudioSource> updateEventHander = null)
        {
            AudioManager.EnsureInstance();
            AudioManager.Instance.Play(audioEnum, audioMixerGroupEnum, audioPlayMod, endEventHander, updateEventHander);
        }
        public static void SetAudioVolume(AudioMixerGroupEnum audioMixerEnum, float volume)
        {
            AudioMixerGroup entry = AudioSOManager.audioMixerGroupSO.GetAudioMixerGroup(audioMixerEnum);
            if (entry != null)
            {
                entry.audioMixer.SetFloat(audioMixerEnum.ToString(), volume);
            }
        }

        public static void PauseAllLoopAudio()
        {
            AudioManager.EnsureInstance();
            AudioManager.Instance.PauseAllLoopAudio();
        }
        public static void ContinueAllLoopAudio()
        {
            AudioManager.EnsureInstance();
            AudioManager.Instance.ContinueAllLoopAudio();
        }
        public static void ReturnAllLoopAudio()
        {
            AudioManager.EnsureInstance();
            AudioManager.Instance.ReturnAllLoopAudio();
        }
        public static bool CheckEnumInLoopAudio(AudioEnum audioEnum)
        {
            AudioManager.EnsureInstance();
            return AudioManager.Instance.CheckEnumInLoopAudio(audioEnum);
        }
    }
}
