
using Game2D.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2D.Controller
{
    public class AudioController : MonoBehaviour
    {
        public Sound[] sounds;

        public AudioSource bgmSource;

        void Awake()
        {
            foreach (Sound s in sounds)
            {
                AudioSource sourceTemp = gameObject.GetComponent<AudioSource>();
                s.source = sourceTemp;
                s.sourceBgm = bgmSource.GetComponent<AudioSource>() ;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }

        public void PlayBgm(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.sourceBgm.clip = s.clip;
            s.sourceBgm.volume = s.volume;
            s.sourceBgm.pitch = s.pitch;
            s.sourceBgm.Play();
        }

        public void StopBgm(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.sourceBgm.Stop();
        }

    }

}
