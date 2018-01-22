using UnityEngine;
using System.Collections;

namespace Goranee
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance = null;

        protected Singleton()
        {
        }
        public static T Get()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }

    }

}