using UnityEngine;
using System.Collections;

public class CurveGenerator : MonoBehaviour {
	static CurveGenerator mInstance;
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
			curve[i] = new Vector2(i*delta,Mathf.Sin(startX+i*delta)+2);
		}
		return curve;
	}
}
