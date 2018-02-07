using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEffectPlayer : baseEffectPlayer
{
    public Animator     AnimComponent;
    // Use this for initialization

    void Awake()
    {
        Init(null);
    }
    private void OnEnable()
    {
        Play();
    }

    public override void Init(System.Action respond)
    {
        if (AnimComponent == null)
        {
            AnimComponent = GetComponentInChildren<Animator>();
        }

        if (0.0f == Lifetime)
        {
            if (AnimComponent != null)
            {
                AnimatorStateInfo info = AnimComponent.GetCurrentAnimatorStateInfo(0);
                if (Lifetime < info.length)
                {
                    Lifetime = info.length;
                }
            }
        }
        SetEndLifeTimeRespond(respond);
    }

    public override void Play()
    {
        gameObject.SetActive(true);
        
        Invoke("Stop", Lifetime * Time.timeScale);
    }

    public override void ReleaseObject()
    {
        Destroy(gameObject);
    }

    public override void Stop()
    {
        AnimComponent.Rebind();
        gameObject.SetActive(false);

        if (endLifeTimeRespond != null)
        {
            endLifeTimeRespond();
        }
    }
    
}
