//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Internal-UIRDefault" {
Properties {
_MainTex ("Atlas", 2D) = "white" { }
_FontTex ("Font", 2D) = "black" { }
_CustomTex ("Custom", 2D) = "black" { }
_Color ("Tint", Color) = (1,1,1,1)
_StencilCompFront ("__scf", Float) = 3
_StencilPassFront ("__spf", Float) = 0
_StencilZFailFront ("__szf", Float) = 1
_StencilFailFront ("__sff", Float) = 0
_StencilCompBack ("__scb", Float) = 8
_StencilPassBack ("__spb", Float) = 0
_StencilZFailBack ("__szb", Float) = 2
_StencilFailBack ("__sfb", Float) = 0
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" "UIE_ComputeIsAvailable" = "1" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" "UIE_ComputeIsAvailable" = "1" }
  ZTest GEqual
  ZWrite Off
  Cull Off
  GpuProgramID 13759
Program "vp" {
SubProgram "gles3 hw_tier00 " {
"#ifdef VERTEX
#version 310 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
 struct _TransformsBuffer_type {
	uint[16] value;
};

layout(std430, binding = 0) readonly buffer _TransformsBuffer {
	_TransformsBuffer_type _TransformsBuffer_buf[];
};
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
layout(location = 0) out mediump vec4 vs_COLOR0;
layout(location = 1) out highp vec4 vs_TEXCOORD0;
layout(location = 2) flat out mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec2 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
ivec4 u_xlati2;
vec4 u_xlat3;
vec2 u_xlat4;
float u_xlat5;
vec2 u_xlat8;
vec2 u_xlat9;
void main()
{
    u_xlatu0.xy = uvec2(in_TEXCOORD1.xy);
    u_xlat1 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 3]));
    u_xlat8.x = dot(u_xlat1.xy, in_TEXCOORD0.xy);
    u_xlat9.x = dot(u_xlat1, in_POSITION0);
    u_xlat2 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 3]));
    u_xlat8.y = dot(u_xlat2.xy, in_TEXCOORD0.xy);
    u_xlat9.y = dot(u_xlat2, in_POSITION0);
    u_xlat1.xy = u_xlat8.xy * _1PixelClipInvView.zw;
    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
    u_xlat1.x = sqrt(u_xlat1.x);
    u_xlat5 = max(u_xlat1.x, 1.0);
    u_xlat1.x = u_xlat1.x + 9.99999997e-07;
    u_xlat5 = roundEven(u_xlat5);
    u_xlat1.x = u_xlat5 / u_xlat1.x;
    u_xlat1.x = u_xlat1.x + -1.0;
    u_xlat8.xy = u_xlat8.xy * u_xlat1.xx;
    u_xlati2 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat8.xy = uintBitsToFloat(floatBitsToUint(u_xlat8.xy) & uvec2(u_xlati2.xx));
    u_xlat8.xy = u_xlat8.xy + u_xlat9.xy;
    u_xlat1 = u_xlat8.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat8.xxxx + u_xlat1;
    vs_TEXCOORD0.zw = u_xlat8.xy;
    u_xlat3 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 3]));
    u_xlat0 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 3]));
    vs_TEXCOORD2 = u_xlat0;
    u_xlat0.x = dot(u_xlat3, in_POSITION0);
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
    u_xlat1.xy = u_xlat0.xy + vec2(1.0, 1.0);
    u_xlat1.xy = u_xlat1.xy / _1PixelClipInvView.xy;
    u_xlat1.xy = u_xlat1.xy + vec2(0.50999999, 0.50999999);
    u_xlat1.xy = trunc(u_xlat1.xy);
    u_xlat1.xy = u_xlat1.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati2.w != 0) ? u_xlat1.xy : u_xlat0.xy;
    gl_Position.zw = u_xlat0.zw;
    u_xlat0 = in_COLOR0 * _Color;
    vs_COLOR0 = u_xlat0;
    u_xlatb0 = u_xlati2.y==0;
    u_xlati0 = u_xlatb0 ? u_xlati2.z : int(0);
    u_xlat1.xyz = uintBitsToFloat(uvec3(u_xlati2.wzy) & uvec3(1065353216u, 1065353216u, 1065353216u));
    u_xlat4.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat2.xy = u_xlat4.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat2.xy : u_xlat4.xy;
    u_xlat0.x = u_xlat1.y + u_xlat1.x;
    u_xlat0.x = u_xlat1.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat1.w = (-u_xlat0.x) + 1.0;
    vs_TEXCOORD1 = u_xlat1;
    return;
}

