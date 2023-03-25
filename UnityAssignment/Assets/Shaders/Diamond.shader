Shader "Custom/Diamond" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _DiamondColor("Diamond Color", Color) = (1,1,1,1)
        _NoiseScale("Noise Scale", Range(0,10)) = 1
        _NoiseIntensity("Noise Intensity", Range(0,1)) = 0.1
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
            LOD 100

            CGPROGRAM
            #pragma surface surf Standard
            struct Input {
                float2 uv_MainTex;
            };

            float4 _Color;
            float _Glossiness;
            float _Metallic;
            float4 _DiamondColor;
            float _NoiseScale;
            float _NoiseIntensity;

            float random(float2 st) {
                return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            float smoothNoise(float2 st) {
                float corners = (random(st + float2(-1,-1)) + random(st + float2(1,-1)) + random(st + float2(-1,1)) + random(st + float2(1,1))) / 16.0;
                float sides = (random(st + float2(-1,0)) + random(st + float2(1,0)) + random(st + float2(0,-1)) + random(st + float2(0,1))) / 8.0;
                float center = random(st) / 4.0;
                return corners + sides + center;
            }

            float perlinNoise(float2 st) {
                float amplitude = 1.0;
                float frequency = 1.0;
                float noise = 0.0;
                for (int i = 0; i < 4; i++) {
                    noise += smoothNoise(st * frequency) * amplitude;
                    frequency *= 2.0;
                    amplitude *= 0.5;
                }
                return noise;
            }

            void surf(Input IN, inout SurfaceOutputStandard o) {
                float2 noiseUV = IN.uv_MainTex * _NoiseScale;
                float noise = (perlinNoise(noiseUV) - 0.5) * _NoiseIntensity;
                float4 color = _DiamondColor + float4(noise, noise, noise, 0);
                o.Albedo = color.rgb * _Color.rgb;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = _Color.a;
                o.Normal = float3(0,0,1);
            }
            ENDCG
    }

        FallBack "Diffuse"
}