Shader "Explorer/fractor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Surface("Surface", vector) = (0, 0, 4, 4)
			_Angle("Angle", range(-3.14159265358, 3.14159265358)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float4 _Surface;
			float _Angle;
            sampler2D _MainTex;

			float2 rot(float2 p, float2 pivot, float a)
			{
				float s = sin(a);
				float c = cos(a);
				p -= pivot;
				p = float2(p.x* c-p.y*s, p.x*s+p.y*c);
				p += pivot;

				return p;
			}
            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = _Surface.xy + (i.uv - 0.5)* _Surface.zw;

				c = rot(c, _Surface.xy, _Angle);

				float2 z;
				float iterations;
				for (iterations = 0; iterations<400; iterations++){
					z = float2(z.x*z.x-z.y*z.y,2*z.x*z.y) + c;
					if(length(z) > 2)
					{
						break;
					}
				}
				float mandalbrot = sqrt(iterations / 400);
				float4 col = sin(float4(.6,.90,.2, 1) * mandalbrot);
                return col;
            }
            ENDCG
        }
    }
}
