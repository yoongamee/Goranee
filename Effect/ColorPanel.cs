using UnityEngine;
using System.Collections;
namespace Goranee
{
	public class ColorPanel 
	{
		public float	 coverSpeed = 1.0f;
		public float	 actionTime = 15.0f;
		private Material mat;
		private Shader	 shader;
		
		private float	 startCoverRatioX;
		private float	 startCoverRatioY;
		private float	 endCoverRatioX;
		private float	 endCoverRatioY;
		
		private float	 nowCoverRatioX = -1.0f;
		private float	 nowCoverRatioY = -1.0f;
		
		private float 	 elapsedTime = 0.0f;
		// Use this for initialization
		public void Init () 
		{
			shader = Shader.Find ("Custom/CurtainEffect");
			if ( shader == null)
			{
				Debug.Log("There's no Shader / Custom/CurtainEffect");
			}
			
			mat = new Material (shader);
			
			elapsedTime = 0.0f;
			//SetData (1.0f, 1.0f, 1.0f, 0.0f, 0.5f, 0.0f, 1.0f);
			
		}
		// 0.0 ~ 1.0f
		public void SetData(float startX, float endX, float startY, float endY, float speed, float opX, float opY)
		{
			Debug.Log("setting data..");
			elapsedTime = 0.0f;
			startCoverRatioX = startX;
			endCoverRatioX = endX;
			
			startCoverRatioY = startY;
			endCoverRatioY = endY;
			
			coverSpeed = speed;
			coverSpeed /= actionTime;
			nowCoverRatioX = startCoverRatioX;
			nowCoverRatioY = startCoverRatioY;
			if ( startCoverRatioX <= endCoverRatioX)
			{
				mat.SetFloat ("_CoverRateX", nowCoverRatioX);
			}
			else
			{
				mat.SetFloat ("_CoverRateX", 1.0f - nowCoverRatioX);
			}
			if ( startCoverRatioY <= endCoverRatioY)
			{
				mat.SetFloat ("_CoverRateY", nowCoverRatioY);
			}
			else
			{
				mat.SetFloat ("_CoverRateY", 1.0f - nowCoverRatioY);
			}
			
			
			mat.SetFloat ("_OperatorX", opX);
			mat.SetFloat ("_OperatorY", opY);
			
			Camera.main.RenderWithShader(shader, "RenderType");
			//StartCoroutine ("Process");
		}
		/*IEnumerator Process()
	{
		while (nowCoverRatioX != endCoverRatioX || nowCoverRatioY != endCoverRatioY) 
		{
			elapsedTime += Time.deltaTime;
			
			if ( startCoverRatioX <= endCoverRatioX)
			{
				nowCoverRatioX = Mathf.Lerp(startCoverRatioX, endCoverRatioX, elapsedTime * coverSpeed);
			}
			else
			{
				nowCoverRatioX = Mathf.Lerp(endCoverRatioX, startCoverRatioX, elapsedTime * coverSpeed);
				nowCoverRatioX = 1.0f - nowCoverRatioX;
			}
			
			if ( startCoverRatioY <= endCoverRatioY)
			{
				nowCoverRatioY = Mathf.Lerp(startCoverRatioY, endCoverRatioY, elapsedTime * coverSpeed);
			}
			else
			{
				nowCoverRatioY = Mathf.Lerp(endCoverRatioY, startCoverRatioY, elapsedTime * coverSpeed);
				nowCoverRatioY = 1.0f - nowCoverRatioY;
			}
			
			mat.SetFloat ("_CoverRateX", nowCoverRatioX);
			mat.SetFloat ("_CoverRateY", nowCoverRatioY);

			yield return null;
		}


		yield return null;
	}*/
		public void Update () 
		{
			if (nowCoverRatioX != endCoverRatioX || nowCoverRatioY != endCoverRatioY) 
			{
				elapsedTime += Time.deltaTime;
				
				if ( startCoverRatioX <= endCoverRatioX)
				{
					nowCoverRatioX = Mathf.Clamp(Mathf.Lerp(endCoverRatioX, startCoverRatioX, elapsedTime * coverSpeed), 0.0f, 1.0f);
				}
				else
				{
					nowCoverRatioX = Mathf.Clamp(Mathf.Lerp(endCoverRatioX, startCoverRatioX, elapsedTime * coverSpeed), 0.0f, 1.0f);
					nowCoverRatioX = 1.0f - nowCoverRatioX;
				}
				
				if ( startCoverRatioY <= endCoverRatioY)
				{
					nowCoverRatioY = Mathf.Clamp(Mathf.Lerp(endCoverRatioY, startCoverRatioY, elapsedTime * coverSpeed), 0.0f, 1.0f);
				}
				else
				{
					nowCoverRatioY = Mathf.Clamp(Mathf.Lerp(endCoverRatioY, startCoverRatioY, elapsedTime * coverSpeed), 0.0f, 1.0f);
					nowCoverRatioY = 1.0f - nowCoverRatioY;
				}
				
				
				mat.SetFloat ("_CoverRateX", nowCoverRatioX);
				mat.SetFloat ("_CoverRateY", nowCoverRatioY);
				
				
			}
			
		}
		
		public void OnRenderImage (RenderTexture source, RenderTexture destination) 
		{ 	
			
			if (mat != null) 
			{
				mat.mainTexture = source;
				Graphics.Blit (source, destination, mat);
			}
		}
	}
}

