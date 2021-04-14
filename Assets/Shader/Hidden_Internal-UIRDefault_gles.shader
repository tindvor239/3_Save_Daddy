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
}
Program "fp" {
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
}
Program "fp" {
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
SubProgram "gles hw_tier00 " {
"#ifdef VERTEX
#version 100

uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
attribute highp vec4 in_POSITION0;
attribute highp vec4 in_COLOR0;
attribute highp vec2 in_TEXCOORD0;
attribute highp vec3 in_TEXCOORD1;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
ivec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
bool u_xlatb1;
vec4 u_xlat2;
vec4 u_xlat3;
bvec3 u_xlatb3;
vec2 u_xlat4;
vec2 u_xlat5;
float u_xlat6;
vec2 u_xlat11;
float u_xlat16;
float roundEven(float x) { float y = floor(x + 0.5); return (y - x == 0.5) ? floor(0.5*y) * 2.0 : y; }
vec2 roundEven(vec2 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); return a; }
vec3 roundEven(vec3 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); return a; }
vec4 roundEven(vec4 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); a.w = roundEven(a.w); return a; }

float trunc(float x) { return sign(x)*floor(abs(x)); }
vec2 trunc(vec2 x) { return sign(x)*floor(abs(x)); }
vec3 trunc(vec3 x) { return sign(x)*floor(abs(x)); }
vec4 trunc(vec4 x) { return sign(x)*floor(abs(x)); }

void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = ivec4(u_xlat0);
    u_xlat1.x = in_TEXCOORD1.z + -4.0;
    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
    u_xlat1.x = (-u_xlat1.x) * 5.0 + in_TEXCOORD1.z;
    u_xlat6 = u_xlat1.x + -3.0;
    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
    u_xlat1.x = (-u_xlat6) * 4.0 + u_xlat1.x;
    u_xlat2.z = u_xlat1.x + -2.0;
    u_xlat2.z = clamp(u_xlat2.z, 0.0, 1.0);
    u_xlat1.x = (-u_xlat2.z) * 3.0 + u_xlat1.x;
    u_xlat2.y = u_xlat1.x + -1.0;
    u_xlat2.y = clamp(u_xlat2.y, 0.0, 1.0);
    u_xlat2.x = (-u_xlat2.y) * 2.0 + u_xlat1.x;
    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
    u_xlatb1 = u_xlat6==1.0;
    if(u_xlatb1){
        u_xlat1.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat1.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat11.xy = u_xlat1.xy * _1PixelClipInvView.zw;
        u_xlat11.x = dot(u_xlat11.xy, u_xlat11.xy);
        u_xlat11.x = sqrt(u_xlat11.x);
        u_xlat16 = max(u_xlat11.x, 1.0);
        u_xlat16 = roundEven(u_xlat16);
        u_xlat11.x = u_xlat11.x + 9.99999997e-07;
        u_xlat11.x = u_xlat16 / u_xlat11.x;
        u_xlat11.x = u_xlat11.x + -1.0;
        u_xlat1.xy = u_xlat1.xy * u_xlat11.xx;
    } else {
        u_xlat1.x = float(0.0);
        u_xlat1.y = float(0.0);
    }
    u_xlat11.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat11.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat1.xy + u_xlat11.xy;
    u_xlat1 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = u_xlat1 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat3 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat3;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat3;
    u_xlatb3.xyz = equal(u_xlat2.xyzx, vec4(1.0, 1.0, 0.0, 0.0)).xyz;
    u_xlat4.xy = u_xlat1.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlatb3.x) ? u_xlat4.xy : u_xlat1.xy;
    u_xlat1.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlatb0 = u_xlatb3.z && u_xlatb3.y;
    u_xlat3.xy = u_xlat1.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (bool(u_xlatb0)) ? u_xlat3.xy : u_xlat1.xy;
    u_xlat3 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat1.zw;
    vs_COLOR0 = u_xlat3;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 100

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif
precision highp int;
uniform 	vec4 _PixelClipRect;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _FontTex;
uniform lowp sampler2D _CustomTex;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
#define SV_Target0 gl_FragData[0]
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
lowp float u_xlat10_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
lowp vec4 u_xlat10_1;
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
    if(((int(u_xlatb0.x) * -1))!=0){discard;}
    u_xlat10_0 = texture2D(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat10_0 * vs_TEXCOORD1.x;
    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat10_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat10_1 = texture2D(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat10_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles hw_tier01 " {
"#ifdef VERTEX
#version 100

uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
attribute highp vec4 in_POSITION0;
attribute highp vec4 in_COLOR0;
attribute highp vec2 in_TEXCOORD0;
attribute highp vec3 in_TEXCOORD1;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
ivec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
bool u_xlatb1;
vec4 u_xlat2;
vec4 u_xlat3;
bvec3 u_xlatb3;
vec2 u_xlat4;
vec2 u_xlat5;
float u_xlat6;
vec2 u_xlat11;
float u_xlat16;
float roundEven(float x) { float y = floor(x + 0.5); return (y - x == 0.5) ? floor(0.5*y) * 2.0 : y; }
vec2 roundEven(vec2 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); return a; }
vec3 roundEven(vec3 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); return a; }
vec4 roundEven(vec4 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); a.w = roundEven(a.w); return a; }

