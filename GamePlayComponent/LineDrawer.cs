using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    public      LineRenderer     Drawer;
    
	// Use this for initialization
	
	void Start()
    {
        if (Drawer == null )
        {
            Debug.LogWarning("Renderer is Empty : LineDrawer");
        }
    }
    public void Draw(Vector3 start, Vector3 startUpVector, Vector3 dest, Vector3 destUpVector, float intensity)
    {
        Clear();
      
        MakeLine(start, startUpVector, dest, destUpVector, intensity);
    }
    public void Draw(List<Vector3> points)
    {
        if ( points == null)
        {
            Clear();
            return;
        }
        if (Drawer == null)
        {
            return;
        }
        Drawer.positionCount = points.Count;
        Drawer.SetPositions(points.ToArray());
    }
    
	private void Clear()
    {
        Drawer.positionCount = 0;
    }
    protected virtual void MakeLine(Vector3 start, Vector3 startUpVector, Vector3 dest, Vector3 destUpVector, float intensity )
    {
        
        Vector3 p0 = start;
        Vector3 p1 = start + startUpVector * intensity;
        Vector3 p2 = dest + destUpVector * intensity;
        Vector3 p3 = dest;
        
        Vector3 midPoint0 = Vector3.zero;
        Vector3 midPoint1 = Vector3.zero;
        Vector3 midPoint2 = Vector3.zero; 
        Vector3 finalPoint0 = Vector3.zero;
        Vector3 finalPoint1 = Vector3.zero;

        Drawer.positionCount = 11;

        int index = 0;

        for (float delta = 0.0f; delta < 1.0f; delta += 0.1f)
        {
            midPoint0 = (p1 - p0) * delta + p0;
            midPoint1 = (p2 - p1) * delta + p1;
            midPoint2 = (p3 - p2) * delta + p2;

            finalPoint0 = (midPoint1 - midPoint0) * delta + midPoint0;
            finalPoint1 = (midPoint2 - midPoint1) * delta + midPoint1;

            Vector3 returnValue = (finalPoint1 - finalPoint0) * delta + finalPoint0;
            Drawer.SetPosition(index, returnValue);

            index++;
        }
        Drawer.SetPosition(index, dest);
    }
}
