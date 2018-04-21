using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Goranee
{
    public class MessageCenter
    {
        private HashSet<IMessageProc> subscribers = new HashSet<IMessageProc>();
        protected BroadcastMessageQueue queue;
        public MessageCenter()
        {
            queue = new BroadcastMessageQueue(broadcast);
        }
        public void Clear()
        {
            subscribers.Clear();
            if (queue != null)
            {
                queue.ClearAllMessages();
            }
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
        public bool SendMsg(baseMessage newMessage, float time)
        {
            return  queue != null ? queue.SendMsg(newMessage, time) : false;
        }
        public void Update(float nowTime)
        {
            if (queue != null)
            {
                queue.Update(nowTime);
            }
        }
        protected void broadcast(baseMessage newMessage)
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
    }
}