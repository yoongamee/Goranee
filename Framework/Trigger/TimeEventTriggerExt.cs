using System;
using System.Collections.Generic;
using Goranee;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[System.Serializable]
public class TimeEventTrigger<TownerType, TkeyType>
{
    protected TimeEventTrigger() { }

    public TimeEventTrigger(TownerType owner)
    {
        Owner = owner;
    }
    

    protected Dictionary<TkeyType, TimeEvent> Events = new Dictionary<TkeyType, TimeEvent>();
    protected TownerType Owner;

    public void HoldAll(bool b)
    {
        if (Events == null)
        {
            return;
        }
        var enumerator = Events.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                enumerator.Current.Value.Hold(b);
            }
        }
    }
    public bool RemoveEvent(TkeyType removeKey)
    {
        if (Events.ContainsKey(removeKey) == false)
        {
            return true;
        }
        return Events.Remove(removeKey);
    }

    public bool AddEvent(TkeyType newEventKey, TimeEvent newEvent)
    {
        if (Events == null)
        {
            return false;
        }

        if (Events.ContainsKey(newEventKey) == true)
        {
            return false;
        }

        Events.Add(newEventKey, newEvent);
        return true;
    }
    public TimeEvent GetEvent(TkeyType type)
    {
        if (Events == null)
        {
            return null;
        }

        if (Events.ContainsKey(type) == false)
        {
            return null;
        }
        return Events[type];
    }
    public void Update(float time)
    {
        if (Events == null)
        {
            return;
        }
        var enumerator = Events.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                enumerator.Current.Value.Update(time);

                if (enumerator.Current.Value.Pass() == true)
                {
                    enumerator.Current.Value.Finish();
                }
            }
        }
    }
}
