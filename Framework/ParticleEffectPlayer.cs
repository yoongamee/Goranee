using System.Collections;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

namespace Goranee
{
    public class ParticleEffectPlayer : MonoBehaviour
    {
        private ParticleSystem[] EffectInterval;
        public bool IsPlaying;

        void Awake()
        {
            Stop();
        }
       
        protected void InitParticles()
        {
            if (EffectInterval == null)
            {
                EffectInterval = GetComponentsInChildren<ParticleSystem>();
            }

            for (int i = 0; i < EffectInterval.Length; ++i)
            {
                if (EffectInterval[i].isPlaying == true)
                {
                    EffectInterval[i].Stop();
                    EffectInterval[i].Clear();
                }
            }
        }

        public void Play()
        {
            gameObject.SetActive(true);
            EffectInterval = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < EffectInterval.Length; ++i)
            {
                EffectInterval[i].Play();
            }
            IsPlaying = true;
        }

        public void Stop()
        {
            InitParticles();
            IsPlaying = false;

            gameObject.SetActive(false);
        }
        void OnDisable()
        {
            Stop();
        }
    }
}

