// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32947,y:32505,varname:node_4795,prsc:2|emission-2754-OUT,alpha-6074-A;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32451,y:32628,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0000000000000000f000000000000000,ntxv:0,isnm:False|UVIN-7962-OUT;n:type:ShaderForge.SFN_Color,id:7050,x:32463,y:32952,ptovrint:False,ptlb:node_7050,ptin:_node_7050,varname:node_7050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9117647,c2:0.08715395,c3:0.08715395,c4:1;n:type:ShaderForge.SFN_Multiply,id:2754,x:32674,y:32837,varname:node_2754,prsc:2|A-6074-RGB,B-7050-RGB;n:type:ShaderForge.SFN_Time,id:5712,x:31817,y:32870,varname:node_5712,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2335,x:31781,y:32241,varname:node_2335,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:7962,x:32154,y:32241,varname:node_7962,prsc:2|A-2335-UVOUT,B-6784-OUT;n:type:ShaderForge.SFN_Multiply,id:9233,x:32041,y:32717,varname:node_9233,prsc:2|A-5712-TSL,B-7-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7,x:32126,y:32957,ptovrint:False,ptlb:node_7,ptin:_node_7,varname:node_7,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Tex2d,id:2984,x:31720,y:32493,ptovrint:False,ptlb:node_2984,ptin:_node_2984,varname:node_2984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:27fda772e5ea7b249960a02dfaeee3cd,ntxv:0,isnm:False|UVIN-2815-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2125,x:31948,y:32607,ptovrint:False,ptlb:node_2125,ptin:_node_2125,varname:node_2125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:6784,x:32006,y:32410,varname:node_6784,prsc:2|A-2984-RGB,B-2125-OUT;n:type:ShaderForge.SFN_TexCoord,id:9794,x:31452,y:32672,varname:node_9794,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:2815,x:31720,y:32726,varname:node_2815,prsc:2|A-9233-OUT,B-9794-UVOUT;proporder:6074-7050-7-2984-2125;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_7050 ("node_7050", Color) = (0.9117647,0.08715395,0.08715395,1)
        _node_7 ("node_7", Float ) = 3
        _node_2984 ("node_2984", 2D) = "white" {}
        _node_2125 ("node_2125", Float ) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _node_7050;
            uniform float _node_7;
            uniform sampler2D _node_2984; uniform float4 _node_2984_ST;
            uniform float _node_2125;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_5712 = _Time + _TimeEditor;
                float2 node_2815 = ((node_5712.r*_node_7)+i.uv0);
                float4 _node_2984_var = tex2D(_node_2984,TRANSFORM_TEX(node_2815, _node_2984));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX((float3(i.uv0,0.0)+(_node_2984_var.rgb*_node_2125)), _MainTex));
                float3 emissive = (_MainTex_var.rgb*_node_7050.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_MainTex_var.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
