Shader "Custom/ColorMixer" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,0)
		_BlendR ("BlendR", Range (0, 1) ) = 0.5 
		_BlendG ("BlendG", Range (0, 1) ) = 0.5
		_BlendB ("BlendB", Range (0, 1) ) = 0.5
		_Texture1 ("Texture 1", 2D) = "" 
		_Texture2 ("Texture 2", 2D) = ""
		_Texture3 ("Texture 3", 2D) = ""
	}

	SubShader {	
		
		Pass {
			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
			
			Color [_Color]
			
			SetTexture[_Texture1] { 
				ConstantColor (0,0,0, [_BlendR]) 
				Combine texture Lerp(constant) previous
			}
			
			SetTexture[_Texture2] { 
				ConstantColor (0,0,0, [_BlendG]) 
				Combine texture Lerp(constant) previous
			}
			
			SetTexture[_Texture3] { 
				ConstantColor (0,0,0, [_BlendB]) 
				Combine texture Lerp(constant) previous
			}
			
			SetTexture[_Texture3]{
				ConstantColor [_Color]
				Combine constant * previous
			}
		}
	}
	
	FallBack "Diffuse"
}
