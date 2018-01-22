using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Goranee
{
    [System.Serializable]
    public class GameObjectPooler : ObjectPool<string, UnityEngine.GameObject>
    {
        public string Path = "Prefab/";
        /*{ 
            get { return Path; }
            set { Path = string.Copy(value); }
        }
        */
        protected override void PushValue(string iKey, Stack<UnityEngine.GameObject> tObjects)
        {
            // todo: string에 path 관련된것이 있으면 다 제거하자.
            GameObject newObject = Singleton<GameObjectUtil>.Get().LoadGameObject(StringUtil.AddString(Path, iKey));
            if (newObject == null)
            {
              //  Debug.Log("Create Object false : " + iKey + " , " + newObject.GetHashCode());
                return;
            }
			//Debug.Log ("Create Object : " + iKey + " , " + newObject.GetHashCode());

            var pos = newObject.name.IndexOf("(");
            newObject.name = newObject.name.Substring(0, pos);
            newObject.SetActive(false);
            tObjects.Push(newObject);
			//Print (iKey);
        }
        public void RemoveAll(string iKey)
        {
            if (dicObj.ContainsKey(iKey) == true)
            {
                Stack<GameObject> tObjects = null;
                dicObj.TryGetValue(iKey, out tObjects);
                int maxCount = tObjects.Count;
                for (int i=0; i< maxCount; i++)
                {
                    GameObject remObject = tObjects.Pop();
                    if ( remObject != null)
                    {
                        GameObject.DestroyImmediate(remObject);
                    }
                }
            }
        }
        public override void Release(string iKey, GameObject iValue)
        {
            if (iValue == null || string.IsNullOrEmpty(iKey) == true)
            {
                return;
            }
            iValue.transform.localPosition = Vector3.right * 100000.0f;
            // todo : 수정해야함 버그의 요지가 있음.
            if (iValue.activeInHierarchy == true)
            {
                iValue.transform.parent = null;        
            }
            
			//Debug.Log (iKey + " RELEASE: " + iValue.GetInstanceID() + " , " + iValue.GetHashCode());
            iValue.SetActive(false);

            base.Release(iKey, iValue);
			//Print (iKey);
        }

        public override GameObject Get(string iKey)
        {
            if (string.IsNullOrEmpty(iKey) == true)
            {
                return null;
            }
            GameObject newGO = base.Get(iKey);
			//Print (iKey);
            if (newGO == null)
            {
                return null;
            }
			//Debug.Log (iKey + " GET: " + newGO.GetInstanceID() + " , " + newGO.GetHashCode());
            newGO.SetActive(true);
            return newGO;
        }
        public GameObject Get(string path, string iKey)
        {
			
            Path = path;
			GameObject newObj = Get(iKey);
			//Print (iKey);
            
			/*if (newObj == null) {
				Debug.Log (iKey + " Breed: ERROR");
			} else {
                

                Debug.Log (iKey + " GET1: " + newObj.GetInstanceID () + " , " + newObj.GetHashCode () + " , " + dicObj[iKey].Count);
			}
            if (newObj == null)
            {
                Debug.Log(iKey + " Breed: ERROR");
                return null;
            }*/
			return newObj;
        }
		public virtual void Print(string iKey)
		{

			if (dicObj.ContainsKey(iKey) != true)
			{
				Debug.Log (iKey + " NO Stack");
			}
			Stack<GameObject> tObjects = null;
			dicObj.TryGetValue(iKey, out tObjects);
			if (tObjects == null) 
			{
				Debug.Log (iKey + " EMPTY Stack");
			}
			//for (int i = 0; i < tObjects.Count; i++)
			{
				Debug.Log (iKey + " : " + tObjects.Count);
			}
		}
    }

}
