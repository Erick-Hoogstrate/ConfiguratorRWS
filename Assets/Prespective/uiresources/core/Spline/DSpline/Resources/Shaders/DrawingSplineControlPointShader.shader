Shader "Custom/DrawingSplineControlPointShader"
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
        struct PointData
        {
            float3 Position;
            float4 Colour;
        };

        StructuredBuffer<PointData> controlpoints;
        float size;
        matrix localToGlobal;


        float3 MultiplyPoint3x4(float3 _point)
        {
            return float3(
                localToGlobal._m00 * _point.x + localToGlobal._m01 * _point.y + localToGlobal._m02 * _point.z + localToGlobal._m03,
                localToGlobal._m10 * _point.x + localToGlobal._m11 * _point.y + localToGlobal._m12 * _point.z + localToGlobal._m13,
                localToGlobal._m20 * _point.x + localToGlobal._m21 * _point.y + localToGlobal._m22 * _point.z + localToGlobal._m23);
        }
#endif

        void ConfigureProcedural()
        {
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
            float3 position = MultiplyPoint3x4(controlpoints[unity_InstanceID].Position);
            unity_ObjectToWorld = 0.0;

            unity_ObjectToWorld._m03 = position.x;
            unity_ObjectToWorld._m13 = position.y;
            unity_ObjectToWorld._m23 = position.z;
            unity_ObjectToWorld._m33 = 1.0;

            unity_ObjectToWorld._m00 = size;
            unity_ObjectToWorld._m11 = size;
            unity_ObjectToWorld._m22 = size;
#endif
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
            float4 col = controlpoints[unity_InstanceID].Colour;
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
