Shader "Custom/seasons-shader"
{
    Properties
    {
        // We will swap between summer and winter texture
        _SummerTex("Summer Texture", 2D) = "white" {}
        _WinterTex("Winter Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex SeasonVert
            #pragma fragment SeasonFrag

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

            sampler2D _SummerTex;
            sampler2D _WinterTex;

            // Global shader variable controls which tiles are used, Summer or Winter
            float _UseSummerTexture;

            v2f SeasonVert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 SeasonFrag(v2f i) : SV_Target
            {
                float4 color = float4(0, 0, 0, 1);

                if (_UseSummerTexture == 1.0)
                {
                    color = tex2D(_SummerTex, i.uv);
                }
                else
                {
                    color = tex2D(_WinterTex, i.uv);
                }

                return color;
            }
            ENDCG
        }
    }
}
