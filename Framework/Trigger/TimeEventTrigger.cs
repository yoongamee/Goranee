using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    [System.Serializable]
    public class TimeEventTrigger : MonoBehaviour
    {
        public List<TimeConditionNode>  Conditions;
        public IResultNode              Result;

        public virtual bool Pass()
        {
            return true;
        }

        
    }
}