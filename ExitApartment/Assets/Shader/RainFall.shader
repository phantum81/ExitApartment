Shader "Custom/RainFall"
{
    
     Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _DistortionTex ("Distortion (RGB)", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _DistortionTex;
            float _DistortionStrength;

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert_img (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = pos;
                o.uv = uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 distortion = (tex2D(_DistortionTex, i.uv).rg * 2.0 - 1.0) * _DistortionStrength;
                float4 color = tex2D(_MainTex, i.uv + distortion);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}