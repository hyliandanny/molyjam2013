using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour {
	//Parameters
	//Dimensions: These are in real world space
	//Length
	public float mLength;
	
	DynamicRenderMesh foreground;
	DynamicRenderMesh border;
	DynamicPhysicsMesh physics;
	
	public float mEndHeight;
	public float mStartHeight;
	public float EndY() {
		return transform.position.y + mEndHeight;
	}
	public float EndX() {
		return transform.position.x + mLength;
	}
	public float StartY() {
		return mStartHeight;
	}
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
		mStartHeight = curve[0].y;
		mEndHeight = curve[curve.Length-1].y;
	}
}
