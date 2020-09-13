Shader "Custom/Beom_half_lambert"
{
    Properties
    {
        [Header(Base)]
        [Space(10)]

        _Color ("기본 색상", Color) = (1,1,1,1)

        _EmissionInt ("색상 절대채도 조정", Range(0,1)) = 0.5
        
        _MainTex ("색상 텍스처 (RGB)", 2D) = "white" {}
        _BumpMap ("Normal map", 2D) = "bump" {}

        [Header(Specular)]
        [Space(10)]
        _Specpow ("스펙큘러 범위 조정", Range(1,200)) = 20 
        _SpecCol ("스펙큘러 칼라 조정", Color) = (1,1,1,1)
        _SpecInt ("스펙큘러 강도 조정", Range(0,1.5)) = 0.3


        [Space(10)]
        [Header(Rim Light)]
        [Space(10)]
        _Rimpow  ("외곽 라이트 범위 조정", Range(1,5)) = 1
        _RimInt  ("외곽 라이트 강도 조정", Range(0,3)) = 1
        _RimCol  ("외각 라이트 색상 조정", Color) = (1,1,1,1)
     
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf CustomBeom vertex:vert
        
        

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        samplerCUBE _CubeMap;

       
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            
            float3 worldRefl;
            INTERNAL_DATA

            float3 vnormal;
        };

        fixed4 _Color;
        fixed4 _Color2;

        fixed _Specpow;
        fixed _SpecInt;
        fixed4 _SpecCol;

        fixed _Rimpow;
        fixed _RimInt;
        fixed4 _RimCol;

        fixed _EmissionInt;
        
        void vert(inout appdata_full v, out Input o)
        {
            float3 normalize_vnormal = normalize(v.normal);//normalize(mul(unity_ObjectToWorld,normalize(v.normal)));

            UNITY_INITIALIZE_OUTPUT(Input, o);
            
            o.vnormal = normalize_vnormal;
        }
       
        void surf (Input IN, inout SurfaceOutput o)
        {
            float3 vertexnormal = IN.vnormal;
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            //o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            o.Emission = _Color * _EmissionInt;
            o.Alpha = c.a;
        }

        float4 LightingCustomBeom (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            lightDir = normalize(lightDir);
            viewDir = normalize(viewDir);

            //// 기본 디퓨즈 공식

            float4 ndotl = saturate(dot(lightDir, normalize(s.Normal))); // Lambert 공식
            //float4 halflam = saturate(ndotl* 0.5 + 0.5); // half Lambert 공식
            float3 DiffuseColor = (ndotl) * s.Albedo * atten * _LightColor0;

            //// 스펙큘러 블린 퐁 공식
            float3 H = normalize(lightDir + viewDir); // 벡터끼리 더하면 항상 정규화 시켜야함.
            float Spec = saturate(dot(H, normalize(s.Normal)));
            float Spec2 = saturate(pow(Spec, _Specpow));
            float3 Specfinal = ((Spec2 * _SpecCol.rgb)) * _SpecInt;

            //// 림 라이트 공식

            float3 Rim = 1 - saturate(dot(viewDir, normalize(s.Normal)));
            float3 Rimpow = saturate(pow (Rim, _Rimpow) * _RimInt) * _RimCol;


            float4 final; // 최종 결과
            final.rgb = DiffuseColor + Specfinal + Rimpow;
            
            final.a = s.Alpha;
            return  final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