#endif
#ifdef FRAGMENT
#version 310 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
layout(location = 0) in mediump vec4 vs_COLOR0;
layout(location = 1) in highp vec4 vs_TEXCOORD0;
layout(location = 2) flat in mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
    u_xlatb0.x = u_xlat0.x<0.0;
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
"#ifdef VERTEX
#version 310 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
 struct _TransformsBuffer_type {
	uint[16] value;
};

layout(std430, binding = 0) readonly buffer _TransformsBuffer {
	_TransformsBuffer_type _TransformsBuffer_buf[];
};
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
layout(location = 0) out mediump vec4 vs_COLOR0;
layout(location = 1) out highp vec4 vs_TEXCOORD0;
layout(location = 2) flat out mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec2 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
ivec4 u_xlati2;
vec4 u_xlat3;
vec2 u_xlat4;
float u_xlat5;
vec2 u_xlat8;
vec2 u_xlat9;
void main()
{
    u_xlatu0.xy = uvec2(in_TEXCOORD1.xy);
    u_xlat1 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 3]));
    u_xlat8.x = dot(u_xlat1.xy, in_TEXCOORD0.xy);
    u_xlat9.x = dot(u_xlat1, in_POSITION0);
    u_xlat2 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 3]));
    u_xlat8.y = dot(u_xlat2.xy, in_TEXCOORD0.xy);
    u_xlat9.y = dot(u_xlat2, in_POSITION0);
    u_xlat1.xy = u_xlat8.xy * _1PixelClipInvView.zw;
    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
    u_xlat1.x = sqrt(u_xlat1.x);
    u_xlat5 = max(u_xlat1.x, 1.0);
    u_xlat1.x = u_xlat1.x + 9.99999997e-07;
    u_xlat5 = roundEven(u_xlat5);
    u_xlat1.x = u_xlat5 / u_xlat1.x;
    u_xlat1.x = u_xlat1.x + -1.0;
    u_xlat8.xy = u_xlat8.xy * u_xlat1.xx;
    u_xlati2 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat8.xy = uintBitsToFloat(floatBitsToUint(u_xlat8.xy) & uvec2(u_xlati2.xx));
    u_xlat8.xy = u_xlat8.xy + u_xlat9.xy;
    u_xlat1 = u_xlat8.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat8.xxxx + u_xlat1;
    vs_TEXCOORD0.zw = u_xlat8.xy;
    u_xlat3 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 3]));
    u_xlat0 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 3]));
    vs_TEXCOORD2 = u_xlat0;
    u_xlat0.x = dot(u_xlat3, in_POSITION0);
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
    u_xlat1.xy = u_xlat0.xy + vec2(1.0, 1.0);
    u_xlat1.xy = u_xlat1.xy / _1PixelClipInvView.xy;
    u_xlat1.xy = u_xlat1.xy + vec2(0.50999999, 0.50999999);
    u_xlat1.xy = trunc(u_xlat1.xy);
    u_xlat1.xy = u_xlat1.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati2.w != 0) ? u_xlat1.xy : u_xlat0.xy;
    gl_Position.zw = u_xlat0.zw;
    u_xlat0 = in_COLOR0 * _Color;
    vs_COLOR0 = u_xlat0;
    u_xlatb0 = u_xlati2.y==0;
    u_xlati0 = u_xlatb0 ? u_xlati2.z : int(0);
    u_xlat1.xyz = uintBitsToFloat(uvec3(u_xlati2.wzy) & uvec3(1065353216u, 1065353216u, 1065353216u));
    u_xlat4.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat2.xy = u_xlat4.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat2.xy : u_xlat4.xy;
    u_xlat0.x = u_xlat1.y + u_xlat1.x;
    u_xlat0.x = u_xlat1.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat1.w = (-u_xlat0.x) + 1.0;
    vs_TEXCOORD1 = u_xlat1;
    return;
}

#endif
#ifdef FRAGMENT
#version 310 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
layout(location = 0) in mediump vec4 vs_COLOR0;
layout(location = 1) in highp vec4 vs_TEXCOORD0;
layout(location = 2) flat in mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
    u_xlatb0.x = u_xlat0.x<0.0;
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
"#ifdef VERTEX
#version 310 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
 struct _TransformsBuffer_type {
	uint[16] value;
};

