﻿using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System;

namespace Goranee
{
    public class MsgExtraCameraShake : MessageExtraInfo
    {
        public float Duration { get; set; }
        public float MaxAngle { get; set; }
        public float Amplitude { get; set; }
        public float Count { get; set; }

        public MsgExtraCameraShake()
        {

        }

        public MsgExtraCameraShake(float duration, float maxAngle, float amplitude, float count)
        {
            Set(duration, maxAngle, amplitude, count);
        }

        public void Set(float duration, float maxAngle, float amplitude, float count)
        {
            Duration = duration;
            MaxAngle = maxAngle;
            Amplitude = amplitude;
            Count = count;
        }
    }
}