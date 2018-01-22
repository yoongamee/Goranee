using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Goranee
{
    [RequireComponent(typeof (BoxCollider))]

    public class GenerateZone : MonoBehaviour
    {
        public BoxCollider genZone;
        public List<GameObject> GenObjects;

        public float GeneratedDeltaTime;

        private void Awake()
        {
            genZone.enabled = false;
        }

        private IEnumerator GenerateObject()
        {
            yield return new WaitForSeconds(GeneratedDeltaTime);

            while (true)
            {
                Generate();
                yield return new WaitForSeconds(GeneratedDeltaTime);
            }
        }

        private void OnDisable()
        {
            StopGenerate();
        }

        private void OnEnable()
        {
            StartGenerate();
        }

        public virtual void StopGenerate()
        {
            StopCoroutine("GenerateObject");
        }

        public virtual void StartGenerate()
        {
            StartCoroutine("GenerateObject");
        }

        public virtual bool Generate()
        {
            //Debug.Log ("Generated : in generateZone " + GenObjects.Count);
            if (GenObjects == null || GenObjects.Count == 0)
            {
                return false;
            }
            Vector3 generatedPos;
            generatedPos.x = Random.Range((genZone.size.x*-0.5f), (genZone.size.x*0.5f)) + genZone.center.x +
                             genZone.gameObject.transform.position.x;
            generatedPos.y = Random.Range((genZone.size.y*-0.5f), (genZone.size.y*0.5f)) + genZone.center.y +
                             genZone.gameObject.transform.position.y;
            generatedPos.z = Random.Range((genZone.size.z*-0.5f), (genZone.size.z*0.5f)) + genZone.center.z +
                             genZone.gameObject.transform.position.z;

            return MakeObject(GenObjects[UnityEngine.Random.Range(0, GenObjects.Count)], generatedPos);
        }

        protected virtual bool MakeObject(GameObject obj, Vector3 position)
        {
            GameObject newObject = GameObject.Instantiate(obj) as GameObject;

            if (newObject == null)
            {
                return false;
            }

            newObject.transform.position = position;
            return true;
        }
    }
}