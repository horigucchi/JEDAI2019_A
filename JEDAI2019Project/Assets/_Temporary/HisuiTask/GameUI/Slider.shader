Shader "Custom/Slider"
{
	Properties
	{
		_MainTex("_MainTex", 2D) = "white" {}
		Colorize("Colorize", Range(0.0, 1.0)) = 1
	}
	
	SubShader
	{
		Pass
		{
			Name "SliderShader"

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex   vertex
			#pragma fragment  fragment
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			sampler2D _MainTex;// テクスチャ画像
			
			struct Out
			{
				float4 vertex   : POSITION;
				float4 texcoord : TEXCOORD0;
				float4 color    : COLOR;
			};

			struct Input
			{
				float4  pos : SV_POSITION;
				float2  uv : TEXCOORD0;
				float4  color : COLOR;
			};

			Input vertex(Out  v)
			{
				Input result;
				result.pos = UnityObjectToClipPos(v.vertex);
				result.uv = v.texcoord.xy;
				result.color = v.color;
				return result;
			}

			float4 fragment(Input i) : COLOR
			{
				return float4(1, i.uv, 1);
			}
			ENDCG
		}
	}
}