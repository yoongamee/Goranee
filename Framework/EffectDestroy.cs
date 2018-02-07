using System.Collections;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

namespace Goranee
{
    public class EffectDestroy : MonoBehaviour
    {
        private ParticleSystem[]    EffectInterval;
        public float                Lifetime = 0f;
        public Animator             AnimatorComponent;

        void OnEnable() 
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
                if (AnimatorComponent != null)
                {
                    AnimatorStateInfo info = AnimatorComponent.GetCurrentAnimatorStateInfo(0);
                    if (Lifetime < info.length)
                    {
                        Lifetime = info.length;
                    }
                }
            }
            
            Invoke("ReleaseObject", Lifetime * Time.timeScale);

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

        public void ReleaseObject()
        {
            //Singleton<GameObjectPooler>.Get().Release(gameObject.name, gameObject);
            Destroy(gameObject);
            if (gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);    
            }
        }

        void OnDisable()
        {
            CancelInvoke();
            InitParticles();
        }
    }

}

