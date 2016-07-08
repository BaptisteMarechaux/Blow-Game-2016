Shader "Custom/TerrainGrassShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue" = "Overlay+100" "RenderType"="Transparent" }
		LOD 100
		Blend SrcAlpha OnMinusSrcAlpha

		Cull off
		ZWrite off

		Pass
		{
			CGPROGRAM
			#pragma target 5.0
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			
			sampler2D _Sprite;
			float4 _Color = float4(0.1f, 0.8f, 0.3f, 1);
			float _Size = float2(1,1);
			float3 _worldPos;
			float3 _Wind = float3(0, 0, 0);

			int _StaticCylinderSpherical = 0;


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct data 
			{
				float3 pos;
			};

			StructuredBuffer<dataW buf_points;

			struct input 
			{
				float4 pos:SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)

			};

			input vert(uint id : SV_VertexID)
			{
				input o;
				o.pos = float4(buf_Points[id].pos + _worldPos, 1.0f);
				return o;
			}

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			float4 RotPoint(float4 p, float3 offset, float3 sideVector, float3 upVector)
			{
				float3 finalPos = p.xyz;

				finalPos += offset.x * sideVector;
				finalPos += offset.y * upVector;

				return float4(finalPos, 1);

			}

			[maxvertexcount(4)]
			void geom(point input p[i], inout TriangleStream<input> triStream)
			{
				float2 half5 = _Size;

				float4 v[4];

				if(_StaticCylinderSpherical == 0)
				{
					v[0] = p[0].pos.xyzw + float4(-half5.x, -half5.y, 0, 0);
					v[1] = p[0].pos.xyzw + float4(-half5.x, half5.y, 0, 0);
					v[2] = p[0].pos.xyzw + float4(half5.x, -half5.y, 0, 0);
					v[3] = p[0].pos.xyzw + float4(half5.x, half5.y, 0, 0);
				}
				else
				{
					float3 up = normalze(float3(0, 1, 0) + (_Wind * .5));
					float3 look = _WorldSpaceCameraPos - p[0].pos.xyz;

					if(_StaticCylinderSpherical == 1)
						look.y = 0;

					look = normalize(look);
					float3 right = normalize(cross(look, up));
					up = normalize(cross(right, look));

					v[0] = RotPoint(p[0].pos.xyzw + float4(-half5.x, -half5.y, 0, 0), right, up);
					v[1] = RotPoint(p[0].pos.xyzw + float4(-half5.x, half5.y, 0, 0), right, up);
					v[2] = RotPoint(p[0].pos.xyzw + float4(half5.x, -half5.y, 0, 0), right, up);
					v[3] = RotPoint(p[0].pos.xyzw + float4(half5.x, half5.y, 0, 0), right, up);
				}

				input pIn;

				pIn.pos = mul(UNITY_MATRIX_VP, v[0]);
				pIn.uv = float2(0.0f, 0.0f);
				UNITY_TRANSFER_FOG(pIn, pIn.pos);
				triStream.Append(pIn);

				
			}	

			ENDCG
		}
	}
}
