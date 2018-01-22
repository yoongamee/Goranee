using UnityEngine;
using System.Collections.Generic;

namespace Goranee
{
    public class InstantGenerator : GenerateZone
    {
        public int MaxGenerateCount;
        public List<GameObject> generatedObjects = new List<GameObject>();
        protected int generateCount;

        public override bool Generate()
        {
            if (base.Generate() == true)
            {
                generateCount++;
            }
            if (MaxGenerateCount <= generateCount)
            {
                StopGenerate();

                //Destroy (this);
            }
            return true;
        }
        protected override bool MakeObject(GameObject obj, Vector3 position)
        {
            //Debug.Log(obj.name);

            GameObject newObject = null;//Singleton<GameObjectPooler>.Get ().Get ( obj.name ); 

            if (newObject == null)
            {
                return false;
            }
            generatedObjects.Add(newObject);
            newObject.transform.position = position;
            return true;
        }
        public void ClearGeneratedObjects()
        {
            for (int i = 0; i < generatedObjects.Count; i++)
            {
                //Singleton<GameObjectPooler>.Get().Release(generatedObjects[i].name, generatedObjects[i]);
                //Destroy();
            }
            generatedObjects.Clear();
        }
    }
}
