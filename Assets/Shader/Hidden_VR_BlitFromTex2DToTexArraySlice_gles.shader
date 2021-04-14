//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/VR/BlitFromTex2DToTexArraySlice" {
Properties {
_MainTex ("Texture", 2D) = "" { }
}
SubShader {
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 610
Program "vp" {
}
Program "fp" {
}
}
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 110014
Program "vp" {
}
Program "fp" {
}
Program "gp" {
}
}
}
}