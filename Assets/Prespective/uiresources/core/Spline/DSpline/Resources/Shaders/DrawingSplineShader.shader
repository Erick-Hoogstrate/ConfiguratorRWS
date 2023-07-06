Shader "Custom/DrawingSplineShader"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        [HideInInspector] _QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }

	//Universal Render Pipeline shader
	SubShader
	{
		PackageRequirements
		{
			"com.unity.render-pipelines.core": "12.1"
			"com.unity.render-pipelines.universal" : "12.1"
		}
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType" = "Transparent"
			"UniversalMaterialType" = "Unlit"
			"Queue" = "Transparent"
			"ShaderGraphShader" = "true"
			"ShaderGraphTargetId" = "UniversalUnlitSubTarget"
		}
		Pass
		{
			Name "Universal Forward"
			Tags
			{
				// <None>
			}

			// Render State
			Cull Back
			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off

			// Debug
			// <None>

			// --------------------------------------------------
			// Pass

			HLSLPROGRAM

			// Pragmas
			#pragma instancing_options procedural:ConfigureProcedural
			#pragma instancing_options assumeuniformscaling procedural:ConfigureProcedural
			#pragma target 4.5
			#pragma exclude_renderers gles gles3 glcore
			#pragma multi_compile_instancing
			#pragma multi_compile_fog
			#pragma instancing_options renderinglayer
			#pragma multi_compile _ DOTS_INSTANCING_ON
			#pragma vertex vert
			#pragma fragment frag

			// Keywords
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma shader_feature _ _SAMPLE_GI
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile_fragment _ DEBUG_DISPLAY

			// Defines
			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define VARYINGS_NEED_POSITION_WS
			#define VARYINGS_NEED_NORMAL_WS
			#define VARYINGS_NEED_VIEWDIRECTION_WS
			#define FEATURES_GRAPH_VERTEX
			/* WARNING: $splice Could not find named fragment 'PassInstancing' */
			#define SHADERPASS SHADERPASS_UNLIT
			#define _FOG_FRAGMENT 1
			#define _SURFACE_TYPE_TRANSPARENT 1
			/* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */


			// custom interpolator pre-include
			/* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */

			// Includes
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"


			half _Glossiness;
			half _Metallic;


			UNITY_INSTANCING_BUFFER_START(Props)

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
			struct Attributes
			{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					float4 tangentOS : TANGENT;
				#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
				#endif
			};
			struct Varyings
			{
					float4 positionCS : SV_POSITION;
					float3 positionWS;
					float3 normalWS;
					float3 viewDirectionWS;
				#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : CUSTOM_INSTANCE_ID;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
				#endif
			};
			struct SurfaceDescriptionInputs
			{
			};
			struct VertexDescriptionInputs
			{
					float3 ObjectSpaceNormal;
					float3 ObjectSpaceTangent;
					float3 ObjectSpacePosition;
			};
			struct SurfaceDescription
			{
				float3 BaseColor;
				float Alpha;
			};

			SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
			{
				#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
				float4 col = colour;
				#else
				float4 col = float4(1, 0, 0, 1);
				#endif

				SurfaceDescription surface = (SurfaceDescription)0;
				float4 _Property_67e06950b1f84e1d9d30edb240eee1d4_Out_0 = col;
				surface.BaseColor = (_Property_67e06950b1f84e1d9d30edb240eee1d4_Out_0.xyz);
				surface.Alpha = 1;
				return surface;
			}
			struct PackedVaryings
			{
					float4 positionCS : SV_POSITION;
					float3 interp0 : INTERP0;
					float3 interp1 : INTERP1;
					float3 interp2 : INTERP2;
				#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : CUSTOM_INSTANCE_ID;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
				#endif
			};

			PackedVaryings PackVaryings(Varyings input)
			{
				PackedVaryings output;
				ZERO_INITIALIZE(PackedVaryings, output);
				output.positionCS = input.positionCS;
				output.interp0.xyz = input.positionWS;
				output.interp1.xyz = input.normalWS;
				output.interp2.xyz = input.viewDirectionWS;
				#if UNITY_ANY_INSTANCING_ENABLED
				output.instanceID = input.instanceID;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				output.cullFace = input.cullFace;
				#endif
				return output;
			}

			Varyings UnpackVaryings(PackedVaryings input)
			{
				Varyings output;
				output.positionCS = input.positionCS;
				output.positionWS = input.interp0.xyz;
				output.normalWS = input.interp1.xyz;
				output.viewDirectionWS = input.interp2.xyz;
				#if UNITY_ANY_INSTANCING_ENABLED
				output.instanceID = input.instanceID;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				output.cullFace = input.cullFace;
				#endif
				return output;
			}


			CBUFFER_START(UnityPerMaterial)
			float4 _Color;
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
			float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
			int _ObjectId;
			int _PassValue;
			#endif

			struct VertexDescription
			{
				float3 Position;
				float3 Normal;
				float3 Tangent;
			};

			VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
			{
				VertexDescription description = (VertexDescription)0;
				description.Position = IN.ObjectSpacePosition;
				description.Normal = IN.ObjectSpaceNormal;
				description.Tangent = IN.ObjectSpaceTangent;
				return description;
			}

			#ifdef FEATURES_GRAPH_VERTEX
			Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
			{
				return output;
			}
			#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
			#endif


			#ifdef HAVE_VFX_MODIFICATION
			#define VFX_SRP_ATTRIBUTES Attributes
			#define VFX_SRP_VARYINGS Varyings
			#define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
			#endif
			VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
			{
				VertexDescriptionInputs output;
				ZERO_INITIALIZE(VertexDescriptionInputs, output);

				output.ObjectSpaceNormal = input.normalOS;
				output.ObjectSpaceTangent = input.tangentOS.xyz;
				output.ObjectSpacePosition = input.positionOS;

				return output;
			}
			SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
			{
				SurfaceDescriptionInputs output;
				ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

				#ifdef HAVE_VFX_MODIFICATION
				// FragInputs from VFX come from two places: Interpolator or CBuffer.
				/* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */

				#endif

				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign = IS_FRONT_VFACE(input.cullFace, true, false);
				#else
				#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
				#endif
				#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

				return output;
			}

			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"

			#ifdef HAVE_VFX_MODIFICATION
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
			#endif

			ENDHLSL
		}
	}

	// Default (Built-In Pipeline) Shader
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
