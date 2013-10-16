Shader "UnityCookie/1 - Flat Vertex" {
	Properties {
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//user defined variables
			
			//Base Input Structs
			struct vertexInput{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 vertCol : COLOR;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.vertCol = v.color;
				return o;
			}
			
			//fragment function
			float4 frag(vertexOutput i) : COLOR
			{
				return i.vertCol;
			}
			ENDCG
		}
	}
	//fallback commented out during development
	//Fallback "Diffuse"
}