layout(std430, binding = 0) readonly buffer _TransformsBuffer {
	_TransformsBuffer_type _TransformsBuffer_buf[];
};
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
layout(location = 0) out mediump vec4 vs_COLOR0;
layout(location = 1) out highp vec4 vs_TEXCOORD0;
layout(location = 2) flat out mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec2 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
ivec4 u_xlati2;
vec4 u_xlat3;
vec2 u_xlat4;
float u_xlat5;
vec2 u_xlat8;
vec2 u_xlat9;
void main()
{
    u_xlatu0.xy = uvec2(in_TEXCOORD1.xy);
    u_xlat1 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(0 >> 2) + 3]));
    u_xlat8.x = dot(u_xlat1.xy, in_TEXCOORD0.xy);
    u_xlat9.x = dot(u_xlat1, in_POSITION0);
    u_xlat2 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(16 >> 2) + 3]));
    u_xlat8.y = dot(u_xlat2.xy, in_TEXCOORD0.xy);
    u_xlat9.y = dot(u_xlat2, in_POSITION0);
    u_xlat1.xy = u_xlat8.xy * _1PixelClipInvView.zw;
    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
    u_xlat1.x = sqrt(u_xlat1.x);
    u_xlat5 = max(u_xlat1.x, 1.0);
    u_xlat1.x = u_xlat1.x + 9.99999997e-07;
    u_xlat5 = roundEven(u_xlat5);
    u_xlat1.x = u_xlat5 / u_xlat1.x;
    u_xlat1.x = u_xlat1.x + -1.0;
    u_xlat8.xy = u_xlat8.xy * u_xlat1.xx;
    u_xlati2 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat8.xy = uintBitsToFloat(floatBitsToUint(u_xlat8.xy) & uvec2(u_xlati2.xx));
    u_xlat8.xy = u_xlat8.xy + u_xlat9.xy;
    u_xlat1 = u_xlat8.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat8.xxxx + u_xlat1;
    vs_TEXCOORD0.zw = u_xlat8.xy;
    u_xlat3 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.x].value[(32 >> 2) + 3]));
    u_xlat0 = vec4(uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 0]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 1]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 2]), uintBitsToFloat(_TransformsBuffer_buf[u_xlatu0.y].value[(48 >> 2) + 3]));
    vs_TEXCOORD2 = u_xlat0;
    u_xlat0.x = dot(u_xlat3, in_POSITION0);
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
    u_xlat1.xy = u_xlat0.xy + vec2(1.0, 1.0);
    u_xlat1.xy = u_xlat1.xy / _1PixelClipInvView.xy;
    u_xlat1.xy = u_xlat1.xy + vec2(0.50999999, 0.50999999);
    u_xlat1.xy = trunc(u_xlat1.xy);
    u_xlat1.xy = u_xlat1.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati2.w != 0) ? u_xlat1.xy : u_xlat0.xy;
    gl_Position.zw = u_xlat0.zw;
    u_xlat0 = in_COLOR0 * _Color;
    vs_COLOR0 = u_xlat0;
    u_xlatb0 = u_xlati2.y==0;
    u_xlati0 = u_xlatb0 ? u_xlati2.z : int(0);
    u_xlat1.xyz = uintBitsToFloat(uvec3(u_xlati2.wzy) & uvec3(1065353216u, 1065353216u, 1065353216u));
    u_xlat4.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat2.xy = u_xlat4.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat2.xy : u_xlat4.xy;
    u_xlat0.x = u_xlat1.y + u_xlat1.x;
    u_xlat0.x = u_xlat1.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat1.w = (-u_xlat0.x) + 1.0;
    vs_TEXCOORD1 = u_xlat1;
    return;
}

#endif
#ifdef FRAGMENT
#version 310 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
layout(location = 0) in mediump vec4 vs_COLOR0;
layout(location = 1) in highp vec4 vs_TEXCOORD0;
layout(location = 2) flat in mediump vec4 vs_TEXCOORD1;
layout(location = 3) flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
    u_xlatb0.x = u_xlat0.x<0.0;
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "gles3 hw_tier00 " {
""
}
SubProgram "gles3 hw_tier01 " {
""
}
SubProgram "gles3 hw_tier02 " {
""
}
}
}
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
  ZTest GEqual
  ZWrite Off
  Cull Off
  GpuProgramID 70470
