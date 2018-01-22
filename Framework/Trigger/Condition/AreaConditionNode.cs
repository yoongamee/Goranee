using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Goranee
{
    [System.Serializable]
    
    public class AreaConditionNode : IConditionNode
    {
        public List<Collider>           Areas;
        public ConditionType            Type;
        public bool                     Once;
        [HideInInspector]
        protected ConditionResult       Result;

        public void Init()
        {
            if (Areas == null)
            {
                Debug.LogError("Plase Setting up Areas");
            }
        }

        public bool Pass()
        {
            if (Areas == null)
            {
                return false;
            }

            if (Result == ConditionResult.CONDITION_ON)
            {
                return true;
            }
            
            return false;
        }
        public void Update(float time) { }
        public void Finish()
        {
            Result = ConditionResult.EXECUTED;
        }
        public void OnArea(ConditionType eventType, Collider other)
        {
            if (Once == true && Result != ConditionResult.NEVER)
            {
                return;
            }

            if (Type == eventType)
            {
                Result = ConditionResult.CONDITION_ON;    
            }
        }
        // enter와 동시에 발생.
       
    }
}
