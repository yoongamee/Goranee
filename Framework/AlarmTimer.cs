using UnityEngine;
using System.Collections;

namespace Goranee
{
    public class AlarmTimer
    {
        public float FinishTime { get; set; }
        protected float elapsedTime;

        public float GetElapsedTime()
        {
            return elapsedTime;
        }

        public void Start()
        {
            elapsedTime = 0.0f;
        }

        public bool Update(float deltaTime)
        {
            if (FinishTime <= elapsedTime)
            {
                return true;
            }

            elapsedTime += deltaTime;

            return false;
        }
    }
}

