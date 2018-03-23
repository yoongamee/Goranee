using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public class Camera2DOrthoCalculator : MonoBehaviour
    {

        public float DesiredWidth = 720;
        public float DesiredHeight = 1280;

        void Awake()
        {
            /*
                Camera Size = x / (((x / y) * 2) * s)
                Where:
                    x = Screen Width(px)
                    y = Screen Height(px)
                    s = Desired Height of Photoshop Square (px)
            */

            Camera cam = GetComponent<Camera>();
            if (cam != null)
            {
                cam.orthographicSize = DesiredWidth / ((DesiredWidth / DesiredHeight) * 2);
            }
        }
    }
}