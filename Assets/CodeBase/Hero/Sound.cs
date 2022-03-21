using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace CodeBase.Hero
{
    public class Sound : MonoBehaviour
    {
        public AudioClip[] stepSounds;
        public AudioClip[] AttackSounds;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Step()
        {
            audioSource.PlayOneShot(stepSounds[Random.Range(0,stepSounds.Length)]);
        }

        public void AttackBegin()
        {
            audioSource.PlayOneShot(AttackSounds[Random.Range(0,AttackSounds.Length)]);
        }
        
    }
}