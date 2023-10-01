Shader "Unlit/Glow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Threshold ("Glow Threshold", Range(0,1)) = .45
    }

    CGINCLUDE
    #pragma vertex vert
    #pragma fragment frag

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
    };

    sampler2D _MainTex;
    sampler2D _NoiseTex;
    float _Threshold;


    v2f vert (appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }
    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv); // Obtenir la couleur du sprite
                half4 noise = tex2D(_NoiseTex, i.uv + float2(0.75*_Time.x, 0.75*_Time.x));
                float mask = col.r -_Threshold;
                mask = mask > 0;
                mask*= noise.r+.5;
                
                return col + col*mask ;// + mask;
            }
            ENDCG
        }
    }
}
