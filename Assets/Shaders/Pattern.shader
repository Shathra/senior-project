Shader "Pattern" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", COLOR) = (1,1,1,1)
		_TexScale ("Texture Scale", Float) = 1.0
		[MaterialToggle] _isVertical("Is Vertical", Float) = 0
		[MaterialToggle] _isHorizontal("Is Horizontal", Float) = 0
	}
	SubShader {
		ZWrite Off
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float _TexScale;
			uniform float _isVertical;
			uniform float _isHorizontal;
			uniform float4 _Color;

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
				float x, y;
				if(_isHorizontal > 0) {
					x = fmod(v.posWorld.x * scale, 1.0f);
				} else
					x = v.tex.x;

				if(_isVertical > 0) {
					y = fmod(v.posWorld.y * scale, 1.0f);
				} else
					y = v.tex.y;

				if(x < 0.0)
					x += 1.0f;
				if(y < 0.0)
					y += 1.0f;

				float4 col = tex2D (_MainTex, float2(x, y)).rgba;
				return col * _Color;
            }
            ENDCG
        }
	} 
	Fallback "Diffuse"
}  