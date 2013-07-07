using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour {
	
	float amount = 2f;			//how much do I scale by
	float currentPercent = 0f;		//what's my current scale
	float speed = 1f;				//how fast do I scale
	
	bool increasing;				//am I increasing scale?
	
	Vector3 axis;					//what axis do I scale on?
	Vector3 initScale;				//starting scale
	Vector3 rRot;					//the random rotation
	Vector3 initRot;				//the initial rotation
	
	// Use this for initialization
	void Start () {
		
		initRot = transform.eulerAngles;
		rRot = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
		
		speed = Random.Range(.5f, 1.5f);
		amount = Random.Range(.7f, 2f);
		axis = Vector3.one + Vector3.up;
		
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
				amount = Random.Range(.7f, 2f);
				speed = Random.Range(.5f, 1.5f);
				initRot = transform.eulerAngles;
				rRot = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
			}
		}
		
		transform.localScale = Vector3.Lerp(initScale, axis * amount, currentPercent);
		transform.eulerAngles = Vector3.Lerp(initRot, rRot, currentPercent);
	}
}
