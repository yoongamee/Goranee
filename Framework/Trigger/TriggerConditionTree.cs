using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Goranee
{
    [System.Serializable]
    public class TriggerConditionTree
    {
        public List<IConditionNode> Conditions;

        public virtual bool Pass()
        {
            return true;
        }
    }
}
