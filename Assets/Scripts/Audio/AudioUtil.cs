using System;
using UnityEngine;

namespace MizukiTool.Audio
{
    public static class AudioUtil
    {
        public static void Play(AudioEnum audioEnum, AudioMixerGroupEnum audioMixerGroupEnum, AudioPlayMod audioPlayMod, Action<AudioSource> endEventHander = null, Action<AudioSource> updateEventHander = null)
        {
            AudioManager.EnsureInstance();
            AudioManager.Instance.Play(audioEnum, audioMixerGroupEnum, audioPlayMod, endEventHander, updateEventHander);
        }
    }
}
