Shader "Custom/Overlay" {
	Properties {
        _MainTex ("Texture to blend", 2D) = "black" {}
    }
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            Blend DstColor Zero                 // Multiplicative
            SetTexture [_MainTex] { combine texture }
        }
    }
}
