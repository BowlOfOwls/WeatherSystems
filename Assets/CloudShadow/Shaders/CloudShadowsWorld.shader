﻿Shader "Projector/CloudShadowsWorld" {
	Properties {
		[Header(Opacity)]
		_Opacity ("Color", Color) = (0.5,0.5,0.5,1.0)
		[Header(Textures and Size)]
		[NoScaleOffset]
		_ShadowTex ("Shadow Texture", 2D) = "black" { }
		_ShadowTiling ("Tiling", Float) = 0.002
		[NoScaleOffset]
		_NoiseTex ("Distortion Texture", 2D) = "black" { }
		_NoiseTiling ("Tiling", Float) = 0.002
		[Header(Movement)]
		_SpeedX ("SpeedX", Float) = 1
		_SpeedY ("SpeedY", Float) = 1
		[Header(Height and Distance)]
		_EdgeBlend ("Edge Blend", Range(0.1,100)) = 5.0
		_Height ("Height", Float) = 0.0
		_Distance ("Distance Fade", Float) = 500.0
		_DistanceBlend ("Distance Blend", Range(0.1,5000)) = 500.0
	 }
	 
	 Subshader {
 		Tags { "RenderType"="Transparent" "Queue"="Transparent+200" }
		Pass {
			ZWrite Off
			//Offset -1, -1
			Blend DstColor Zero

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
		 
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uvNoise : TEXCOORD1;
				float3 wPos : TEXCOORD2; // added for height comparisons.
				UNITY_FOG_COORDS(3)
			};

			float4x4 unity_Projector;
			sampler2D _ShadowTex;
			sampler2D _NoiseTex;
			float4 _ShadowTex_ST;
			float4 _NoiseTex_ST;
			float _ShadowTiling;
			float _NoiseTiling;
			float4 _Opacity;
			float _SpeedX;
			float _SpeedY;
			float _Height;
			float _EdgeBlend;
			float _Distance;
			float _DistanceBlend;

			v2f vert (appdata_tan v) {
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.uv = TRANSFORM_TEX (mul (unity_Projector, v.vertex).xy, _ShadowTex);
				o.uvNoise = TRANSFORM_TEX (mul (unity_Projector, v.vertex).xy, _NoiseTex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
		 
			fixed4 frag (v2f i) : COLOR {
				float4 noiseDistortion = tex2D(_NoiseTex, (float2(i.wPos.x*_NoiseTiling+frac(_Time.x*-_SpeedX),i.wPos.z*_NoiseTiling+frac(_Time.x*-_SpeedY))));
				fixed4 c = tex2D(_ShadowTex, fixed2(i.wPos.x*_ShadowTiling + noiseDistortion.x + frac(_Time.x*-_SpeedX),i.wPos.z*_ShadowTiling + noiseDistortion.y + frac(_Time.x*-_SpeedY)));
				c = clamp(lerp(1,c,(i.wPos.y-_Height)/ _EdgeBlend),0,1);
				float dist = distance(_WorldSpaceCameraPos, i.wPos);
				float distDiff = saturate((dist-_Distance)/_DistanceBlend);
				if (dist>_Distance)
					c = lerp(c-distDiff,2,distDiff);
				c = saturate(c+_Opacity);
				UNITY_APPLY_FOG_COLOR(i.fogCoord, c, fixed4(1,1,1,1));
				return c;
			}
			ENDCG
		}
	}
}