using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Goranee
{
    public abstract class MessageExtraInfoEx : MessageExtraInfo
    {
        protected MessageExtraInfoEx()
        {

        }

        public abstract bool Execute();
    }
}


