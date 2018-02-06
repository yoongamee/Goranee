using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Goranee
{
    public abstract class State<TOwner, TMessage> : IMessageProc<TMessage>
    {
        public State()
        {
            ID = -1;
        }

        public State(int id)
        {
            ID = id;
        }
        public virtual void ReceiveMessage(TMessage message) { }
        public int ID { get; protected set; }
        public abstract bool In(TOwner owner);
        public abstract bool Out(TOwner owner);
        public abstract bool Execute(TOwner owner);
        public abstract bool Finish(TOwner owner);
    }
}

