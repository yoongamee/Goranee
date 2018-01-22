using UnityEngine;
using System.Collections;
[System.Serializable]

public class ITriggerConditionTest
{
    public int TiggerCondition;
    public virtual bool Query()
    {
        return true;
    }
}
