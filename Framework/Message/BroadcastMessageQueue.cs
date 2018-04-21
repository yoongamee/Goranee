using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Goranee
{
    public class BroadcastMessageQueue : baseMessageQueue
    {
        protected BroadcastMessageQueue() { }
        public BroadcastMessageQueue (Action<baseMessage> newBroadCaster)
        {
            broadcaster = newBroadCaster;
        }
        protected Action<baseMessage> broadcaster;

        public override bool SendMsg(baseMessage newMessage, float time)
        {
            if (newMessage == null)
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
            else
            {
                if (broadcaster != null)
                {
                    broadcaster.Invoke(newMessage);
                }
            }
            delayedMessages.Remove(newMessage);
            
        }

        protected override bool IsPassTime(baseMessage newMessage, float time)
        {
            if (newMessage.DispatchTime <= time)
            {
                return true;
            }
            return false;
        }
    }
}


