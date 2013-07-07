using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveGenerator : MonoBehaviour {
	static CurveGenerator mInstance;
	public int mNumberOfCurves;
	public List<float> amplitudes;
	public List<float> frequencies;
	public List<float> phases;
	public List<float> weights;
	
	void Awake() {
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
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
	public Vector2[] GetCurve(Terrain terrain, float startX, float length, int samples) {
		float delta = length/samples;
		Vector2[] curve = new Vector2[samples+1];
		for(int i = 0; i < curve.Length; i++) {
			curve[i] = new Vector2(i*delta,GetY(startX+i*delta));
		}
		return curve;
	}
	float GetY(float x) {
		float y = 0;
		for(int i = 0; i < mNumberOfCurves; i++) {
			y += weights[i]*amplitudes[i]*Mathf.Sin(frequencies[i]*x+phases[i]);
		}
		return y;
	}
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
		}
	}
}
