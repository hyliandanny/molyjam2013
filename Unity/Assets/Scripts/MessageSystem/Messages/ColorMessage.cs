using UnityEngine;
using System.Collections;

public class ColorMessage : Message {
	float mR;
	float mG;
	float mB;
	float mPercent;
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
	public float Percent {
		get {
			return mPercent;
		}
	}
	public ColorMessage(float _r, float _g, float _b) {
		mR = _r;
		mG = _g;
		mB = _b;
		mPercent = (mR+mG+mB)/3f;
	}
}
