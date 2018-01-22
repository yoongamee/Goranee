using System.Linq;
using UnityEngine;
using System.Collections.Generic;
namespace Goranee
{
	public class ColorMinusEffect : MonoBehaviour
	{
		
		private List<Renderer> renderers;
		private Shader colorShader;
		// Use this for initialization
		void Awake()
		{
			Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
			renderers = rs.ToList();
			
			colorShader = Shader.Find("CustomShader/ColorMinusBlend");
			
			if (colorShader == null)
			{
				return;
			}
			On();
		}
		
		public void On()
		{
			for (int i = 0; i < renderers.Count; i++)
			{
				for (int j = 0; j < renderers[i].materials.Length; j++)
				{
					if (renderers[i].materials[j] != null)
					{
						renderers[i].materials[j].shader = colorShader;
					}
					renderers[i].materials[j].mainTexture = renderers[i].materials[j].GetTexture(0);
				}
			}
		}
		
		public void Off()
		{
			for (int i = 0; i < renderers.Count; i++)
			{
				for (int j = 0; j < renderers[i].materials.Length; j++)
				{
					if (renderers[i].materials[j] != null)
					{
						renderers[i].materials[j].shader = Shader.Find("Self-Illumin/Diffuse");
					}
					renderers[i].materials[j].mainTexture = renderers[i].materials[j].GetTexture(0);
				}
			}
		}
		
	}
}

