// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShader"
{
	Properties
	{
		_MainTexture("Main Colour (RGB) Hello!", 2D) = "white" {}
		_Colour("Colour", Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction

			#include "UnityCG.cginc"
			
			
			struct appdata 
			{
			// Float4 - 4 values, 1,1,1,1
			// Float2 - 2 values, 2,3
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 position: SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			float4 _Colour;
			sampler2D _MainTexture;
			
			v2f vertexFunction(appdata IN)
			{
				v2f OUT;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				
				return OUT;
			}
			
			fixed4 fragmentFunction(v2f IN) : SV_Target
			{
				float4 textureColor = tex2D(_MainTexture, IN.uv);
				
				return textureColor * _Colour;
			}
			
			//Vertex
			// Build Objects

			//Fragment
			// Colour it in

			ENDCG
		}
	}
}