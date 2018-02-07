using System.Collections;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

namespace Goranee
{
    public class ParticleEffectPlayer : baseEffectPlayer
    {
        private ParticleSystem[]    EffectInterval;

        void Awake()
        {
            Init(null);
        }
        private void OnEnable()
        {
            Play( );
        }
        public override void Init(System.Action respond)
        {
            EffectInterval = GetComponentsInChildren<ParticleSystem>();

            if (0.0f == Lifetime)
            {
                if (EffectInterval != null)
                {
                    for (int i = 0; i < EffectInterval.Length; ++i)
                    {
                        EffectInterval[i].Play();
                        if (0 == i)
                            Lifetime = EffectInterval[i].main.duration;

                        if (0 < i && Lifetime < EffectInterval[i].main.duration)
                            Lifetime = EffectInterval[i].main.duration;
                    }
                }
            }
            SetEndLifeTimeRespond(respond);
        }

        public override void Play()    
        {
            gameObject.SetActive(true);
            if (EffectInterval != null)
            {
                for (int i = 0; i < EffectInterval.Length; ++i)
                {
                    EffectInterval[i].Play();
                }
            }
            
            Invoke("Stop", Lifetime * Time.timeScale);
        }

        public override void ReleaseObject()
        {
            Destroy(gameObject);
        }

        public override void Stop()
        {
            for (int i = 0; i < EffectInterval.Length; ++i)
            {
                if (EffectInterval[i] != null)
                {
                    EffectInterval[i].Stop();
                }
            }
            gameObject.SetActive(false);
            
            if (endLifeTimeRespond != null)
            {
                endLifeTimeRespond();
            }
        }
        public void OnDestroy()
        {
            for (int i = 0; i < EffectInterval.Length; ++i)
            {
                if (EffectInterval[i] != null)
                {
                    EffectInterval[i] = null;
                }
            }
        }
    }
}

