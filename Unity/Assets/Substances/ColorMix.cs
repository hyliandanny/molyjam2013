using UnityEngine;
using System.Collections;

public class ColorMix : MonoBehaviour {
	public bool useSharedMaterial = false;
	public bool useComplimentaryColor = false;
	
	float r;
	float g;
	float b;
	
	float movingR;
	float movingG;
	float movingB;
	
	void Awake() {
		if(useSharedMaterial) {
			movingR = r = LevelBuilder.Instance.R;
			movingG = g = LevelBuilder.Instance.G;
			movingB = b = LevelBuilder.Instance.B;
		}
	}
	public void SetColor(float _r, float _g, float _b) {
		r = _r;
		g = _g;
		b = _b;
	}
	
	// Update is called once per frame
	void Update () {
		movingR = Mathf.Clamp(Mathf.MoveTowards(movingR, r, Time.deltaTime),0,r);
		movingG = Mathf.Clamp(Mathf.MoveTowards(movingG, g, Time.deltaTime),0,g);
		movingB = Mathf.Clamp(Mathf.MoveTowards(movingB, b, Time.deltaTime),0,b);
			
		Color mixedColor = new Color(movingR, movingG, movingB, 1f);
		
		/*if(useComplimentaryColor){
			HSLColor hsl = HSLColor.FromRGBA(mixedColor);
			hsl.h += 180f - 15f;
			if(hsl.h > 360f){
				hsl.h -= 360f;
			}
			mixedColor = hsl.ToRGBA();		//tint the object
		}*/
		
		if(useSharedMaterial) {
			renderer.sharedMaterial.SetFloat("_BlendR", movingR);
			renderer.sharedMaterial.SetFloat("_BlendG", movingG);
			renderer.sharedMaterial.SetFloat("_BlendB", movingB);
			renderer.sharedMaterial.SetColor("_Color", mixedColor);
		}
		else {
			renderer.material.SetFloat("_BlendR", movingR);
			renderer.material.SetFloat("_BlendG", movingG);
			renderer.material.SetFloat("_BlendB", movingB);
			renderer.material.SetColor("_Color", mixedColor);
		}
	}
}
