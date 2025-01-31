using System;
using System.Collections.Generic;
using UnityEngine;
namespace MizukiTool.Audio
{
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// 单体实例
        /// </summary>
        public static AudioManager Instance;
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
        /// 循环播放的音效
        /// </summary>
        private List<AudioPlayEntry> mAudioEntryInLoop = new List<AudioPlayEntry>();
        /// <summary>
        /// 非循环，等待结束的音效
        /// </summary>
        private List<AudioPlayEntry> mAudioEntryWaitFinish = new List<AudioPlayEntry>();
        /// <summary>
        /// 未使用的AudioPlayEntry
        /// </summary>
        private List<AudioPlayEntry> UnusedAudioPlayEntry = new List<AudioPlayEntry>();
        void Start()
        {
            OnStart();
        }
        void Update()
        {
            OnUpdate();
        }
        /// <summary>
        /// 启动
        /// </summary>
        private void OnStart()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        private void CreatNewAudioPlayEntry()
        {
            AudioPlayEntry audioPlayEntry = new AudioPlayEntry(mNextAudioID);
            UnusedAudioPlayEntry.Add(audioPlayEntry);
            mNextAudioID++;
        }
        public AudioPlayEntry GetAudioPlayEntry()
        {
            if (UnusedAudioPlayEntry.Count == 0)
            {
                CreatNewAudioPlayEntry();
            }

            AudioPlayEntry audioPlayEntry = UnusedAudioPlayEntry[0];
            UnusedAudioPlayEntry.RemoveAt(0);
            audioPlayEntry.Init();
            GameObject go = mAudioSourceObjectPool.Get();
            audioPlayEntry.SetTargetGO(go.transform);
            return audioPlayEntry;
        }
        public bool RetrunAudioPlayEntry(AudioPlayEntry audioPlayEntry)
        {
            if (audioPlayEntry == null)
            {
                return false;
            }
            if (audioPlayEntry.TargetAudioSource.isPlaying)
            {
                audioPlayEntry.TargetAudioSource.Stop();
            }
            UnusedAudioPlayEntry.Add(audioPlayEntry);
            mAudioSourceObjectPool.Free(audioPlayEntry.SelfTransform.gameObject);
            audioPlayEntry.SetTargetGO(null);
            return true;
        }
        public void StartAudioPlayEntry(AudioPlayMod audioPlayMod, AudioPlayEntry audioPlayEntry)
        {
            switch (audioPlayMod)
            {
                case AudioPlayMod.Normal:
                    {
                        mAudioEntryDic.Add(audioPlayEntry.ID, audioPlayEntry);
                        mAudioEntryWaitFinish.Add(audioPlayEntry);
                    }
                    break;
                case AudioPlayMod.Loop:
                    {
                        mAudioEntryDic.Add(audioPlayEntry.ID, audioPlayEntry);
                        audioPlayEntry.TargetAudioSource.loop = true;
                        mAudioEntryInLoop.Add(audioPlayEntry);
                    }
                    break;
                case AudioPlayMod.FadeInOut:
                    {
                        mAudioEntryDic.Add(audioPlayEntry.ID, audioPlayEntry);
                        mAudioEntryInFading.Add(audioPlayEntry);
                    }
                    break;
            }
            audioPlayEntry.Play();
        }
        private void OnUpdate()
        {


            for (int i = mAudioEntryInFading.Count - 1; i >= 0; i--)
            {
                var audioEntry = mAudioEntryInFading[i];
                if (audioEntry.TargetAudioSource.isPlaying)
                {
                    audioEntry.TargetAudioSource.volume = Mathf.Min(1, audioEntry.TargetAudioSource.volume + Time.deltaTime);
                }
                else
                {
                    mAudioEntryDic.Remove(audioEntry.ID);
                    mAudioEntryInFading.Remove(audioEntry);
                    RetrunAudioPlayEntry(audioEntry);
                }
            }

            for (int i = mAudioEntryWaitFinish.Count - 1; i >= 0; i--)
            {
                var audioEntry = mAudioEntryWaitFinish[i];
                if (!audioEntry.IsPlaying())
                {
                    audioEntry.OnAudioEnd();
                    mAudioEntryDic.Remove(audioEntry.ID);
                    mAudioEntryWaitFinish.Remove(audioEntry);
                    RetrunAudioPlayEntry(audioEntry);
                }
            }
            foreach (var audioEnty in mAudioEntryDic)
            {
                audioEnty.Value.OnUpdate();
            }
        }

        public long Play(AudioEnum audioEnum, AudioMixerGroupEnum audioMixerGroupEnum, AudioPlayMod audioPlayMod, Action<AudioSource> endEventHander = null, Action<AudioSource> updateEventHander = null)
        {
            EnsureInstance();
            AudioPlayEntry audioPlayEntry = GetAudioPlayEntry();
            audioPlayEntry.TargetAudioSource.clip = AudioSOManager.audioSO.GetAudioClip(audioEnum);
            audioPlayEntry.TargetAudioSource.outputAudioMixerGroup = AudioMixerGroupManager.GetAudioMixerGroup(audioMixerGroupEnum);
            if (endEventHander != null)
            {
                audioPlayEntry.SetEndHander(endEventHander);
            }
            if (updateEventHander != null)
            {
                audioPlayEntry.SetUpdateHander(updateEventHander);
            }
            StartAudioPlayEntry(audioPlayMod, audioPlayEntry);
            return audioPlayEntry.ID;
        }
        public static void EnsureInstance()
        {
            if (Instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                Instance = go.AddComponent<AudioManager>();
            }
        }
        public void PauseAllLoopAudio()
        {
            for (int i = mAudioEntryInLoop.Count - 1; i >= 0; i--)
            {
                var audioEntry = mAudioEntryInLoop[i];
                audioEntry.Pause();
            }
        }
        public void ContinueAllLoopAudio()
        {
            for (int i = mAudioEntryInLoop.Count - 1; i >= 0; i--)
            {
                var audioEntry = mAudioEntryInLoop[i];
                audioEntry.UnPause();
            }
        }
        public void ReturnAllLoopAudio()
        {
            for (int i = mAudioEntryInLoop.Count - 1; i >= 0; i--)
            {
                var audioEntry = mAudioEntryInLoop[i];
                mAudioEntryDic.Remove(audioEntry.ID);
                mAudioEntryInLoop.Remove(audioEntry);
                RetrunAudioPlayEntry(audioEntry);
            }
        }
        public bool CheckEnumInLoopAudio(AudioEnum audioEnum)
        {
            foreach (var audioEntry in mAudioEntryInLoop)
            {
                if (audioEntry.TargetAudioSource.clip == AudioSOManager.audioSO.GetAudioClip(audioEnum))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public enum AudioPlayMod
    {
        /// <summary>
        /// 普通播放
        /// </summary>
        Normal,
        /// <summary>
        /// 循环播放
        /// </summary>
        Loop,
        /// <summary>
        /// 淡入淡出
        /// </summary>
        FadeInOut,
    }
}
