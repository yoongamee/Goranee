// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "CustomShader/ColorMinusBlend" 
{
	SubShader 
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass 
		{
			LOD 200
 
			CGPROGRAM
 
			#pragma vertex vert 
			#pragma fragment frag
			#pragma target 2.0
			uniform sampler2D _MainTex;    
 
			struct VertexIn
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			struct VertexOut 
			{
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			};
 
			VertexOut vert(VertexIn input) 
			{
				VertexOut output;
  
				output.tex = input.texcoord;
				output.pos = UnityObjectToClipPos(input.vertex);
				return output;
			}
 
			float4 frag(VertexOut input) : COLOR
			{
				float4 diffuse = tex2D(_MainTex, input.tex.xy);  
				return diffuse - saturate(float4(0.5, 0.5, 0.5, 0.2));
			}
			ENDCG
		} 
	}

	FallBack "Diffuse"
}