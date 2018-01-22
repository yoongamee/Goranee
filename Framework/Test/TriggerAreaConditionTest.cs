using System;

using UnityEngine;
using System.Collections;
//[CustomEditor(typeof(ITriggerConditionTest), true)]   
[System.Serializable]

public class TriggerAreaConditionTest : ITriggerConditionTest
{
    public BoxCollider collider;
    public int Babo = 0;
    public override bool Query()
    {
        return true;
    }
    
    public void PrintMan()
    {
        
    }
}
