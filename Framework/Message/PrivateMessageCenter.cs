
using System;

using UnityEngine;

namespace Goranee
{
    public class PrivateMessageCenter : MessageQueue<Message>
    {
        private IMessageProc<Message> owner;
        private PrivateMessageCenter() { }

        public PrivateMessageCenter(IMessageProc<Message> newOwner)
        {
            owner = newOwner;
        }
        public void SendMessageToOwner(Message newMessage)
        {
            if (newMessage.ExtraInfo == null)
            {
                return;
            }

            if (newMessage.DispatchTime == MessageQueue<Message>.Immediately)
            {
                //Console.WriteLine("Send Message To System Immediatly : " + newMessage.ExtraInfo.MessageID);
                owner.ReceiveMessage(newMessage);
                return;
            }

            // time class는 정리가 필요
            newMessage.DispatchTime += Time.realtimeSinceStartup;

            delayedMessages.Add(newMessage);
            //Console.WriteLine("Message Added : " + newMessage.ExtraInfo.MessageID + " : Time is " + Time.time);
        }

        public override bool SendMsg(Message newMessage)
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

            if (newMessage.DispatchTime == MessageQueue<Message>.Immediately)
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

        protected override void discard(Message newMessage)
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

        protected override bool IsPassTime(Message newMessage)
        {
            if (newMessage.DispatchTime <= (double)Time.realtimeSinceStartup)
            {
                return true;
            }
            return false;
        }
    }
}