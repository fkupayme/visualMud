Shader "UnityCookie/Normal Mapped Vertex Color Blend" {
	Properties {
		_Color ("Color Tint", Color) = (1.0,1.0,1.0,1.0)
	    _MainTex ("Diffuse Texture, Gloss(A)", 2D) = "white" {}
	    _BlendTex ("Blend Texture, Gloss(A)", 2D) = "white" {}
        _BumpMap ("Bump Map", 2D) = "bump" {}
		_VertStrength("Vert Color Strength",Range(0.0,2.0)) = 1.0
      	_SpecColor ("Specular Color", Color) = (1,1,1,1)
      	_Shininess ("Shininess", Float) = 10
	}
	SubShader {
		Pass {
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			
			//user defined variables
			uniform sampler2D _MainTex;
			uniform fixed4 _MainTex_ST;
			uniform sampler2D _BlendTex;
			uniform fixed4 _BlendTex_ST;
            uniform sampler2D _BumpMap;
            uniform fixed4 _BumpMap_ST;
			uniform fixed4 _Color;
	        uniform fixed4 _SpecColor; 
	        uniform half _Shininess; 
	        uniform fixed _VertStrength;
			
			//Unity defined Variables
         	uniform fixed4 _LightColor0; 
			
			//Base Input Structs
			struct vertexInput{
				half4 vertex : POSITION;
            	half3 normal : NORMAL;
				fixed4 texcoord : TEXCOORD0;
				half4 tangent : TANGENT;
				half4 colors : COLOR;
			};
			struct vertexOutput {
				half4 pos : SV_POSITION;
				half4 tex : TEXCOORD0;
				fixed4 lightDirection : TEXCOORD1;
				fixed3 viewDirection : TEXCOORD2;
				fixed3 normalWorld : TEXCOORD3;
				fixed3 tangentWorld : TEXCOORD4;
				fixed3 binormalWorld : TEXCOORD5;
				half3 vertexColor : COLOR;
			};
			
			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				
				o.normalWorld = normalize( mul( half4( v.normal, 0.0 ), _World2Object ).xyz );
				o.tangentWorld = normalize( mul( _Object2World, v.tangent ).xyz );
				o.binormalWorld = normalize( cross(o.normalWorld, o.tangentWorld) * v.tangent.w );
				
				half4 posWorld = mul(_Object2World, v.vertex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				
				o.viewDirection = normalize( _WorldSpaceCameraPos.xyz - posWorld.xyz );
				
				half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
				
				o.lightDirection = fixed4(
					normalize( lerp(_WorldSpaceLightPos0.xyz , fragmentToLightSource, _WorldSpaceLightPos0.w) ),
					lerp(1.0 , 1.0/length(fragmentToLightSource), _WorldSpaceLightPos0.w)
				);
	            o.vertexColor = v.colors.xyz;
	            return o;
			}
			
			//fragment function
			fixed4 frag(vertexOutput i) : COLOR
			{
	 			
	 			//Texture Maps
	 			fixed4 tex;
	 			if (i.vertexColor.x > 0.5){
	 				tex = tex2D(_MainTex, _MainTex_ST.xy * i.tex.xy + _MainTex_ST.zw);
	 			}
	 			else{
	 				tex = tex2D(_BlendTex, _BlendTex_ST.xy * i.tex.xy + _BlendTex_ST.zw);
	 			}
                fixed4 texN =  tex2D (_BumpMap, _BumpMap_ST.xy * i.tex.xy + _BumpMap_ST.zw);
                
                //unpackNormal function
	            half3 localCoords = fixed3(2.0 * texN.ag - fixed2(1.0, 1.0), 1.0);
	 
	            float3x3 local2WorldTranspose = float3x3(
	               i.tangentWorld, 
	               i.binormalWorld, 
	               i.normalWorld);
	            fixed3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));
 
	 			//vertex overlay
	 			//tex.xyz = lerp(tex.xyz,tex.xyz*(i.vertexColor.xyz*2),_VertStrength);
	 			
	 			//Lighting
	 			//calculate things once only
	 			fixed nDotL = max(0.0, dot(normalDirection, i.lightDirection.xyz));
	 			
	 			fixed3 ambientLight = UNITY_LIGHTMODEL_AMBIENT.xyz;
	            fixed3 diffuseReflection = i.lightDirection.w * _LightColor0.xyz * nDotL;
	 			fixed3 specularReflection = nDotL * i.lightDirection.w * _LightColor0.xyz * _SpecColor.xyz * pow(max(0.0,dot(reflect(-i.lightDirection.xyz, normalDirection), i.viewDirection)), _Shininess);
	 			fixed3 lightFinal = ambientLight + diffuseReflection + (specularReflection * tex.a);
	 			return fixed4(tex.xyz * lightFinal * _Color.xyz, 1.0);
	 			//return fixed4(i.vertexColor, 1.0);
			}
			ENDCG
		}
	}
	//fallback commented out during development
	//Fallback "Diffuse"
}