Shader "Pattern" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_TexScale ("Texture Scale", Float) = 1.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
			float3 worldPos;
		};
		sampler2D _MainTex;
		float _TexScale;
		void surf ( Input IN , inout SurfaceOutput o) {
			float scale = 1.0f / _TexScale;
			float x = fmod(IN.worldPos.x * scale, 1.0f);
			float y = fmod(IN.worldPos.y * scale, 1.0f);
			if(x < 0.0)
				x += 1.0f;
			if(y < 0.0)
				y += 1.0f;
			o.Albedo = tex2D (_MainTex, float2(x, y)).rgb;
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
		}
		ENDCG
	} 
	Fallback "Diffuse"
}  