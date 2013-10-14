Shader "sublime\tut0"
{
	Properties{
		_Color ("_Color", Color) = (0.5,0.5,0.5,0.5)
	}
	 
	SubShader{  
		Pass {
			Tags { "LightMode" = "ForwardBase"} 
			CGPROGRAM
			#pragma vertex perVertex
			#pragma fragment perPixel
			 
			uniform float4 _Color;
			uniform float4 _LightColor0;
			
			struct perVertexInput{
				float4 vertex : POSITION;
				float3 normal : NORMAL; 
			};
			
			struct perPixelInput{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};   
			
			perPixelInput perVertex(perVertexInput i)
			{
				perPixelInput o; 
				 
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				
				 
				float3 normalDirection = normalize(mul(float4(i.normal,0.0),_World2Object).xyz);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 light =  _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection)) + UNITY_LIGHTMODEL_AMBIENT.xyz;
				
				o.col = float4(light * _Color.rgb,1.0);
				
				
				return o;
			}
			
			float4 perPixel(perPixelInput i) : COLOR
			{ 
				return i.col;
			}
			
			ENDCG
		}
	}
}