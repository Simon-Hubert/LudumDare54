// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Glow"
{
    Properties
    {
        _MainTex ("NoiseTexture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue" = "Transparent+1"
        }
        LOD 100

        Pass
        {
            ZWrite Off
            //Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct Vertex
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            struct Fragment
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Fragment vert (Vertex v)
            {
                Fragment o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv2 = v.uv2;

                return o;
            }

            fixed4 frag (Fragment i) : COLOR//SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float mask = (col.r-.45);
                mask = mask > 0;

                return fixed4(mask*1.5, mask*1.5, mask*1.5,1);
            }
            ENDCG
        }
    }
}
