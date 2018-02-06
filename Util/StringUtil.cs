using System;
using System.Collections.Generic;
using System.Text;

namespace Goranee
{
    public class StringUtil
    {
        protected static StringBuilder stringBuffer = new StringBuilder(2048);

        
        public static string AddString(params string[] strings)
        {
            if (strings == null)
            {
                return string.Empty;
            }

            stringBuffer.Remove(0, stringBuffer.Length);

            for (int i = 0; i < strings.Length; i++)
            {
                stringBuffer.Append(strings[i]);
            }


            return stringBuffer.ToString();
        }

        public static string AddString(List<string> strings)
        {
            if (strings == null)
            {
                return string.Empty;
            }

            stringBuffer.Remove(0, stringBuffer.Length);

            for (int i = 0; i < strings.Count; i++)
            {
                stringBuffer.Append(strings[i]);
            }


            return stringBuffer.ToString();
        }
    }
}