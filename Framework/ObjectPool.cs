using System.Collections.Generic;

// TValue는 항상 1개 이상 유지해야 함. 
// 그렇지 않으면 subclass의 Type을 알아 올 수가 없음.
// 풀링될 오브젝트는 모두 new()를 가지고 있어야 함.
// new에서 createinstance로 바꾼건. subclass의 type을 알아야 제대로 된 생성자를 호출 할 수 있기 때문.
using UnityEngine;

namespace Goranee
{

    public abstract class ObjectPool<TKey, TValue> where TValue : class
    {
        protected Dictionary<TKey, Stack<TValue>> dicObj;
        public ObjectPool()
        {
            dicObj = new Dictionary<TKey, Stack<TValue>>();
        }
        protected abstract void PushValue(TKey key, Stack<TValue> tObjects);

        public virtual TValue Get(TKey iKey)
        {
            if (dicObj.ContainsKey(iKey) != true)
            {
                Add(iKey, 1);
            }

            Stack<TValue> tObjects = null;
            dicObj.TryGetValue(iKey, out tObjects);

            if (tObjects.Count == 0)
                Add(iKey, 1);

            if (tObjects.Count == 0)
            {
                return null;
            }
            return tObjects.Pop();
        }

        virtual public void Add(TKey iKey, int iCount)
        {
            if (dicObj.ContainsKey(iKey) != true)
            {
                dicObj.Add(iKey, new Stack<TValue>());
            }
            Stack<TValue> tObjects = null;
            dicObj.TryGetValue(iKey, out tObjects);

            for (int i = 0; i < iCount; i++)
            {
                PushValue(iKey, tObjects);
            }
        }

        public virtual void Release(TKey iKey, TValue iValue)
        {
            if (dicObj.ContainsKey(iKey) == true)
            {
                Stack<TValue> tObjects = null;
                dicObj.TryGetValue(iKey, out tObjects);

                tObjects.Push(iValue);
            }

        }
        public virtual void Destroy()
        {
            dicObj.Clear();
        }

        public virtual void Print()
        {
            var enumrator = dicObj.GetEnumerator();
            int count = 0;
            while (count <= dicObj.Count)
            {
                enumrator.MoveNext();
                if (enumrator.Current.Value != null)
                {
					Debug.Log(enumrator.Current.Key + " : " + enumrator.Current.Value.Count);
                }
                count++;
            }
        }

    }

}