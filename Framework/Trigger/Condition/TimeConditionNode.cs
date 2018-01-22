using UnityEngine;
using System.Collections;

namespace Goranee
{
    [System.Serializable]
    public class TimeConditionNode : IConditionNode
    {
        public int      RepeatCount = (int)RepeatMode.INFINITY;
        public float    Terms = 1.0f;


        protected float elapsedTime = 0.0f;
        protected int   executeCount = 0;

        protected bool  hold = false;

        public bool IsHold()
        {
            return hold;
        }

        public void SetRemainTime(float remainTime)
        {
			elapsedTime = Terms - remainTime;//(Terms- remainTime);
			//Debug.Log(hold + " , " + elapsedTime + " , " + Terms);
        }

        public float GetRemainTime()
        {
			return Terms - elapsedTime;
        }
        public void SetInfo( int repeatCount, float timeDiff)
        {
            RepeatCount = repeatCount;
			Terms = timeDiff;
        }
        public virtual void Init()
        {
            executeCount = 0;
			elapsedTime = 0;
        }
		public virtual void Hold(bool b)
        {
			hold = b;
            /*if ( b == true && elapsedTime <= 0.0f)
            {
                Debug.Log("True");
            }*/
            
        }
        public virtual void Restart()
        {
            hold = false;
			Finish ();
        }
        public virtual void Update(float deltaTime)
        {
            if (hold == true)
            {
				return;
            }
			//Debug.Log (deltaTime);
			elapsedTime += deltaTime;
        }

        public virtual bool Pass()
        {

			if ( Terms <= elapsedTime)
            {
                if (RepeatCount == (int) RepeatMode.INFINITY ||
                    0 < RepeatCount - executeCount)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Finish()
        {
			elapsedTime = 0;
            executeCount++;
        }
    }    
}


