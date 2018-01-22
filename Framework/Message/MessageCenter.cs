using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Goranee
{
    public class MessageCenter : MessageQueue<Message>
    {

        private HashSet<IMessageProc<Message>> subscribers = new HashSet<IMessageProc<Message>>();

        public void AddSubScriber(IMessageProc<Message> newMember)
        {
            if (subscribers.Contains(newMember) == false)
            {
                subscribers.Add(newMember);
            }
        }
        public void RemoveSubScriber(IMessageProc<Message> remMember)
        {
            if ( subscribers.Contains(remMember) == true)
            {
                subscribers.Remove(remMember);
            }
        }
        private void broadcast(Message newMessage)
        {
            var enumerator = subscribers.GetEnumerator();
            while (enumerator.MoveNext() == true)
            {
                if (enumerator.Current != null)
                {
                    enumerator.Current.ReceiveMessage(newMessage);
                }
            }
        }

        public override bool SendMsg(Message newMessage)
        {
            if (newMessage == null)
            {
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
            if (newMessage.Receiver == null && newMessage.DispatchTime == MessageQueue<Message>.Immediately)    // broadcasting
            {
                broadcast(newMessage);
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
            else
            {
                broadcast(newMessage);
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