Shader "Custom/DrawingSplineShader"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    { 
        Lighting Off
        AlphaTest Greater 0.5
        Tags { "RenderType" = "Opaque" "ForceNoShadowCasting" = "True" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard noshadow
        #pragma surface ConfigureSurface Standard noshadow
        #pragma instancing_options procedural:ConfigureProcedural
        #pragma instancing_options assumeuniformscaling procedural:ConfigureProcedural
        #pragma target 4.5
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
        struct LineData
        {
            float3 Position;
            float4 Rotation;
            float Length;
        };

        StructuredBuffer<LineData> lines;
        float radius;
        float4 colour;
#endif
        void ConfigureProcedural()
        {
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
            LineData data;
            float3 position = lines[unity_InstanceID].Position;
            unity_ObjectToWorld = 0.0;

            unity_ObjectToWorld._m03 = position.x;
            unity_ObjectToWorld._m13 = position.y;
            unity_ObjectToWorld._m23 = position.z;
            unity_ObjectToWorld._m33 = 1.0;

            float4 quaternion = lines[unity_InstanceID].Rotation;
            float x = quaternion.x * 2.0;
            float y = quaternion.y * 2.0;
            float z = quaternion.z * 2.0;
            float xx = quaternion.x * x;
            float yy = quaternion.y * y;
            float zz = quaternion.z * z;
            float xy = quaternion.x * y;
            float xz = quaternion.x * z;
            float yz = quaternion.y * z;
            float wx = quaternion.w * x;
            float wy = quaternion.w * y;
            float wz = quaternion.w * z;

            float length = lines[unity_InstanceID].Length;

            unity_ObjectToWorld._m00 = (1.0 - (yy + zz)) * radius;
            unity_ObjectToWorld._m10 = (xy + wz) * radius;
            unity_ObjectToWorld._m20 = (xz - wy) * radius;

            unity_ObjectToWorld._m01 = (xy - wz) * radius;
            unity_ObjectToWorld._m11 = (1.0 - (xx + zz)) * radius;
            unity_ObjectToWorld._m21 = (yz + wx) * radius;

            unity_ObjectToWorld._m02 = (xz + wy) * length;
            unity_ObjectToWorld._m12 = (yz - wx) * length;
            unity_ObjectToWorld._m22 = (1.0 - (xx + yy)) * length;
#endif
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
            float4 col = colour;
#else
            float4 col = float4(1, 0, 0, 1);
#endif
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * col;
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
