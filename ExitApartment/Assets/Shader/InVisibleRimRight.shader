Shader "Custom/InVisibleRimRight"
{
 Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)     // �˺��� ����
        _RimColor ("Rim Color", Color) = (1,1,1,1)   // ���� ����Ʈ ����
        _RimPower ("Rim Power", Range(0.1, 10.0)) = 3.0 // ���� ����Ʈ ����
        _MainTex ("Albedo (RGB)", 2D) = "white" {}   // �˺��� �ؽ�ó
        _Transparency ("Transparency", Range(0, 1)) = 1.0 // ����
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
            float3 viewDir;  // �� ����
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo ���� �� �ؽ�ó
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // ���� ����
            c.a *= _Transparency;

            o.Albedo = c.rgb;
            o.Alpha = c.a;

            // Rim Lighting ���
            float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            float rimFactor = pow(rim, _RimPower);

            // Emission�� Rim Light ���� ����
            o.Emission = _RimColor.rgb * rimFactor;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
