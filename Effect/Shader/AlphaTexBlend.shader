Shader "CustomShader/AlphaTexBlend" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha Tex (RGB)", 2D) = "white" {}
		_AlphaValue ("Alpha Value (float)", Range(0.0 ,1.0) ) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert 
		#pragma target 2.0
		
		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float	  _AlphaValue;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AlphaTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
		 {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 c1 = tex2D (_AlphaTex, IN.uv_AlphaTex);
			o.Albedo = (c.rgb * (1.0 - _AlphaValue) ) + (c1.rgb * _AlphaValue);

			o.Alpha = _AlphaValue;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
