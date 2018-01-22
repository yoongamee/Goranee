using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Goranee
{
    public abstract class State<TOwner, TMessage>
    {
        public static readonly int MySelf = -1;
        public State()
        {
            ID = -1;
        }

        public State(int id)
        {
            ID = id;
        }

        public int ID { get; protected set; }
        public abstract bool In(TOwner owner);
        public abstract bool Out(TOwner owner);
        public abstract bool Execute(TOwner owner);
        public abstract int Finish(TOwner owner);

        public abstract bool ProcessMessage(TOwner owner, TMessage message);
    }
}

