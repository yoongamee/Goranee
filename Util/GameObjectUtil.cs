
using UnityEngine;

namespace Goranee
{
    public class GameObjectUtil
    {
        public void ChangeLayerWithChild(GameObject target, string layerName)
        {
            target.layer = LayerMask.NameToLayer(layerName);
            foreach (Transform child in target.transform)
            {
                ChangeLayerWithChild(child.gameObject, layerName);
            }
        }
        public GameObject LoadGameObject(string path, Vector3 position, Quaternion rotation)
        {
            GameObject newGameObject = LoadGameObject(path);
            if (newGameObject != null)
            {
                CopyPosRotation(newGameObject.transform, position, rotation);
            }

            return newGameObject;
        }

        public Texture LoadTexture(string path)
        {
            /*
         * UnityEngine.Object original = Resources.Load(path);
        if (original != null)
        {
            newGameObject = GameObject.Instantiate(original) as GameObject;
        }

         * */
            return Resources.Load(path) as Texture;
        }

        public GameObject LoadGameObject(string path, Vector3 position)
        {
            GameObject newGameObject = LoadGameObject(path);
            if (newGameObject != null)
            {
                CopyPos(newGameObject.transform, position);
            }

            return newGameObject;
        }

        public GameObject LoadGameObject(string path)
        {
            if (string.IsNullOrEmpty(path) == true)
            {
                return null;
            }
            GameObject newGameObject = null;

            UnityEngine.Object original = Resources.Load(path);
            if (original != null)
            {
                newGameObject = GameObject.Instantiate(original) as GameObject;
            }

            return newGameObject;
        }

        public void SetTransform(Transform target, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            if (target == null)
                return;

            target.position = position;
            target.rotation = rotation;
            target.localScale = scale;
        }

        public void CopyPosRotation(Transform target, Transform source)
        {
            CopyPos(target, source.position);
            CopyRot(target, source.rotation);
        }

        public void CopyPos(Transform target, Vector3 position, Space space = Space.Self)
        {
            if (target == null)
                return;

            target.position = position;
        }

        public void CopyRot(Transform target, Quaternion quat, Space space = Space.Self)
        {
            if (target == null)
                return;

            target.Rotate(quat.eulerAngles, space);
        }

        public void CopyPosRotation(Transform target, Vector3 sourceVector, Quaternion sourceQuaternion)
        {
            CopyPos(target, sourceVector);
            CopyRot(target, sourceQuaternion);
        }

        public void AttachGO(Transform dest, Transform source, Vector3 position, Quaternion rotation, Vector3 scale,
            Space space = Space.Self)
        {
            if (source == null)
                return;

            if (dest == null)
            {
                source.parent = null;
            }
            else
            {
                source.parent = dest.transform;
            }
            if (space == Space.Self)
            {
                source.transform.localPosition = position;
                source.transform.localRotation = rotation;
            }
            else
            {
                source.transform.position = position;
                source.transform.rotation = rotation;
            }
            source.transform.localScale = scale;

        }

        public void SetInitValue(Transform transform, bool worldPos, bool localPos, bool scale, bool worldRot, bool localRot)
        {
            if (transform == null)
            {
                return;
            }
            if (worldPos == true)
            {
                transform.position = Vector3.zero;
            }
            if (localPos == true)
            {
                transform.localPosition = Vector3.zero;
            }
            if (scale == true)
            {
                transform.localScale = Vector3.one;
            }
            if (worldRot == true)
            {
                transform.rotation = Quaternion.identity;
            }
            if (localRot == true)
            {
                transform.localRotation = Quaternion.identity;
            }
        }

        public void CopyTransform(Transform dest, Transform source)
        {
            if (dest == null || source == null)
            {
                return;
            }
            dest.position = source.position;
            dest.localPosition = source.position;
            dest.rotation = source.rotation;

        }
        public void AttachGO(Transform dest, Transform source)
        {
			//Vector3 localScale = source.localScale;
			//Vector3 localPosition = source.localPosition;

            //Debug.Log(source.gameObject.name + " , " + source.gameObject.layer);

            if (source == null)
                return;

            if (dest == null)
            {
                source.parent = null;
            }
            else
            {
                source.parent = dest.transform;
            }
            source.transform.localPosition = Vector3.zero;
            source.transform.localRotation = Quaternion.identity;
            // 이것때문에 유니티에서 버그 발생 invalid AABB a
            //source.localPosition = localPosition;
            //source.localScale = localScale;

        }

        public bool DetachParentGO(Transform sourceNode)
        {
            if (sourceNode == null)
                return false;

            sourceNode.parent = null;
            return true;
        }
        public void DetachGO(Transform node, string detachNodeName, bool destroy)
        {
            if (string.IsNullOrEmpty(detachNodeName) == true)
                return;

            if (node == null)
                return;

            Transform releaseNode = node.transform.Find(detachNodeName);

            if (releaseNode == null)
                return;

            releaseNode.parent = null;

            if (destroy == true)
            {
                GameObject.DestroyImmediate(releaseNode.gameObject);
            }
        }

        // goto gameobject pool
        public void ReleaseGO(Transform node, string objectPoolKey, string releaseNodeName)
        {
            if (string.IsNullOrEmpty(releaseNodeName) == true)
                return;

            if (string.IsNullOrEmpty(objectPoolKey) == true)
                return;

            if (node == null)
                return;

            Transform releaseNode = node.transform.Find(releaseNodeName);

            if (releaseNode == null)
                return;

        }

        public bool IsinNode(GameObject node, string objectName)
        {
            if (string.IsNullOrEmpty(objectName) == true)
                return false;

            if (node == null)
                return false;

            if (node.transform.Find(objectName) == null)
                return false;

            return true;
        }

        public Transform GetNode(GameObject node, string objectName)
        {
            if (string.IsNullOrEmpty(objectName) == true)
                return null;

            if (node == null)
                return null;

            return node.transform.Find(objectName);
        }
        public void ChangeLayers(GameObject go, string name)
        {
            Debug.Log(go.name);
            if (go == null)
            {
                Debug.Log("thsrss");
                return;
            }
            Transform[] transforms = go.GetComponentsInChildren<Transform>();
            if (transforms == null)
            {
                Debug.Log("thsrss11");
                return;
            }
            if (transforms.Length == 0)
            {
                Debug.Log("thsrss3311");
                return;
            }
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].gameObject.layer = LayerMask.NameToLayer(name);
                Debug.Log(transforms[i].name);
            }
        }

    }
}