using UnityEngine;

namespace Goranee
{
    [RequireComponent(typeof(Collider))]
    abstract public class baseGenerator : MonoBehaviour
    {
        public Collider GenerateZone;

        abstract public void Generate();

    }
}

