// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CurtainEffect" 
{
	Properties 
	{
		_ColorTint ("Color", Color) = (0.2,0.2,0.2,0.9)
		_CoverRateX ("CoverRate X-Axis", Float ) = 1.0
		_CoverRateY ("CoverRate Y-Axis", Float ) = 1.0
		_OperatorX("OperatorX", Float) = 0.0
		_OperatorY("OperatorY", Float) = 1.0
		// operator means bigger = 0, or smaller = 1
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
 			#include "UnityCG.cginc"
 			
 			uniform sampler2D _MainTex;
 			
 			
			sampler2D _MainColor;
			float4 _ColorTint;
			float _Alpha;
			float _CoverRateX;
			float _CoverRateY;
			int	 _OperatorX;
			int	 _OperatorY;
			
			struct fragIN 
			{
                float4 pos			: SV_POSITION;
                float4 texcoord		: TEXCOORD0;
                float4 screenPos	: TEXCOORD1;
                
            };
            	
			fragIN vert(appdata_base v) 
			{
				fragIN o;
				
				o.pos = UnityObjectToClipPos (v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
				o.texcoord = v.texcoord;
				
                return o;
            }

            fixed4 frag(fragIN IN) : SV_Target 
            {
            
            	float2 wcoord = (IN.screenPos.xy / IN.screenPos.w);
            	float4 color = tex2D(_MainTex, IN.texcoord);
            	
            	if ( _OperatorX == 0.0)
            	{
            		if ( wcoord.x < _CoverRateX)
	            	{
	            		if ( _OperatorY == 0.0)
	            		{
	            			if ( wcoord.y < _CoverRateY)
	            			{
	            				color = dot(color.rgb, _ColorTint.rgb);
	            			}
	            		}
	            		else
	            		{
	            			if ( wcoord.y > _CoverRateY)
	            			{
	            				color = dot(color.rgb, _ColorTint.rgb);
	            			}
	            		}
	            	}
            	}
            	else
            	{
            		if ( wcoord.x > _CoverRateX)
	            	{
	            		if ( _OperatorY == 0.0)
	            		{
	            			if ( wcoord.y < _CoverRateY)
	            			{
	            				color = dot(color.rgb, _ColorTint.rgb);
	            			}
	            		}
	            		else
	            		{
	            			if ( wcoord.y > _CoverRateY)
	            			{
	            				color = dot(color.rgb, _ColorTint.rgb);
	            			}
	            		}
	            	}
            	}
            	return color;
            }

			ENDCG
		}
	}
}
