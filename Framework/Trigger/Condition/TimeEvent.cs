using Goranee;
using UnityEngine;
using System.Collections;

public class TimeEvent : TimeConditionNode
{
    public delegate void ResultExecuter();
    protected ResultExecuter Executer;

    protected TimeEvent() { }

    public TimeEvent(ResultExecuter newExecuter)
    {
        SetResultExecuter(newExecuter);
    }
    public void SetResultExecuter(ResultExecuter newExecuter)
    {
        Executer = newExecuter;
    }

    public override void Finish()
    {
        base.Finish();
        if (Executer != null)
        {
            Executer();
        }
    }
}
