using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour {
	
	float amount = 2f;			//how much do I scale by
	float currentPercent = 0f;		//what's my current scale
	float speed = 1f;				//how fast do I scale
	
	bool increasing;				//am I increasing scale?
	
	Vector3 axis;					//what axis do I scale on?
	Vector3 initScale;				//starting scale
	
	// Use this for initialization
	void Start () {
		//axis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		amount = Random.Range(.7f, 2f);
		axis = Vector3.one + Vector3.up;
		//axis = axis.normalized;
		initScale = transform.localScale;
		currentPercent = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(increasing){
			currentPercent += Time.deltaTime * speed;
			
			if(currentPercent >= 1f){
				increasing = false;
			}
		} else {
			currentPercent -= Time.deltaTime * speed;
			if(currentPercent <= 0f){
				increasing = true;
			}
		}
		
		transform.localScale = Vector3.Lerp(initScale, axis * amount, currentPercent);
	}
}
