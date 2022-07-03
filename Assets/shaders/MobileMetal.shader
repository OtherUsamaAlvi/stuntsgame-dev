Shader "Mobile/MobileMetal"
{
    Properties{
      _MainTex("Texture", 2D) = "white" {}
      _Color("Main Color", Color) = (1, 1, 1, 1)
       _BumpMap("Normalmap", 2D) = "bump" {}
      _Cube("Cube env tex", CUBE) = "black" {}
      _MixPower("Mix Power", Range(1, 0.01)) = 0.5  
    }
        SubShader{
          Tags { "RenderType" = "Opaque" }
          CGPROGRAM

          sampler2D _MainTex;
          sampler2D _BumpMap;
          fixed4 _Color;
          samplerCUBE _Cube;
          fixed _MixPower;

          #pragma surface surf Lambert
          struct Input {
             float2 uv_MainTex;
             float3 worldRefl;
             float2 uv_BumpMap;
             float3 viewDir;
             
          };


          void surf(Input IN, inout SurfaceOutput o) {
              half4  mytex = tex2D(_MainTex, IN.uv_MainTex) ;
              o.Albedo = mytex.rgb * _Color.rgb;
              o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
             
              o.Emission = texCUBE(_Cube, IN.viewDir).rgb;
              o.Alpha = 0.2 - saturate(dot (normalize(IN.viewDir),o.Normal));
          }
        ENDCG
    }
    FallBack "Mobile/Diffuse"
}
