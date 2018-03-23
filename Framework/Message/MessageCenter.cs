using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Goranee
{
    public class MessageCenter : baseMessageQueue
    {
        private HashSet<IMessageProc> subscribers = new HashSet<IMessageProc>();

        public void Clear()
        {
            subscribers.Clear();
            delayedMessages.Clear();
        }
        public void AddSubscriber(IMessageProc newMember)
        {
            if (subscribers.Contains(newMember) == false)
            {
                subscribers.Add(newMember);
            }
        }
        public void RemoveSubscriber(IMessageProc remMember)
        {
            if ( subscribers.Contains(remMember) == true)
            {
                subscribers.Remove(remMember);
            }
        }
        private void broadcast(baseMessage newMessage)
        {
            var enumerator = subscribers.GetEnumerator();
            while (enumerator.MoveNext() == true)
            {
                if (enumerator.Current != null)
                {
                    if (enumerator.Current.ReceiveMessage(newMessage) == true && newMessage.SwallowMSG == true)
                    {
                        break;
                    }
                }
            }
        }

        public override bool SendMsg(baseMessage newMessage)
        {
            if (newMessage == null)
            {
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