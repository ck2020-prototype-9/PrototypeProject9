Shader "Custom/Team9_BaseShader"
{
    Properties
    {
        [Header(Color)]
        [Space(10)]

        _Color ("컬러1", Color) = (0.5,0.5,0.5,0.5)
        _ColorR ("컬러2", Color) = (1,1,1,1)
        _ColorG ("컬러3", Color) = (1,1,1,1)
        _ColorB ("컬러4", Color) = (1,1,1,1)

        
        [Header(Main Texture)]
        [Space(10)]

        _MainTex ("메인 텍스처", 2D) = "black" {}

        [Space(10)]
        [Space(10)]
        _EmissionInt ("색상 절대채도 조정", Range(0,1)) = 0.5
        
        _MaskTex ("색상 마스크용 텍스처", 2D) = "black" {}
        _MainLn ("색상 명도 조절", Range(0,2)) = 0.5


        [Header(Specular)]
        [Space(10)]
        _Specpow ("스펙큘러 범위 조정", Range(1,200)) = 20 
        _SpecCol ("스펙큘러 칼라 조정", Color) = (1,1,1,1)
        _SpecInt ("스펙큘러 강도 조정", Range(0,1.5)) = 0


        [Space(10)]
        [Header(Rim Light)]
        [Space(10)]

 
        _Rimpow  ("외각 라이트 범위 조정", Range(0,10)) = 0
        _RimInt  ("외각 라이트 강도 조정", Range(0,3)) = 0
        _RimCol  ("외각 라이트 색상 조정", Color) = (1,1,1,1)

       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf CustomBeom
        #pragma shader_feature_RIMONOFF_ON
        

        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MaskTex;
        samplerCUBE _CubeMap;
       
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_MaskTex;


            float3 worldRefl;
            INTERNAL_DATA

        };

        fixed _Rimonoff;

        fixed4 _Color;
        fixed4 _ColorR;
        fixed4 _ColorG;
        fixed4 _ColorB;

        fixed _Specpow;
        fixed _SpecInt;
        fixed4 _SpecCol;
 
        fixed _Rimpow;
        fixed _RimInt;
        fixed4 _RimCol;

        fixed _MainLn;
        fixed _EmissionInt;
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            
            fixed4 c = tex2D(_MaskTex,IN.uv_MaskTex);
            fixed4 d = tex2D(_MainTex, IN.uv_MainTex);

            o.Albedo = lerp(_Color, _ColorR, c.r);
            o.Albedo = lerp(o.Albedo, _ColorG, c.g);
            o.Albedo = lerp(o.Albedo , _ColorB, c.b);
            

            o.Emission = d.rgb + (o.Albedo * _EmissionInt);
            o.Alpha = c.a;
        }

        float4 LightingCustomBeom (SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            lightDir = normalize(lightDir);
            viewDir = normalize(viewDir);
            s.Normal = normalize(s.Normal);


            //// 기본 디퓨즈 공식

            float4 ndotl = saturate(dot(lightDir, s.Normal)); // Lambert 공식
            //float4 halflam = saturate(ndotl* 0.5 + 0.5); // half Lambert 공식
            float3 DiffuseColor = (ndotl) * s.Albedo * atten * _LightColor0;

            //// 스펙큘러 블린 퐁 공식
            float3 H = normalize(lightDir + viewDir); // 벡터끼리 더하면 항상 정규화 시켜야함.
            float Spec = saturate(dot(H, s.Normal));
            float Spec2 = saturate(pow(Spec, _Specpow));
            float3 Specfinal = ((Spec2 * _SpecCol.rgb)) * _SpecInt;
            
           


            //// 림 라이트 공식
            float3 Rim = 1 - saturate(dot(viewDir, s.Normal));
            float3 Rimpow = saturate( pow (Rim,(_Rimpow)) *  _RimInt) * _RimCol;
 

            float4 final; // 최종 결과
            final.rgb = DiffuseColor + Specfinal;
            final.a = s.Alpha;

            final.rgb = DiffuseColor + Specfinal + Rimpow;
            return  final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
