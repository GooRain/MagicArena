Shader "Raymarching/Mandelbrot"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area ("Area", vector) = (0, 0, 4, 4)
        _Angle ("Angle", range(-3.1415, 3.1415)) = 0
        _Color0 ("Bg Color", Color) = (0, 0, 0, 0)
        _Color1 ("Color 2", Color) = (0, 0, 0, 0)
        _Color2 ("Color 3", Color) = (0, 0, 0, 0)
        _Color3 ("Color 4", Color) = (0, 0, 0, 0)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float4 _Color0;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            
            float4 _Area;
            
            float _Angle;
            
            float4 grad(float t)
            {
                float a_scaled = t * 3;
                float4 gradient_2_3 = lerp(_Color2, _Color3, saturate(a_scaled - 2));
                float4 gradient_1_23 = lerp(_Color1, gradient_2_3, saturate(a_scaled - 1));
                float4 gradient = lerp(_Color0, gradient_1_23, saturate(a_scaled));
                return gradient;
            }
            
            float2 rot(float2 p, float2 pivot, float a)
            {
                float s = sin(a);
                float c = cos(a);
                
                p -= pivot;
                p = float2(p.x * c - p.y * s, p.x * s + p.y * c);
                p += pivot;
                
                return p;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : COLOR 
            {
                float2 c = _Area.xy + (i.uv - .5) * _Area.zw;
                c = rot(c, _Area.xy, _Angle);
                
                float2 z;
                
                float iter;
                
                for(iter = 0; iter < 255; ++iter)
                {
                    z = float2(z.x * z.x - z.y * z.y, 2 * z.x * z.y) + c;
                    //z = float2(z.x * z.x, 2 * z.x * z.y) + c;
                    if (length(z) > 2) break;
                }
                
                float power = (iter / 255);
                
                float4 col = sin(float4(.3, .45, .65, 1)*power*20)*.5+.5;
                
                return col; //grad(power);
            }
            
            ENDCG
        }
    }
}
