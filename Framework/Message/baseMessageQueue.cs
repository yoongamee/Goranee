
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public abstract class baseMessageQueue
    {
        public static readonly float Immediately = -1.0f;

        protected List<baseMessage> delayedMessages = new List<baseMessage>(32);

        public abstract bool SendMsg(baseMessage newMessage);
        protected abstract void discard(baseMessage newMessage);

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

        protected abstract bool IsPassTime(baseMessage newMessage);
    }

}