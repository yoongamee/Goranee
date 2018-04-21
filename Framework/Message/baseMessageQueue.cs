
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public abstract class baseMessageQueue
    {
        public static readonly float Immediately = -2.0f;
        public static readonly float Queueing = -1.0f;

        protected List<baseMessage> delayedMessages = new List<baseMessage>(32);

        public virtual void ClearAllMessages()
        {
            delayedMessages.Clear();
        }
        public virtual void AddMessage(baseMessage message)
        {
            if ( message != null)
            {
                delayedMessages.Add(message);
            }
        }
        public abstract bool SendMsg(baseMessage newMessage, float time);
        protected abstract void discard(baseMessage newMessage);

        public virtual void Update(float time)  // not delta time
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

                if (IsPassTime(delayedMessages[i], time) == true)
                {
                    discard(delayedMessages[i]);
                }
            }
        }

        protected abstract bool IsPassTime(baseMessage newMessage, float nowTime);
    }

}