Program "vp" {
SubProgram "gles3 hw_tier00 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "gles3 hw_tier00 " {
""
}
SubProgram "gles3 hw_tier01 " {
""
}
SubProgram "gles3 hw_tier02 " {
""
}
}
}
}
SubShader {
 Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
 Pass {
  Tags { "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
  ZTest GEqual
  ZWrite Off
  Cull Off
  GpuProgramID 195550
Program "vp" {
SubProgram "gles3 hw_tier00 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
in highp vec4 in_POSITION0;
in highp vec4 in_COLOR0;
in highp vec2 in_TEXCOORD0;
in highp vec3 in_TEXCOORD1;
out mediump vec4 vs_COLOR0;
out highp vec4 vs_TEXCOORD0;
flat out mediump vec4 vs_TEXCOORD1;
flat out mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
int u_xlati0;
uvec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
ivec4 u_xlati1;
vec4 u_xlat2;
vec4 u_xlat3;
vec4 u_xlat4;
vec2 u_xlat5;
vec2 u_xlat6;
vec2 u_xlat13;
void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = uvec4(u_xlat0);
    u_xlati1 = ivec4(uvec4(equal(in_TEXCOORD1.zzzz, vec4(4.0, 3.0, 2.0, 1.0))) * 0xFFFFFFFFu);
    u_xlat2.xyz = uintBitsToFloat(uvec3(uint(u_xlati1.w) & uint(1065353216u), uint(u_xlati1.z) & uint(1065353216u), uint(u_xlati1.y) & uint(1065353216u)));
    if(u_xlati1.x != 0) {
        u_xlat3.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat3.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat13.xy = u_xlat3.xy * _1PixelClipInvView.zw;
        u_xlat1.x = dot(u_xlat13.xy, u_xlat13.xy);
        u_xlat1.x = sqrt(u_xlat1.x);
        u_xlat13.x = max(u_xlat1.x, 1.0);
        u_xlat13.x = roundEven(u_xlat13.x);
        u_xlat1.x = u_xlat1.x + 9.99999997e-07;
        u_xlat1.x = u_xlat13.x / u_xlat1.x;
        u_xlat1.x = u_xlat1.x + -1.0;
        u_xlat3.xy = u_xlat3.xy * u_xlat1.xx;
    } else {
        u_xlat3.x = float(0.0);
        u_xlat3.y = float(0.0);
    }
    u_xlat13.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat13.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat3.xy + u_xlat13.xy;
    u_xlat3 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat3;
    u_xlat3 = u_xlat3 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat4 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat4;
    u_xlat4 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat4;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat3.wwww + u_xlat4;
    u_xlat4.xy = u_xlat3.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlati1.w != 0) ? u_xlat4.xy : u_xlat3.xy;
    u_xlat1.xw = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlati1.y==0);
#else
    u_xlatb0 = u_xlati1.y==0;
#endif
    u_xlati0 = u_xlatb0 ? u_xlati1.z : int(0);
    u_xlat6.xy = u_xlat1.xw * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (int(u_xlati0) != 0) ? u_xlat6.xy : u_xlat1.xw;
    u_xlat1 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat3.zw;
    vs_COLOR0 = u_xlat1;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _PixelClipRect;
UNITY_LOCATION(0) uniform mediump sampler2D _MainTex;
UNITY_LOCATION(1) uniform mediump sampler2D _FontTex;
UNITY_LOCATION(2) uniform mediump sampler2D _CustomTex;
in mediump vec4 vs_COLOR0;
in highp vec4 vs_TEXCOORD0;
flat in mediump vec4 vs_TEXCOORD1;
flat in mediump vec4 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
vec2 u_xlat4;
bvec2 u_xlatb4;
void main()
{
vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
    u_xlatb0.xy = greaterThanEqual(vs_TEXCOORD0.zwzz, vs_TEXCOORD2.xyxx).xy;
    u_xlatb0.zw = greaterThanEqual(vs_TEXCOORD2.zzzw, vs_TEXCOORD0.zzzw).zw;
    u_xlat0 = mix(vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0), vec4(u_xlatb0));
    u_xlat0.xy = u_xlat0.zw + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(hlslcc_FragCoord.xyxy, _PixelClipRect.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlatb4.xy = greaterThanEqual(_PixelClipRect.zwzw, hlslcc_FragCoord.xyxy).xy;
    u_xlat4.xy = mix(vec2(0.0, 0.0), vec2(1.0, 1.0), vec2(u_xlatb4.xy));
    u_xlat0.xy = u_xlat4.xy + u_xlat0.xy;
    u_xlat0.z = 1.0;
    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, -7.94999981));
#ifdef UNITY_ADRENO_ES3
    u_xlatb0.x = !!(u_xlat0.x<0.0);
#else
    u_xlatb0.x = u_xlat0.x<0.0;
#endif
    if(((int(u_xlatb0.x) * int(0xffffffffu)))!=0){discard;}
    u_xlat16_0.x = texture(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat16_0.x * vs_TEXCOORD1.x;
    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat16_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat16_1 = texture(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat16_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "gles3 hw_tier00 " {
""
}
SubProgram "gles3 hw_tier01 " {
""
}
SubProgram "gles3 hw_tier02 " {
""
}
}
}
}
}