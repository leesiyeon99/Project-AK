
// 테두리를 가질 프리팹의 매터리얼은 원본 매터리얼 사본을 만들어서 셰이더 변경 Custom -> OutLine 설정
// (그냥 적용하면 같은 매터리얼을 쓰는 다른 오브젝트도 전부 변경되버림 사본으로 따로 적용해야함)
// 테두리를 가질 프리팹들은 만들어진 Outline 사본 매터리얼 사용


Shader "Custom/OutLine"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // 알베도 텍스쳐의 기본값 흰색
        _OutLineColor("OutLine Color", Color) = (0,0,0,0) // 테두리의 색상 조절
        _OutLineWidth("OutLine Width", Range(0.001, 0.05)) = 0.01 // 테두리의 두께 조절
        _BlinkSpeed("Blink Speed", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"} // 셰이더가 가려치면 투명하게  // 
        LOD 200 // 셰이더의 복잡도 LOD 지정 ( 200 중간 수준 )

        // 테두리 패스
        cull front // 앞면을 제거해 오브젝트의 외곽선만 표시
        zwrite off // 깊이 버퍼를 쓰지 않아 다른 오브젝트와 겹치면 맨 위에 그려지게함

        CGPROGRAM
        #pragma surface surf NoLight vertex:vert noshadow noambient // 테두리는 조명, 그림자 받지 않음

        float4 _OutLineColor; // 테두리 색상
        float _OutLineWidth; // 테두리 두께
        float _BlinkSpeed; // 깜빡일 속도

        void vert(inout appdata_full v) {
            float time = _Time.y;
            // 0에서 _OutLineWidth까지 시간에따라 sin값으로 두께를 변화시킴
            float width = lerp(0.0, _OutLineWidth, abs(sin(time * _BlinkSpeed))); // 0에서 _OutLineWidth까지 두께 변화
            v.vertex.xyz += v.normal.xyz * width; // 법선 방향으로 테두리 두께만큼 이동시켜 테두리 표시
        }

        struct Input
        {
            float4 color; 
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            
        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(_OutLineColor.rgb, 1); // 테두리 색상 반환
        }
        ENDCG

        // 메인 패스
        cull back // 테두리가 아닌 바깥쪽 렌더링을 위해 뒷면 제거
        zwrite on // 이쪽은 깊이 버퍼를 써 내부가 올바르게 보이게
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0 // 

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
    FallBack "Diffuse" // 셰이더가 지원되지 않으면 기본 Diffuse 셰이더로 사용
}