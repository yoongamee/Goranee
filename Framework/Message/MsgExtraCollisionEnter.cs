using UnityEngine;
using System.Collections;

namespace Goranee
{
    public class MsgExtraCollisionEnter : MessageExtraInfo
    {
        public Collision collisionInfo;

        public MsgExtraCollisionEnter()
        {

        }

        public MsgExtraCollisionEnter(Collision collision)
        {
            Set(collision);
        }

        public void Set(Collision collision)
        {
            collisionInfo = collision;
            MessageID = 1;
        }

        public override bool Executer()
        {
            return true;
        }
    }
}