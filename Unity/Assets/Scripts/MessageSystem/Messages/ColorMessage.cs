using UnityEngine;
using System.Collections;

public class ColorMessage : Message {
	float mR;
	float mG;
	float mB;
	public float R {
		get {
			return mR;
		}
	}
	public float G {
		get {
			return mG;
		}
	}
	public float B{
		get {
			return mB;
		}
	}
	public ColorMessage(float _r, float _g, float _b) {
		mR = _r;
		mG = _g;
		mB = _b;
	}
}
