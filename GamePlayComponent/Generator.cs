using UnityEngine;
using System.Collections;

namespace Goranee
{
    public class Generator : MonoBehaviour
    {
        public float generateTime = 0.5f;
        public float genMoveSpeed;
        protected float lastGenerateTime = 0.0f;

        public BoxCollider genBoundary;
        public GameObject Block;

        public MeshFilter genPosition;
        public Vector3 targetPosition;

        public Vector3 dirForce;
        public float Force;

        // Update is called once per frame
        private void UpdateLogic()
        {
            if (lastGenerateTime + generateTime < Time.fixedTime)
            {
                lastGenerateTime = Time.fixedTime;
                Generate();
            }

            genPosition.transform.localPosition = Vector3.Lerp(genPosition.transform.localPosition, targetPosition,
                Time.deltaTime*genMoveSpeed);

            if (Vector3.Distance(genPosition.transform.localPosition, targetPosition) < 0.1f)
            {
                NewGeneratePos();
            }
        }

        private void NewGeneratePos()
        {
            Vector3 generatedPos;
            generatedPos.x = Random.Range((genBoundary.size.x*-0.5f), (genBoundary.size.x*0.5f)) + genBoundary.center.x;
            generatedPos.y = Random.Range((genBoundary.size.y*-0.5f), (genBoundary.size.y*0.5f)) + genBoundary.center.y;
            generatedPos.z = 0.0f;
                //Random.Range((genPosition.size.z * -0.5f), (genPosition.size.z * 0.5f)) + genPosition.center.z;

            targetPosition = generatedPos;
        }

        private void Generate()
        {
            //lastGeneratedBlock = null;

            GameObject newObject = GameObject.Instantiate(Block) as GameObject;


            newObject.transform.position = genPosition.transform.position;

            Rigidbody chrRigidBody = newObject.GetComponent<Rigidbody>();

            if (chrRigidBody == null)
            {
                Destroy(newObject);
            }
            //chrRigidBody.AddForce( dirForce.dirForce * 250.0f);
            chrRigidBody.AddForce(dirForce*Force);
            /*else 
		{
			lastGeneratedBlock = newObject.transform;
			blocks.Insert( blocks.Count, newObject);
			if ( 6 < blocks.Count )
			{
				ClearBlocks(0, blocks.Count - 6);
			}
			
			
			chrRigidBody.AddForce( Vector3.down * force);
		}*/
        }
    }
}