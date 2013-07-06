using UnityEngine;
using System.Collections;

public class ColorMix : MonoBehaviour {
	
	public bool rValue;
	public bool gValue;
	public bool bValue;
	
	float r;
	float g;
	float b;
	
	float movingR;
	float movingG;
	float movingB;
	
	
	void Start(){
		Debug.Log("BlendR :" + renderer.material.HasProperty("_BlendR"));
		Debug.Log(renderer.material.shader);
	}
	
	// Update is called once per frame
	void Update () {
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
}
