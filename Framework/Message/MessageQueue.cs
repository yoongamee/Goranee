using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public class MessageQueue : baseMessageQueue
    {
        public override bool SendMsg(baseMessage newMessage, float time)
        {
            if (newMessage == null)
            {
                return false;
            }
            if (newMessage.Receiver == null)
            {
                return false;
            }
            if (newMessage.Sender == null)
            {
                return false;
            }

            if (newMessage.DispatchTime == baseMessageQueue.Immediately)
            {
                discard(newMessage);
                return true;
            }

            // time class는 정리가 필요
            newMessage.DispatchTime += time;

            delayedMessages.Add(newMessage);

            return true;
        }

        protected override void discard(baseMessage newMessage)
        {

            if (newMessage.Receiver != null)
            {
                newMessage.Receiver.ReceiveMessage(newMessage);
            }

            delayedMessages.Remove(newMessage);
        }

        protected override bool IsPassTime(baseMessage newMessage, float nowTime)
        {
            if (newMessage.DispatchTime <= nowTime)
            {
                return true;
            }
            return false;
        }
    }
}