using System;
using System.Collections.Generic;
using Pattern;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        private AudioSource audioSource;
        
        public Dictionary<SoundType, AudioClip> clips = new Dictionary<SoundType, AudioClip>()
        {
            {SoundType.ClickBall, null},
            {SoundType.MakeMove, null}
        };
        private void Awake()
        {
            AssignInstance(this);
            audioSource = GetComponent<AudioSource>();
            AssignSoundFromResource();
        }

        private void AssignSoundFromResource()
        {
            clips[SoundType.ClickBall] = Resources.Load<AudioClip>($"Sound/{SoundType.ClickBall}");
            clips[SoundType.MakeMove] = Resources.Load<AudioClip>($"Sound/{SoundType.MakeMove}");
        }

        public void PlaySound(SoundType _soundType)
        {
            if (clips[_soundType] != null)
            {
                audioSource.PlayOneShot(clips[_soundType]);
            }
        }
    }
}
