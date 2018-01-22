using UnityEngine;
using System.Collections;

public class StaticMeshCombiner : MonoBehaviour
{
	// Use this for initialization
	void Awake () 
    {
        StaticBatchingUtility.Combine(gameObject);
	}
	
}
