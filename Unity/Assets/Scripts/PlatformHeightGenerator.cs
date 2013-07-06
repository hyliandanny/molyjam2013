using UnityEngine;
using System.Collections;

public class PlatformHeightGenerator : MonoBehaviour {
	static PlatformHeightGenerator mInstance;
	public static PlatformHeightGenerator Instance() {
		if(!mInstance) {
			mInstance = (PlatformHeightGenerator)GameObject.FindObjectOfType(typeof(PlatformHeightGenerator));
		}
		return mInstance;
	}
	
	//each terrain type will have a different type for distance
	public float GetHeight(Terrain terrain, float distance) {
		return Mathf.Sin(distance)+1;
	}
}
