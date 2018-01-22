using System;
using UnityEngine;
using System.Collections;

public class BezierCurveMove : baseObjectMove
{
    protected Vector3 controlPoint0, controlPoint1, controlPoint2, controlPoint3;
    protected Vector3 midPoint0, midPoint1, midPoint2;
    protected Vector3 finalPoint0, finalPoint1;
	protected float	epsilon = 0.3f;
    protected float delta = 0.0f;

	public void SetTargetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float descEpsilon = 0.01f)
    {
        delta = 0.0f;
        Finish = false;
        controlPoint0 = p0; controlPoint1 = p1; controlPoint2 = p2; controlPoint3 = p3;
		epsilon = descEpsilon;
        /*Debug.Log(controlPoint0);
        Debug.Log(controlPoint1);
        Debug.Log(controlPoint2);
        Debug.Log(controlPoint3);
        */
    }
    protected void Clear()
    {
        controlPoint0 = Vector3.zero;
        controlPoint1 = Vector3.zero;
        controlPoint2 = Vector3.zero;
        controlPoint3 = Vector3.zero;
    }
    protected void UpdatePoints()
    {
        if (controlPoint0 != controlPoint1)
        {
            delta += (Speed + Mathf.Lerp(0.0f, 0.0001f, delta));    // with velocity
            //delta += Speed;

            if (1.0f <= delta)
            {
                delta = 1.0f;
            }
        }
        else
        {
            returnValue = controlPoint3;
            return;
        }
        midPoint0 = (controlPoint1 - controlPoint0) * delta + controlPoint0;
        midPoint1 = (controlPoint2 - controlPoint1) * delta + controlPoint1;
        midPoint2 = (controlPoint3 - controlPoint2) * delta + controlPoint2;

        finalPoint0 = (midPoint1 - midPoint0) * delta + midPoint0;
        finalPoint1 = (midPoint2 - midPoint1) * delta + midPoint1;

        returnValue = (finalPoint1 - finalPoint0) * delta + finalPoint0;

        //Debug.Log("retr = " + returnValue + " l " + finalPoint0 + " : " + finalPoint1);

        Debug.DrawLine(controlPoint1, controlPoint0, Color.yellow);
        Debug.DrawLine(controlPoint2, controlPoint1, Color.yellow);
        Debug.DrawLine(controlPoint3, controlPoint2, Color.yellow);

        Debug.DrawLine(midPoint0, midPoint1, Color.blue);
        Debug.DrawLine(midPoint1, midPoint2, Color.green);

        Debug.DrawLine(finalPoint1, finalPoint0, Color.magenta);

        

    }
    public override void Go()
    {
        
    }

    override public Vector3 Update(Vector3 nowTransform, float deltaTime)
    {
        if (Finish == true)
        {
            return nowTransform;
        }
//        Debug.Log(delta);
		//Debug.Log(Vector3.Distance(controlPoint3, nowTransform));
	
		if (Vector3.Distance(controlPoint3, nowTransform) <= epsilon)
        {
            Finish = true;
            //Clear();
        }

        UpdatePoints();
        //returnValue += nowTransform;
        
        return returnValue;
    }
}
