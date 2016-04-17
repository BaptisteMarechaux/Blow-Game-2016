Shader "Custom/TerrainSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_HeightMin ("Height Min", Float) = -1
		_HeightMax ("Height Max", Float) = 1
		_ColorMin ("Tint Color At Min", Color) = (0,0,0,1)
		_ColorMax ("Tint Color At Max", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_SnowLevel("Snow Level", Float)  = 1.0
		_SnowColor("Snow Color", Color) = (0,0,0,1)

		_HighMountLevel("HighMount Level", Float) = 1.0
		_HighMountColor("HighMount Color", Color) = (0,0,0,1)

		_MountLevel("Mount Level", Float) = 1.0
		_MountColor("Mount Color", Color) = (0, 0, 0, 1)

		_LowMountLevel("LowMount Level", Float) = 1.0
		_LowMountColor("LowMount Color", Color) = (0, 0, 0, 1)

		_LandLevel("Land Level", Float) = 1.0
		_LandColor("Land Color", Color) = (0, 0, 0, 1)

		_BeachLevel("BeachLevel", Float) = 1.0
		_BeachColor("Beach Color", Color) = (0, 0, 0, 1)
		 
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

		float _SnowLevel;
		float4 _SnowColor;

		float _HighMountLevel;
		float4 _HighMountColor;

		float _MountLevel;
		float4 _MountColor;

		float _LowMountLevel;
		float4 _LowMountColor;

		float _LandLevel;
		float4 _LandColor;

		float _BeachLevel;
		float4 _BeachColor;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color ;
			float h = (_HeightMax-IN.worldPos.y) / (_HeightMax-_HeightMin);
			fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);

			o.Albedo = c.rgb * tintColor;
			
			if (IN.worldPos.y >= _SnowLevel)
				o.Albedo = _SnowColor;
			if (IN.worldPos.y <= _SnowLevel)
				o.Albedo = lerp(_HighMountColor, _SnowColor, (IN.worldPos.y - _HighMountLevel)/(_SnowLevel - _HighMountLevel));
			if (IN.worldPos.y <= _HighMountLevel)
				o.Albedo = lerp(_MountColor, _HighMountColor, (IN.worldPos.y - _MountLevel)/(_HighMountLevel - _MountLevel));
			if (IN.worldPos.y <= _MountLevel)
				o.Albedo = lerp(_LowMountColor, _MountColor, (IN.worldPos.y - _LowMountLevel)/(_MountLevel - _LowMountLevel));
			if (IN.worldPos.y <= _LowMountLevel)
				o.Albedo = lerp(_LandColor, _LowMountColor, (IN.worldPos.y - _LandLevel)/(_LowMountLevel - _LandLevel));
			if (IN.worldPos.y <= _LandLevel)
				o.Albedo = lerp(_BeachColor, _LandColor, (IN.worldPos.y - _BeachLevel)/(_LandLevel - _BeachLevel));

			o.Albedo = o.Albedo * c.rgb;
			//o.Albedo = c.rgb * tintColor;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
