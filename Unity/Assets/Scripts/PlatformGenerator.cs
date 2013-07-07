using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour {
	//Parameters
	//Dimensions: These are in real world space
	//Length
	public float mLength;
	public int platformNumber;
	bool playerHasReachedThePlatform = false;
	
	DynamicRenderMesh foreground;
	DynamicRenderMesh border;
	DynamicRenderMesh background;
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
	void Awake() {
		Messenger.AddListener(typeof(PlayerDistanceMessage),HandlePlayerDistanceMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(PlayerDistanceMessage),HandlePlayerDistanceMessage);
	}
	public void GeneratePlatform() {
		playerHasReachedThePlatform = false;
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
			else if(t.name == "background") {
				background = t.GetComponent<DynamicRenderMesh>();
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
		if(background == null) {
			GameObject go = new GameObject("background");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			background = go.AddComponent<DynamicRenderMesh>();
			background.type = DynamicMeshType.Background;
		}
		if(physics == null) {
			GameObject go = new GameObject("physics");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			physics = go.AddComponent<DynamicPhysicsMesh>();
			physics.type = DynamicMeshType.Physics;
		}
		
		Vector2[] curve = CurveGenerator.Instance.GetCurve(Terrain.Ground,transform.position.x,mLength,(int)Mathf.Round(mLength*10));
		Vector2[] uppercurve = CurveGenerator.Instance.GetCurve(Terrain.Hill,transform.position.x,mLength,(int)Mathf.Round(mLength*10));
		physics.Generate(curve);
		foreground.Generate(curve);
		background.Generate(uppercurve);
		border.Generate(curve);
		mStartHeight = curve[0].y;
		mEndHeight = curve[curve.Length-1].y;
	}
	void HandlePlayerDistanceMessage(Message msg) {
		PlayerDistanceMessage message = msg as PlayerDistanceMessage;
		if(message != null) {
			if(!playerHasReachedThePlatform && message.Distance > transform.position.x) {
				playerHasReachedThePlatform = true;
				Messenger.Invoke(typeof(NextPlatformReachedMessage),new NextPlatformReachedMessage(this));
			}
		}
	}
	public void SetForegroundColor(float r, float g, float b) {
		foreground.GetComponent<ColorMix>().SetColor(r,g,b);
		border.GetComponent<ColorMix>().SetColor(r,g,b);
	}
	public void SetBackgroundColor(float r, float g, float b) {
		background.GetComponent<ColorMix>().SetColor(r,g,b);
	}
}
