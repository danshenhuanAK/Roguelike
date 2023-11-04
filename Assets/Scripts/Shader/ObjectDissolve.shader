Shader "Custom/ObjectDissolve"
{
     Properties
    {
        _MainTex("Main Texture", 2D) = "defaulttexture" {}
        _NoiseTex("Noise Map", 2D) = "defaulttexture" {}                            //噪声图
        _DissolveTex ("Dissolve Color", COLOR) = (0, 0, 0, 0)                       //边缘色
        _Dissolve("Dissolve Amount", Range(0, 1)) = 0                               //消融程度
    }
    SubShader
    {
       Tags{
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Cull Off ZWrite Off ZTest Always
        Blend One OneMinusSrcAlpha

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

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            sampler2D _NoiseTex;        
            float _Dissolve;


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 MainCol = tex2D(_MainTex, i.uv);
                MainCol.rgb *= MainCol.a;
                // 随机阈值
                fixed randValue = tex2D(_NoiseTex, i.uv).r;   
                float cutout = 0.6 - 1.2 * _Dissolve + randValue;
                clip(cutout - 0.5);   
    
                float weight = 1.0 - clamp(8 * cutout - 4, 0.0, 1.0);
                float2 burnUV = float2(weight, 0);
                fixed3 edgeColor = weight * tex2D(_DissolveTex, burnUV ).xyz;
                edgeColor = step(0.05, MainCol.a) * edgeColor;
 
                // // 需要剔除的部分    
                fixed3 finalCol = MainCol.rgb + edgeColor;   
                return fixed4(finalCol, MainCol.a);
            }
            ENDCG
        }
    }
}
