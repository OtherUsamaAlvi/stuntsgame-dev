Shader "Mobile/Custom Bumped Diffuse" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Color("Main Color", Color) = (1, 1, 1, 1)
        [NoScaleOffset] _BumpMap("Normalmap", 2D) = "bump" {}
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 250

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed4 _Color;
        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
        }
        ENDCG
    }

        FallBack "Mobile/Diffuse"
}