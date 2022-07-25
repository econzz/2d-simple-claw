using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2D.Define
{
    /// <summary>
    /// Sound class
    /// </summary>
    [System.Serializable]
    public class Sound
    {

        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0f, 1f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;

        [HideInInspector]
        public AudioSource sourceBgm;
    }
}

