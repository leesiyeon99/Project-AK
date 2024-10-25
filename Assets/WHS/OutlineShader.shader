Shader "Custom/OutLine"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // 알베도 텍스쳐의 기본값 흰색
        _OutLineColor("OutLine Color", Color) = (0,0,0,0) // 테두리의 색상
        _OutLineWidth("OutLine Width", Range(0.001, 0.05)) = 0.01 // 테두리의 두께
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        cull front // 앞면을 제거해 오브젝트의 외곽선만 표시
        zwrite off // 다른 오브젝트와 겹치면 맨 위에 그려지게함

        CGPROGRAM
        #pragma surface surf NoLight vertex:vert noshadow noambient // 테두리는 조명, 그림자 받지 않음

        float4 _OutLineColor;
        float _OutLineWidth;

        void vert(inout appdata_full v) {
            v.vertex.xyz += v.normal.xyz * _OutLineWidth;
        }

        struct Input
        {
            float4 color;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {

        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(_OutLineColor.rgb, 1);
        }
        ENDCG

        cull back
        zwrite on
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;
        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}