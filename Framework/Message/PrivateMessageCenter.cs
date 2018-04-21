
using System;

using UnityEngine;

namespace Goranee
{
    public class PrivateMessageCenter
    {
        private IMessageProc owner;
        private MessageQueue queue = new MessageQueue();
        private PrivateMessageCenter() { }

        public void Clear()
        {
            queue.ClearAllMessages();
        }
        public PrivateMessageCenter(IMessageProc newOwner)
        {
            owner = newOwner;
        }
        public void SendMessageToOwner(baseMessage newMessage)
        {
            if (newMessage.DispatchTime == baseMessageQueue.Immediately)
            {
                owner.ReceiveMessage(newMessage);
                return;
            }
            else
            {
                newMessage.SetReceiver(owner);
            }
            // time class는 정리가 필요
            newMessage.DispatchTime += Time.realtimeSinceStartup;

            queue.AddMessage(newMessage);
        }        
    }
}