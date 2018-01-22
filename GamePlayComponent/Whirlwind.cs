using UnityEngine;
using System.Collections;

namespace Goranee
{
    [RequireComponent(typeof (Collider))]
    public class Whirlwind : MonoBehaviour
    {
        public float power;
        public Vector3 waveDir;

        protected Vector3 nowWaveDir;
        protected Vector3 desiredDir;

        protected float degree;
        protected float velocity;

        private void Start()
        {
            nowWaveDir = waveDir;

            desiredDir.x = nowWaveDir.x*-1.0f;

            //InvokeRepeating ("ChangeWaveStream", 0.0f, 0.1f);

        }

        private void OnTriggerStay(Collider other)
        {
            Rigidbody targetRigid = other.gameObject.GetComponentInChildren<Rigidbody>();

            if (targetRigid != null)
            {
                nowWaveDir.x += velocity;
                targetRigid.AddTorque(nowWaveDir*power);
                targetRigid.AddForce(nowWaveDir*power); 
                nowWaveDir.x -= velocity;

                //Debug.Log(nowWaveDir);
            }
        }

        private void Update()
        {
            velocity = Mathf.Sin(degree);
            degree += (Time.deltaTime*7.0f);

            if (360.0f < degree)
                degree = 0.0f;

        }
    }
}