using UnityEngine;
using System.Collections;

namespace Goranee
{
    public enum ConditionType : int
    {
        ENTER = 0,
        STAY,
        EXIT,

    }

    public enum ConditionResult : int
    {
        NEVER = 0,
        CONDITION_ON,
        EXECUTED,
    }

    public enum RepeatMode : int
    {
        INFINITY = -1,
        ONCE     = 1,
    }
    public interface IConditionNode
    {
        void Init();
        bool Pass();
        void Finish();

        void Update(float time);
    }
}