Shader "Custom/OutlineWithBaseTexture"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // 기본 텍스처
        _OutLineColor ("Outline Color", Color) = (0,0,0,1) // 테두리 색상
        _OutLineWidth ("Outline Width", Range(0.001, 0.05)) = 0.01 // 테두리 두께
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // 아웃라인 패스
        Pass
        {
            Cull Front // 앞면을 제거해 오브젝트의 외곽선만 표시
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha // 투명도를 고려한 블렌딩

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _OutLineWidth;
            fixed4 _OutLineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                // 법선 방향으로 오브젝트를 확장하여 테두리 생성
                v.vertex.xyz += v.normal * _OutLineWidth;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutLineColor; // 테두리 색상 적용
            }
            ENDCG
        }

        // 기본 텍스처 패스
        Pass
        {
            Cull Back // 뒷면을 그리기 위해 Back Cull
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha // 투명도를 고려한 블렌딩

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // UV 좌표
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv); // 텍스처 샘플링
                return col; // 원본 색상 반환
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
