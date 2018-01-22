using UnityEngine;
using System.Collections;

public class BlinkController : MonoBehaviour
{
    private float duration = 0.2f;
    private float elapsedTime = 0.0f;
    
	void Start () 
    {
	    if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader == null)
	    {
	        Destroy(this);
			Debug.Log("null blink");
	    }
		if ( string.Equals(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader.name, "CustomShader/AlphaTexBlend") == true)
	    {
            elapsedTime = 0.0f;
	    }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        elapsedTime += Time.deltaTime * Time.timeScale;
	    if (duration < elapsedTime)
	    {
            Destroy(this);
            return;   
	    }

	    float remainTime = elapsedTime/duration;


	    float alpha = Mathf.Lerp(0.0f, 2.0f, remainTime);
        // for pingpong
        if (1.0f <= alpha)
	    {
	        alpha = 2.0f - alpha;
	    }
        
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_AlphaValue", alpha);
    }

    void OnDisable()
    {
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_AlphaValue", 0.0f);
    }

}