float trunc(float x) { return sign(x)*floor(abs(x)); }
vec2 trunc(vec2 x) { return sign(x)*floor(abs(x)); }
vec3 trunc(vec3 x) { return sign(x)*floor(abs(x)); }
vec4 trunc(vec4 x) { return sign(x)*floor(abs(x)); }

void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = ivec4(u_xlat0);
    u_xlat1.x = in_TEXCOORD1.z + -4.0;
    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
    u_xlat1.x = (-u_xlat1.x) * 5.0 + in_TEXCOORD1.z;
    u_xlat6 = u_xlat1.x + -3.0;
    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
    u_xlat1.x = (-u_xlat6) * 4.0 + u_xlat1.x;
    u_xlat2.z = u_xlat1.x + -2.0;
    u_xlat2.z = clamp(u_xlat2.z, 0.0, 1.0);
    u_xlat1.x = (-u_xlat2.z) * 3.0 + u_xlat1.x;
    u_xlat2.y = u_xlat1.x + -1.0;
    u_xlat2.y = clamp(u_xlat2.y, 0.0, 1.0);
    u_xlat2.x = (-u_xlat2.y) * 2.0 + u_xlat1.x;
    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
    u_xlatb1 = u_xlat6==1.0;
    if(u_xlatb1){
        u_xlat1.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat1.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat11.xy = u_xlat1.xy * _1PixelClipInvView.zw;
        u_xlat11.x = dot(u_xlat11.xy, u_xlat11.xy);
        u_xlat11.x = sqrt(u_xlat11.x);
        u_xlat16 = max(u_xlat11.x, 1.0);
        u_xlat16 = roundEven(u_xlat16);
        u_xlat11.x = u_xlat11.x + 9.99999997e-07;
        u_xlat11.x = u_xlat16 / u_xlat11.x;
        u_xlat11.x = u_xlat11.x + -1.0;
        u_xlat1.xy = u_xlat1.xy * u_xlat11.xx;
    } else {
        u_xlat1.x = float(0.0);
        u_xlat1.y = float(0.0);
    }
    u_xlat11.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat11.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat1.xy + u_xlat11.xy;
    u_xlat1 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = u_xlat1 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat3 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat3;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat3;
    u_xlatb3.xyz = equal(u_xlat2.xyzx, vec4(1.0, 1.0, 0.0, 0.0)).xyz;
    u_xlat4.xy = u_xlat1.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlatb3.x) ? u_xlat4.xy : u_xlat1.xy;
    u_xlat1.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlatb0 = u_xlatb3.z && u_xlatb3.y;
    u_xlat3.xy = u_xlat1.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (bool(u_xlatb0)) ? u_xlat3.xy : u_xlat1.xy;
    u_xlat3 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat1.zw;
    vs_COLOR0 = u_xlat3;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 100

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif
precision highp int;
uniform 	vec4 _PixelClipRect;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _FontTex;
uniform lowp sampler2D _CustomTex;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
#define SV_Target0 gl_FragData[0]
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
lowp float u_xlat10_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
lowp vec4 u_xlat10_1;
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
    if(((int(u_xlatb0.x) * -1))!=0){discard;}
    u_xlat10_0 = texture2D(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat10_0 * vs_TEXCOORD1.x;
    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat10_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat10_1 = texture2D(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat10_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
SubProgram "gles hw_tier02 " {
"#ifdef VERTEX
#version 100

uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	vec4 _MainTex_ST;
uniform 	vec4 _MainTex_TexelSize;
uniform 	mediump vec4 _Color;
uniform 	vec4 _1PixelClipInvView;
uniform 	vec4 _Transforms[80];
attribute highp vec4 in_POSITION0;
attribute highp vec4 in_COLOR0;
attribute highp vec2 in_TEXCOORD0;
attribute highp vec3 in_TEXCOORD1;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
vec4 u_xlat0;
ivec4 u_xlatu0;
bool u_xlatb0;
vec4 u_xlat1;
bool u_xlatb1;
vec4 u_xlat2;
vec4 u_xlat3;
bvec3 u_xlatb3;
vec2 u_xlat4;
vec2 u_xlat5;
float u_xlat6;
vec2 u_xlat11;
float u_xlat16;
float roundEven(float x) { float y = floor(x + 0.5); return (y - x == 0.5) ? floor(0.5*y) * 2.0 : y; }
vec2 roundEven(vec2 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); return a; }
vec3 roundEven(vec3 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); return a; }
vec4 roundEven(vec4 a) { a.x = roundEven(a.x); a.y = roundEven(a.y); a.z = roundEven(a.z); a.w = roundEven(a.w); return a; }

float trunc(float x) { return sign(x)*floor(abs(x)); }
vec2 trunc(vec2 x) { return sign(x)*floor(abs(x)); }
vec3 trunc(vec3 x) { return sign(x)*floor(abs(x)); }
vec4 trunc(vec4 x) { return sign(x)*floor(abs(x)); }

void main()
{
    u_xlat0.x = in_TEXCOORD1.x * 4.0;
    u_xlat0.yzw = in_TEXCOORD1.xxy * vec3(4.0, 4.0, 4.0) + vec3(1.0, 2.0, 3.0);
    u_xlatu0 = ivec4(u_xlat0);
    u_xlat1.x = in_TEXCOORD1.z + -4.0;
    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
    u_xlat1.x = (-u_xlat1.x) * 5.0 + in_TEXCOORD1.z;
    u_xlat6 = u_xlat1.x + -3.0;
    u_xlat6 = clamp(u_xlat6, 0.0, 1.0);
    u_xlat1.x = (-u_xlat6) * 4.0 + u_xlat1.x;
    u_xlat2.z = u_xlat1.x + -2.0;
    u_xlat2.z = clamp(u_xlat2.z, 0.0, 1.0);
    u_xlat1.x = (-u_xlat2.z) * 3.0 + u_xlat1.x;
    u_xlat2.y = u_xlat1.x + -1.0;
    u_xlat2.y = clamp(u_xlat2.y, 0.0, 1.0);
    u_xlat2.x = (-u_xlat2.y) * 2.0 + u_xlat1.x;
    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
    u_xlatb1 = u_xlat6==1.0;
    if(u_xlatb1){
        u_xlat1.x = dot(_Transforms[int(u_xlatu0.x)].xy, in_TEXCOORD0.xy);
        u_xlat1.y = dot(_Transforms[int(u_xlatu0.y)].xy, in_TEXCOORD0.xy);
        u_xlat11.xy = u_xlat1.xy * _1PixelClipInvView.zw;
        u_xlat11.x = dot(u_xlat11.xy, u_xlat11.xy);
        u_xlat11.x = sqrt(u_xlat11.x);
        u_xlat16 = max(u_xlat11.x, 1.0);
        u_xlat16 = roundEven(u_xlat16);
        u_xlat11.x = u_xlat11.x + 9.99999997e-07;
        u_xlat11.x = u_xlat16 / u_xlat11.x;
        u_xlat11.x = u_xlat11.x + -1.0;
        u_xlat1.xy = u_xlat1.xy * u_xlat11.xx;
    } else {
        u_xlat1.x = float(0.0);
        u_xlat1.y = float(0.0);
    }
    u_xlat11.x = dot(_Transforms[int(u_xlatu0.x)], in_POSITION0);
    u_xlat11.y = dot(_Transforms[int(u_xlatu0.y)], in_POSITION0);
    u_xlat0.x = dot(_Transforms[int(u_xlatu0.z)], in_POSITION0);
    u_xlat5.xy = u_xlat1.xy + u_xlat11.xy;
    u_xlat1 = u_xlat5.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[0] * u_xlat5.xxxx + u_xlat1;
    u_xlat1 = hlslcc_mtx4x4unity_ObjectToWorld[2] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = u_xlat1 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat3 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat3;
    u_xlat3 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat3;
    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat3;
    u_xlatb3.xyz = equal(u_xlat2.xyzx, vec4(1.0, 1.0, 0.0, 0.0)).xyz;
    u_xlat4.xy = u_xlat1.xy + vec2(1.0, 1.0);
    u_xlat4.xy = u_xlat4.xy / _1PixelClipInvView.xy;
    u_xlat4.xy = u_xlat4.xy + vec2(0.50999999, 0.50999999);
    u_xlat4.xy = trunc(u_xlat4.xy);
    u_xlat4.xy = u_xlat4.xy * _1PixelClipInvView.xy + vec2(-1.0, -1.0);
    gl_Position.xy = (u_xlatb3.x) ? u_xlat4.xy : u_xlat1.xy;
    u_xlat1.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlatb0 = u_xlatb3.z && u_xlatb3.y;
    u_xlat3.xy = u_xlat1.xy * _MainTex_TexelSize.xy;
    vs_TEXCOORD0.xy = (bool(u_xlatb0)) ? u_xlat3.xy : u_xlat1.xy;
    u_xlat3 = in_COLOR0 * _Color;
    u_xlat0.x = u_xlat2.y + u_xlat2.x;
    u_xlat0.x = u_xlat2.z + u_xlat0.x;
    u_xlat0.x = min(u_xlat0.x, 1.0);
    u_xlat2.w = (-u_xlat0.x) + 1.0;
    gl_Position.zw = u_xlat1.zw;
    vs_COLOR0 = u_xlat3;
    vs_TEXCOORD0.zw = u_xlat5.xy;
    vs_TEXCOORD1 = u_xlat2;
    vs_TEXCOORD2 = _Transforms[int(u_xlatu0.w)];
    return;
}

#endif
#ifdef FRAGMENT
#version 100

#ifdef GL_FRAGMENT_PRECISION_HIGH
    precision highp float;
#else
    precision mediump float;
#endif
precision highp int;
uniform 	vec4 _PixelClipRect;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _FontTex;
uniform lowp sampler2D _CustomTex;
varying mediump vec4 vs_COLOR0;
varying highp vec4 vs_TEXCOORD0;
flat varying mediump vec4 vs_TEXCOORD1;
flat varying mediump vec4 vs_TEXCOORD2;
#define SV_Target0 gl_FragData[0]
vec4 u_xlat0;
mediump vec4 u_xlat16_0;
lowp float u_xlat10_0;
bvec4 u_xlatb0;
mediump vec4 u_xlat16_1;
lowp vec4 u_xlat10_1;
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
    if(((int(u_xlatb0.x) * -1))!=0){discard;}
    u_xlat10_0 = texture2D(_FontTex, vs_TEXCOORD0.xy).w;
    u_xlat16_0.w = u_xlat10_0 * vs_TEXCOORD1.x;
    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
    u_xlat16_1 = u_xlat10_1 * vs_TEXCOORD1.yyyy + vs_TEXCOORD1.wwww;
    u_xlat16_0.xyz = vs_TEXCOORD1.xxx;
    u_xlat16_0 = u_xlat16_0 + u_xlat16_1;
    u_xlat10_1 = texture2D(_CustomTex, vs_TEXCOORD0.xy);
    u_xlat16_0 = u_xlat10_1 * vs_TEXCOORD1.zzzz + u_xlat16_0;
    SV_Target0 = u_xlat16_0 * vs_COLOR0;
    return;
}

#endif
"
}
}
Program "fp" {
SubProgram "gles hw_tier00 " {
""
}
SubProgram "gles hw_tier01 " {
""
}
SubProgram "gles hw_tier02 " {
""
}
}
}
}
}