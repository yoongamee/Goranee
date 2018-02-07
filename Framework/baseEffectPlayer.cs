using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class baseEffectPlayer : MonoBehaviour
{
    public float                        Lifetime = 0.0f;

    protected System.Action             endLifeTimeRespond;

    abstract public void Init(System.Action respond);
    abstract public void Play();
    abstract public void Stop();
    abstract public void ReleaseObject();

    virtual public void SetEndLifeTimeRespond(System.Action respond)
    {
        endLifeTimeRespond = respond;
    }
}
