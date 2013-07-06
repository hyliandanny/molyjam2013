using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour {
	//Parameters
	//Dimensions: These are in real world space
	//Length
	public float mLength;
	//Height
	public float mHeight;
	
	DynamicRenderMesh foreground;
	DynamicRenderMesh border;
	DynamicPhysicsMesh physics;
	
	//Smoothness
	// 1 is for super smooth
	// 0 is for super jagged
	public float mSmoothness;
	//Variation
	// this is the height variation in the 
	public float mVariation;
	//frequency
	public float mFrequency;
	
	public void GeneratePlatform() {
		if(mLength < 0) {
			mLength = 0;
		}
		foreach(Transform t in transform) {
			if(t.name == "foreground") {
				foreground = t.GetComponent<DynamicRenderMesh>();
			}
			else if(t.name == "border") {
				border = t.GetComponent<DynamicRenderMesh>();
			}
			else if(t.name == "physics") {
				physics = t.GetComponent<DynamicPhysicsMesh>();
			}
		}
		
		if(foreground == null) {
			GameObject go = new GameObject("foreground");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			foreground = go.AddComponent<DynamicRenderMesh>();
			foreground.type = DynamicMeshType.Foreground;
		}
		if(border == null) {
			GameObject go = new GameObject("border");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			border = go.AddComponent<DynamicRenderMesh>();
			border.type = DynamicMeshType.Border;
		}
		if(physics == null) {
			GameObject go = new GameObject("physics");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			physics = go.AddComponent<DynamicPhysicsMesh>();
			physics.type = DynamicMeshType.Physics;
		}
		
		Vector2[] curve = CurveGenerator.Instance.GetCurve(Terrain.Ground,transform.position.x,mLength,(int)Mathf.Round(mLength*10));
		physics.Generate(curve);
		foreground.Generate(curve);
		border.Generate(curve);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
