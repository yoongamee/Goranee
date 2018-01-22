using Goranee;
using UnityEngine;
using System.Collections;

public class AreaTrigger : MonoBehaviour
{
    public AreaConditionNode AreaCondition;
    
	// Use this for initialization
	void Awake ()
	{
	    AreaCondition.Init();
	}

    void UpdateLogic()
    {
        if (AreaCondition.Pass() == true)
        {
            AreaCondition.Finish();
            Debug.Log("Area Condition P{ASS");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter-tri " + Time.frameCount);
        AreaCondition.OnArea(ConditionType.ENTER, other);
    }
    // enter와 동시에 발생.
    void OnTriggerStay(Collider other)
    {
      //  Debug.LogError("Enter-sta " + +Time.frameCount);
    }
}
