using UnityEngine;

public abstract class baseObjectMove
{
    protected Vector3 returnValue;
    protected Vector3 startPosition;
    protected Vector3 destination;

    public bool       lookatDestPos { get; set; }
    public Vector3 Start
    {
        get
        {
            return startPosition;

        }
        set
        {
            startPosition.x = value.x;
            startPosition.y = value.y;
            startPosition.z = value.z;
        }
    }
    public Vector3 Destination
    {
        get
        {
            return destination;
            
        }
        set
        {
            destination.x = value.x;
            destination.y = value.y;
            destination.z = value.z;
        }
    }
    
    public float        Speed          { get; set; }
    public bool         Finish         { get; protected set; }

    public virtual void Go(Vector3 startPosition, Vector3 destPosition)
    {
        Destination = destPosition;
        Start = startPosition;
        Finish = false;
    }
    public virtual void Go(Vector3 destPosition)
    {
        Destination = destPosition;
        Finish = false;
    }
    public virtual void Go()
    {
        Finish = false;
    }
    abstract public Vector3  Update(Vector3 nowPosition, float deltaTime);

}
