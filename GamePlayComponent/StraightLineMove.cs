using System;
using UnityEngine;
using System.Collections;

public class StraightLineMove : baseObjectMove
{
    public StraightLineMove(float speed)
    {
        Speed = speed;
    }
    private StraightLineMove()
    {
    }
    override public Vector3 Update(Vector3 nowTransform, float deltaTime)
    {
        if (Finish == true)
        {
            return nowTransform;
        }
         
        Vector3 remainDistance = nowTransform - Destination;
        if (Math.Abs(remainDistance.sqrMagnitude) < 0.08f)
        {
            Finish = true;
            return nowTransform;
        }
        else
        {
            returnValue = Vector3.MoveTowards(nowTransform, Destination, deltaTime * Speed);
        }
        return returnValue;
    }

}
