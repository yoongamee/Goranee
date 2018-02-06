
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public abstract class MessageQueue<T> 
    {
        public static readonly float Immediately = -1.0f;

        protected List<T> delayedMessages = new List<T>(32);

        public abstract bool SendMsg(T newMessage);
        protected abstract void discard(T newMessage);

        public virtual void Update()
        {
            if (delayedMessages.Count == 0)
            {
                return;
            }
            for (int i = 0; i < delayedMessages.Count; i++)
            {
                if (delayedMessages[i] == null)
                {
                    delayedMessages.RemoveAt(i);
                    continue;
                }

                if (IsPassTime(delayedMessages[i]) == true)
                {
                    discard(delayedMessages[i]);
                }
            }
        }

        protected abstract bool IsPassTime(T newMessage);
    }

}