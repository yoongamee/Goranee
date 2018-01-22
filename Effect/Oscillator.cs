using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
namespace Goranee
{
    public class Oscillator : MonoBehaviour
    {
        public enum AXIS
        {
            Y = 0,
            X = 1,
            Z = 2,
            XZ = 3,
            X_OPPOSITE_Z = 4,
        }

        public AXIS axis = AXIS.Y;


        private Vector3 position;
        public Vector3 originalPosition;

        public float duration = 0.1f;
        public float count = 1.0f;
        public float theta = 0.0f;
        public float amplitude = 10.0f;

        public float elapsedTime = 0.0f;

        public float maxAngle = 360.0f;
        public float magnitude = 0.0f;

        public bool destroy = true;
        // duration 동안 count 번 sin(angle)만큼 움직임
        // amplitude는 기울기, maxangle은 그래프의 범위
        // amplitude는 강도(흔들리면서 이동하게 되는 거리)
        // theta 역할에 대해서 정리 필요
        // 현재 count번 움직이게되면 다시 초기 그래프가 나옴 bug todo

       
        public void Impact()
        {
            duration = 0.0f;
            theta = Mathf.Sin(0.0f);
            elapsedTime = 0.0f;
            maxAngle = maxAngle * count;
            originalPosition = gameObject.transform.localPosition;
        }
        // todo: refactoring
        public virtual void Impact(Vector3 _originalLocalPosition, bool _destroy,
            float _duration, float _maxAngle, float _amplitude, float _count, AXIS _axis = AXIS.Y)
        {
            /*if (duration != 0.0f)
            {
                originalPosition = gameObject.transform.position;
            }*/
            destroy = _destroy;
            originalPosition = _originalLocalPosition;
            duration = 0.0f;
            amplitude = _amplitude;
            elapsedTime = 0.0f;
            axis = _axis;

            maxAngle = _maxAngle * _count;
            duration = _duration;

        }
        protected virtual void UpdateLogic()
        {
            if (duration == 0.0f)
            {
                return;
            }

            elapsedTime += Time.deltaTime * Time.timeScale;
            if (duration < elapsedTime)
            {
                Finish();
                return;
            }
            float remainTime = elapsedTime / duration;

            if (1.0f < remainTime)
            {
                Finish();
                return;
            }
            
            float angle = Mathf.Lerp(0.0f, maxAngle, remainTime);


            magnitude = Mathf.Sin(Mathf.Deg2Rad * (angle + theta));

            //Debug.Log(maxAngle + " , " + angle + " , " +  magnitude);

            magnitude *= amplitude;

            position = originalPosition;
            switch (axis)
            {
                case AXIS.X:
                    {
                        position.x = position.x + magnitude;
                    }
                    break;

                case AXIS.Y:
                    {
                        position.y = position.y + magnitude;
                    }
                    break;
                case AXIS.Z:
                    {
                        position.z = position.z + magnitude;
                    }
                    break;
                case AXIS.XZ:
                    {
                        position.x = position.x + magnitude;
                        position.z = position.z + magnitude;
                    }
                    break;
                case AXIS.X_OPPOSITE_Z:
                    {
                        position.x = position.x + magnitude;
                        position.z = position.z - magnitude;
                    }
                    break;
            }

            gameObject.transform.localPosition = position;
            
        }
        
        protected virtual void Finish()
        {
            duration = 0.0f;
            gameObject.transform.localPosition = originalPosition;
            if (destroy == true)
            {
                DestroyObject(this);
            }
            //Debug.Log("FINISH");
        }
        public virtual void ForceFinish()
        {
            Finish();
        }
    }
}

