using UnityEngine;
using System.Collections;

namespace Goranee
{
    abstract public class AnimEventReceiver<T> : MonoBehaviour
    {
        [HideInInspector]
        public T eventProcesser;

    }
}
