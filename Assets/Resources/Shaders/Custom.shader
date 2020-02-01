Shader "Custom/MaskShader" {
	SubShader{
		colormask 0
		Pass {
			Stencil {
				Ref 1
				Comp Greater
				Pass Replace 
			}
		}
	}
}