using System;
using UnityEngine;
using System.Collections;

public class HalfCircleMove : baseObjectMove
{

    public Vector3 Center;
    public float Distance;
    public float angle = 0.0f;
    public Vector3 NextPos;
    override public Vector3 Update(Vector3 nowTransform, float deltaTime)
    {
        angle += Speed;


        returnValue.x = Mathf.Cos(angle) * Center.x + Mathf.Sin(angle) * -1.0f * Center.y;
        returnValue.y = Mathf.Sin(angle) * Center.x + Mathf.Cos(angle) * Center.y;
        returnValue.z = nowTransform.z;    
        return returnValue;
    }
}
