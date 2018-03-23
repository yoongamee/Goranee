using UnityEngine;
using System.Collections;
using System;

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
        }
    }
}