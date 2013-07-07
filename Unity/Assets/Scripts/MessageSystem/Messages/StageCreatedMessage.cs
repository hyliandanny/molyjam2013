using UnityEngine;
using System.Collections;

public class StageCreatedMessage : Message {
	float mStartX;
	float mEndX;
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
	public float B {
		get {
			return mB;
		}
	}
	public float StartX {
		get {
			return mStartX;
		}
	}
	public float EndX {
		get {
			return mEndX;
		}
	}
	public StageCreatedMessage(float startX,float endX,float r, float g, float b) {
		mStartX = startX;
		mEndX = endX;
		mR = r;
		mG = g;
		mB = b;
	}
}
