using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace MizukiTool.Audio
{
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// 单体实例
        /// </summary>
        public static AudioManager Instance;
        /// <summary>
        /// 使用的混音器
        /// </summary>
        public AudioMixer mAudioMixer;
        /// <summary>
        /// 外部提供的资源加载接口
        /// </summary>
        private Func<string, AudioClip> mAudioClipLoader;
        /// <summary>
        /// 音效组管理器
        /// </summary>
        private AudioMixerGroupManager mGroupMgr = new AudioMixerGroupManager();
        /// <summary>
        /// 音效播放对象池，用于在指定位置播放音效
        /// </summary>
        private GameObjectPool mAudioSourceObjectPool = new GameObjectPool();
        /// <summary>
        /// 下个音效ID
        /// </summary>
        private long mNextAudioID = 1;
        /// <summary>
        /// 音效字典，存放所有正在播放的音效
        /// </summary>
        private Dictionary<long, AudioPlayEntry> mAudioEntryDic = new Dictionary<long, AudioPlayEntry>();
        /// <summary>
        /// 淡入淡出的音效
        /// </summary>
        private List<AudioPlayEntry> mAudioEntryInFading = new List<AudioPlayEntry>();
        /// <summary>
        /// 非循环，等待结束的音效
        /// </summary>
        private List<AudioPlayEntry> mAudioEntryWaitFinish = new List<AudioPlayEntry>();
        /// <summary>
        /// 启动
        /// </summary>
        private void OnStart()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}