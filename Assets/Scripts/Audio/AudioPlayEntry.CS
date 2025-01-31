using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

namespace MizukiTool.Audio
{
    public class AudioPlayEntry
    {
        private bool isPlaying = false;
        public long ID;
        public AudioSource TargetAudioSource
        {
            get
            {
                AudioSource audioSource = null;
                if (SelfTransform != null)
                {
                    if (!SelfTransform.TryGetComponent(out audioSource))
                    {
                        SelfTransform.gameObject.AddComponent<AudioSource>();
                    }
                    return SelfTransform.GetComponent<AudioSource>();
                }
                return null;
            }
        }
        public Transform SelfTransform;
        public Action<AudioSource> endHander;
        public Action<AudioSource> updateHander;
        public AudioPlayEntry(long ID)
        {
            this.ID = ID;
            SelfTransform = null;
            endHander = null;
        }
        public void Init()
        {
            SelfTransform = null;
            endHander = null;
        }
        public AudioPlayEntry SetPosition(Vector3 position)
        {
            this.SelfTransform.position = position;
            return this;
        }
        public AudioPlayEntry SetTargetGO(Transform targetGO)
        {
            this.SelfTransform = targetGO;
            return this;
        }
        public AudioPlayEntry SetEndHander(Action<AudioSource> endHander)
        {
            this.endHander = endHander;
            return this;
        }
        public AudioPlayEntry SetUpdateHander(Action<AudioSource> updateHander)
        {
            this.updateHander = updateHander;
            return this;
        }
        public void Play()
        {
            if (SelfTransform != null)
            {
                TargetAudioSource.transform.position = SelfTransform.position;
            }
            isPlaying = true;
            TargetAudioSource.Play();
        }
        public void Stop()
        {
            TargetAudioSource.Stop();
        }
        public void Pause()
        {
            TargetAudioSource.Pause();
        }
        public void UnPause()
        {
            TargetAudioSource.UnPause();
        }
        public AudioPlayEntry SetLoop(bool loop)
        {
            TargetAudioSource.loop = loop;
            return this;
        }
        public AudioPlayEntry SetVolume(float volume)
        {
            TargetAudioSource.volume = volume;
            return this;
        }
        public void SetPitch(float pitch)
        {
            TargetAudioSource.pitch = pitch;
        }
        public void OnAudioEnd()
        {
            if (endHander != null)
            {
                endHander(TargetAudioSource);
            }
        }
        public void OnUpdate()
        {
            if (updateHander != null)
            {
                updateHander(TargetAudioSource);
            }
            if (!TargetAudioSource.isPlaying)
            {
                isPlaying = false;
            }
        }
        public bool IsPlaying()
        {
            return isPlaying;
        }

    }
}