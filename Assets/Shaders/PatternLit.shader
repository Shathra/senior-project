//Under Construction
Shader "PatternLit" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_TexScale ("Texture Scale", Float) = 1.0
	}
	SubShader {
		Pass {
			Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float _TexScale;

			struct vin {
				float4 pos : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct vout {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
			};

            vout vert(vin v) {
				vout o;
				o.posWorld = mul(_Object2World, v.pos);
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.tex = v.texcoord;
				return o;
            }

            float4 frag(vout v) : COLOR {
				float scale = 1.0f / _TexScale;
				float x = fmod(v.posWorld.x * scale, 1.0f);
				float y = fmod(v.posWorld.y * scale, 1.0f);
				if(x < 0.0)
					x += 1.0f;
				if(y < 0.0)
					y += 1.0f;
				float4 col = tex2D (_MainTex, float2(x, y)).rgba;
				return col;
            }

            ENDCG
        }
	} 
	Fallback "Diffuse"
}  