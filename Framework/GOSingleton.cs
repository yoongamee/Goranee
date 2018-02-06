using UnityEngine;

namespace Goranee
{

    public class GOSingleton<T> where T : MonoBehaviour
    {

        private static T instance = null;
        private static bool isQuit = false;

        public static T Get()
        {
            if (isQuit)
            {
                return null;
            }

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<T>();
                    instance.gameObject.name = instance.GetType().Name;
                    GameObject.DontDestroyOnLoad(instance.gameObject);
                }
            }

            return instance;
        }

        void OnDestroy()
        {
            instance = null;
            isQuit = true;
        }

        public void OnApplicationQuit()
        {
            instance = null;
            isQuit = true;
        }
    }

}
