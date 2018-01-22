using UnityEngine;
using System.Collections;

namespace Goranee
{
    public class TimeUtil
    {
        public static string GetTimeFromSecond(int seconds)
        {
            int hours = seconds / 3600;
            int minute = seconds % 3600 / 60;
            int second = seconds % 3600 % 60;
           
            return StringUtil.AddString( string.Format("{0:D2}", hours)+ ":" + string.Format("{0:D2}", minute) + ":" +
                string.Format("{0:D2}", second));
        }
    }
}