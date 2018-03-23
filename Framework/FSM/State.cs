using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Goranee
{
    public abstract class State<TOwner> : IMessageProc
    {
        public State()
        {
            ID = -1;
        }

        public State(int id)
        {
            ID = id;
        }
        public virtual bool ReceiveMessage(baseMessage message) { return false; }
        public int ID { get; protected set; }
        public abstract bool In(TOwner owner);
        public abstract bool Out(TOwner owner);
        public abstract bool Execute(TOwner owner);
        public abstract bool Finish(TOwner owner);
    }
}

