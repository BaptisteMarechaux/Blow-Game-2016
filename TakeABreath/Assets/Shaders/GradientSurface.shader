Shader "Custom/GradientSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_WaterColor("Water Color", Color) = (0.3, 0.3, 0.78, 1)
		_WaterHeight("Water Height", float) = 0.1
		_HeightScale("Height Scale", float) = 1
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		 _HeightMin ("Height Min", Float) = -1
		 _HeightMax ("Height Max", Float) = 1
		 _ColorMin ("Tint Color At Min", Color) = (0,0,0,1)
		 _ColorMax ("Tint Color At Max", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _ColorMin;
		 fixed4 _ColorMax;
		 float _HeightMin;
		 float _HeightMax;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void vert (inout appdata_full v, out Input o) {

			o.worldPos = mul(UNITY_MATRIX_MVP, v.vertex);
			if(o.worldPos.y < 1) {
				float phase = _Time * 20.0;
				float offset = (o.worldPos.x + (o.worldPos.z * 0.2)) * 0.5;
				o.worldPos.y = sin(phase + offset) * 0.2;
			}

		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color ;
			float h = (_HeightMax-IN.worldPos.y) / (_HeightMax-_HeightMin);
			fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);
			o.Albedo = c.rgb * tintColor;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
