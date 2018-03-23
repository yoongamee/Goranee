using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public abstract class baseMessage
    {
        public IMessageProc     Sender          { get; protected set; }
        public IMessageProc     Receiver        { get; protected set; }

        public int              ID              { get; set; }
        public double           DispatchTime    { get; set; }
        public bool             SwallowMSG      { get; set; }

        public abstract void Execute();

        public virtual void SetReceiver(IMessageProc newReceiver)
        {
            Receiver = newReceiver;
        }

        public virtual void Set(IMessageProc sender, IMessageProc receiver, int messageID, float dispatchTime,
            bool swallow)
        {
            Sender = sender;
            Receiver = receiver;
            DispatchTime = dispatchTime;
            ID = messageID;
            SwallowMSG = swallow;
        }
    }

}
