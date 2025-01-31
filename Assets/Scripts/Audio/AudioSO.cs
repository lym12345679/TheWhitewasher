using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MizukiTool.Audio
{
    [CreateAssetMenu(fileName = "AudioSO", menuName = "MizukiSO/AudioSO")]
    public class AudioSO : ScriptableObject
    {
        public List<AudioClass> audioList = new List<AudioClass>();
        private void OnValidate()
        {
            foreach (var item in audioList)
            {
                item.Name = item.audioEnum.ToString();
            }
        }
        public AudioClip GetAudioClip(AudioEnum audioEnum)
        {
            foreach (var item in audioList)
            {
                if (item.audioEnum == audioEnum)
                {
                    return item.audioClip;
                }
            }
            return null;
        }
    }
    [Serializable]
    public class AudioClass
    {
        [HideInInspector]
        public string Name;
        public AudioEnum audioEnum;
        public AudioClip audioClip;
    }
    public enum AudioEnum
    {
        BGM_Arknight_Babel1,
        BGM_Arknight_Babel2,
        Test_BGM1,
        Test_BGM2,
        Test_BGM3,
        Test_BGM4,
        Test_BGM5,
        Button_Clicked,
        Game_Fail,
    }
}
