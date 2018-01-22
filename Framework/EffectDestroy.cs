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
        void OnEnable() // Mono의 경우 pooling시 Start나 Awake가 아닌 Enable-Disable에 Get-Release를 넣어주길.
        {
            EffectInterval = GetComponentsInChildren<ParticleSystem>();

            //InitParticles(); 파티클이 정상작동하지 않음. stop다음 바로 play하면 particle이 실행되지 않음.

            if (0.0f == Lifetime)
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
                AnimatorComponent.StartPlayback();
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
            Singleton<GameObjectPooler>.Get().Release(gameObject.name, gameObject);
            if (gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);    
            }
        }

        void OnDisable()
        {
            CancelInvoke();
            ReleaseObject();
            InitParticles();
        }
    }

}

