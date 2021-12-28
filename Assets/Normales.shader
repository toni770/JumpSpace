Shader "Unlit/Normales"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        [KeywordEnum(On, Off)]_UseColor("Use Color", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature _USECOLOR_ON _USECOLOR_OFF

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            float4 NormalAnimation(float4 vertex, float4 normal)
            {
                float4 vert;
                vertex += abs(_SinTime.w) * normal;
                vert = UnityObjectToClipPos(vertex);
                return vert;
            }

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = NormalAnimation(v.vertex, v.normal) ;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col;
                #if _USECOLOR_ON
                col =  _Color;
                #else
                col = tex2D(_MainTex, i.uv);
                #endif
                return col;
            }
            ENDCG
        }
    }
}
