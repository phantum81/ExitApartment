Shader "Custom/InVisibleRimRight"
{
 Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)     // 알베도 색상
        _RimColor ("Rim Color", Color) = (1,1,1,1)   // 리임 라이트 색상
        _RimPower ("Rim Power", Range(0.1, 10.0)) = 3.0 // 리임 라이트 강도
        _MainTex ("Albedo (RGB)", 2D) = "white" {}   // 알베도 텍스처
        _Transparency ("Transparency", Range(0, 1)) = 1.0 // 투명도
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:blend

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _RimColor;
        half _RimPower;
        half _Transparency;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;  // 뷰 방향
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo 색상 및 텍스처
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // 투명도 적용
            c.a *= _Transparency;

            o.Albedo = c.rgb;
            o.Alpha = c.a;

            // Rim Lighting 계산
            float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            float rimFactor = pow(rim, _RimPower);

            // Emission에 Rim Light 색상 적용
            o.Emission = _RimColor.rgb * rimFactor;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
