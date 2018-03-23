
using System;

using UnityEngine;

namespace Goranee
{
    public class PrivateMessageCenter : baseMessageQueue
    {
        private IMessageProc owner;
        private PrivateMessageCenter() { }

        public void Clear()
        {
            delayedMessages.Clear();
        }
        public PrivateMessageCenter(IMessageProc newOwner)
        {
            owner = newOwner;
        }
        public void SendMessageToOwner(baseMessage newMessage)
        {
            if (newMessage.DispatchTime == baseMessageQueue.Immediately)
            {
                //Console.WriteLine("Send Message To System Immediatly : " + newMessage.ExtraInfo.MessageID);
                owner.ReceiveMessage(newMessage);
                return;
            }
            else
            {
                newMessage.SetReceiver(owner);
            }
            // time class는 정리가 필요
            newMessage.DispatchTime += Time.realtimeSinceStartup;

            delayedMessages.Add(newMessage);
            //Console.WriteLine("Message Added : " + newMessage.ExtraInfo.MessageID + " : Time is " + Time.time);
        }

        public override bool SendMsg(baseMessage newMessage)
        {
            if (newMessage == null)
            {
                return false;
            }
            if (newMessage.Receiver == null)
            {
                //Console.WriteLine("NO Receiver -----\n");
                return false;
            }
            if (newMessage.Sender == null)
            {
                //Console.WriteLine("NO Sender -----\n");
            }

            if (newMessage.DispatchTime == baseMessageQueue.Immediately)
            {
                //Console.WriteLine("Send Message Immediatly : " + newMessage.ExtraInfo.MessageID);
                discard(newMessage);
                return true;
            }

            // time class는 정리가 필요
            newMessage.DispatchTime += Time.realtimeSinceStartup;

            delayedMessages.Add(newMessage);
            /*if (newMessage.ExtraInfo != null)
            {
                Console.WriteLine("Message Added : " + newMessage.ExtraInfo.MessageID + " : Time is " + Time.time);
            }*/
            return true;
        }

        protected override void discard(baseMessage newMessage)
        {
            /*if (newMessage.ExtraInfo != null)
            {
                Console.WriteLine("Send Message Dischard - remove : " + newMessage.ExtraInfo.MessageID);
            }*/
            if (newMessage.Receiver != null)
            {
                newMessage.Receiver.ReceiveMessage(newMessage);
            }
            /*
        if (newMessage.ExtraInfo != null)
        {
            newMessage.ExtraInfo.Executer(newMessage.Sender, newMessage.Receiver);
        }
        */
            delayedMessages.Remove(newMessage);
            //Singleton<PoolerManager>.Get().Release(newMessage);   todo change
        }

        protected override bool IsPassTime(baseMessage newMessage)
        {
            if (newMessage.DispatchTime <= (double)Time.realtimeSinceStartup)
            {
                return true;
            }
            return false;
        }
    }
}