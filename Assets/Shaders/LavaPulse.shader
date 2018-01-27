// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Standard" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RoughnessTex("Roughness", 2D) = "black" {}
		_Normal("Normal", 2D) = "bump" {}
		_OcclusionTex("Occlusion", 2D) = "white" {}
		_EmissionTex("Emission", 2D) = "black" {}
		_Emission("Emission", Float) = 0.0
		_EmissionColor("Emission Color", Color) = (1,1,1,1)
		_PulseTime("Pulse Time",Int) = 1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows
#include "UnityPBSLighting.cginc"
		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RoughnessTex;
		sampler2D _Normal;
		sampler2D _OcclusionTex;
		sampler2D _EmissionTex;

	struct Input {
		float2 uv_MainTex;
		float2 uv_RoughnessTex;
		float2 uv_Normal;
		float2 uv_OcclusionTex;
		float2 uv_EmissionTex;

	};

	half _Smoothness;
	half _Emission;
	fixed4 _Color;
	fixed4 _EmissionColor;
	int _PulseTime;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)



		void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));;
			fixed4 r = tex2D(_RoughnessTex, IN.uv_RoughnessTex);
			o.Smoothness = 1 - r.r;
			fixed4 ao = tex2D(_OcclusionTex, IN.uv_OcclusionTex);
			o.Occlusion = ao;
			o.Alpha = c.a;
			half impulse = sin(_PulseTime * _Time[0]);
			o.Emission = impulse * impulse  * _Emission * _EmissionColor * tex2D(_EmissionTex, IN.uv_EmissionTex);
		}
	ENDCG
	}
		FallBack "Diffuse"
}