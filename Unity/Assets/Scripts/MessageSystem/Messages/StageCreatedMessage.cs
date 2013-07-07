using UnityEngine;
using System.Collections;

public class StageCreatedMessage : Message {
	float mStartX;
	float mEndX;
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
	public StageCreatedMessage(float startX,float endX) {
		mStartX = startX;
		mEndX = endX;
	}
}
