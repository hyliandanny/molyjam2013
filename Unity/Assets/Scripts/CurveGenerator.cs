using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveGenerator : MonoBehaviour {
	public bool autoCalc = true;
	static CurveGenerator mInstance;
	public int mNumberOfCurves;
	public List<float> amplitudes;
	public List<float> frequencies;
	public List<float> phases;
	public List<float> weights;
	
	float seed;
	void Awake() {
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
		seed = Random.value*Time.realtimeSinceStartup;
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
	}
	public static CurveGenerator Instance {
		get {
			if(!mInstance) {
				mInstance = (CurveGenerator)GameObject.FindObjectOfType(typeof(CurveGenerator));
				if(!mInstance) {
					GameObject go = new GameObject("CurveGenerator");
					mInstance = go.AddComponent<CurveGenerator>();
				}
			}
			return mInstance;
		}
	}
	int indexOfMinAmp = 0;
	float minAmp = 1000;
	public Vector2[] GetCurve(Terrain terrain, float startX, float length, int samples) {
		indexOfMinAmp = 0;
		minAmp = 1000;
		for(int i = 0; i < mNumberOfCurves; i++) {
			if(amplitudes[i] < minAmp) {
				minAmp = amplitudes[i];
				indexOfMinAmp = i;
			}
		}
		float delta = length/samples;
		Vector2[] curve = new Vector2[samples+1];
		for(int i = 0; i < curve.Length; i++) {
			curve[i] = new Vector2(i*delta,GetY(terrain,startX+i*delta));
		}
		return curve;
	}
	float GetY(Terrain terrain,float x) {
		float y = 0;
		for(int i = 0; i < mNumberOfCurves; i++) {
			if(terrain == Terrain.Ground) {
				y += weights[i]*amplitudes[i]*Mathf.Sin(frequencies[i]*x+phases[i]);
			}
			else {
				//THIS MULTIPLIER SHOULD BE VARIABLE BASED ON THE BLISS LEVEL
				y += weights[i]*amplitudes[i]*Mathf.Sin(frequencies[i]*x+phases[i]);
			}
		}
		return y;
	}
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
		}
	}
	//assuming 2 curves
	void Update() {
		if(autoCalc) {
			float t = Time.timeSinceLevelLoad/10+seed;
			mNumberOfCurves = 2;
			amplitudes[0] = 0.5f*(Mathf.Sin(t)+1);
			frequencies[0] = 1/2f*amplitudes[0];
			phases[0] = 180*(Mathf.Sin(t)+1);
			weights[0] = 1;
		
			amplitudes[1] = 10*0.5f*(Mathf.Cos(t)+1);
			frequencies[1] = 1/amplitudes[1];
			phases[1] = 180*(Mathf.Cos(t)+1);
			weights[1] = 0.75f+(Mathf.Sin(t)+1)/8f;
		}
	}
}
