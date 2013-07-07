using UnityEngine;
using System.Collections;

public class ColorMix : MonoBehaviour {
	public bool digitalColor = true;
	public bool rValue;
	public bool gValue;
	public bool bValue;
	
	float r;
	float g;
	float b;
	
	float movingR;
	float movingG;
	float movingB;
	
	public void SetColor(float _r, float _g, float _b) {
		digitalColor = false;
		r = _r;
		g = _g;
		b = _b;
	}
	
	// Update is called once per frame
	void Update () {
		if(digitalColor) {
			if(rValue){
				r = 1f;
			} else {
				r = 0f;
			}
		
			if(gValue){
				g = 1f;
			} else {
				g = 0f;
			}
		
			if(bValue){
				b = 1f;
			} else {
				b = 0f;
			}
			
			movingR = Mathf.MoveTowards(movingR, r, Time.deltaTime);
			movingG = Mathf.MoveTowards(movingG, g, Time.deltaTime);
			movingB = Mathf.MoveTowards(movingB, b, Time.deltaTime);
		
			Color mixedColor = new Color(movingR, movingG, movingB, 1f);
			renderer.sharedMaterial.SetFloat("_BlendR", movingR/(movingR+movingG+movingB));
			renderer.sharedMaterial.SetFloat("_BlendG", movingG/(movingR+movingG+movingB));
			renderer.sharedMaterial.SetFloat("_BlendB", movingB/(movingR+movingG+movingB));
			renderer.sharedMaterial.SetColor("_Color", mixedColor);
		}
		else {
			movingR = Mathf.Clamp(Mathf.MoveTowards(movingR, r, Time.deltaTime),0,r);
			movingG = Mathf.Clamp(Mathf.MoveTowards(movingG, g, Time.deltaTime),0,g);
			movingB = Mathf.Clamp(Mathf.MoveTowards(movingB, b, Time.deltaTime),0,b);
			
			Color mixedColor = new Color(movingR, movingG, movingB, 1f);
			renderer.material.SetFloat("_BlendR", movingR);
			renderer.material.SetFloat("_BlendG", movingG);
			renderer.material.SetFloat("_BlendB", movingB);
			renderer.material.SetColor("_Color", mixedColor);
		}
	}
}
