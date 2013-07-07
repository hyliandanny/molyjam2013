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
	public void GeneratePlatform(float x){
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
		Vector2[] curve = CurveGenerator.Instance.GetCurve(Terrain.Ground,x,mLength,(int)Mathf.Round(mLength*10));
		Vector2[] uppercurve = CurveGenerator.Instance.GetCurve(Terrain.Hill,x,mLength,(int)Mathf.Round(mLength*10));
		mStartHeight = curve[0].y;
		mEndHeight = curve[curve.Length-1].y;
		if(physics) {
			physics.Generate(curve);
		}
		if(foreground) {
			foreground.Generate(curve);
		}
		if(background != null) {
			background.Generate(uppercurve);
			mStartHeight = uppercurve[0].y;
			mEndHeight = uppercurve[uppercurve.Length-1].y;
		}
		if(border) {
			border.Generate(curve);
		}
	}
	public void GeneratePlatform() {
		GeneratePlatform(transform.position.x);
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
		if(foreground) {
			foreground.GetComponent<ColorMix>().SetColor(r,g,b);
		}
		if(border) {
			border.GetComponent<ColorMix>().SetColor(r,g,b);
		}
	}
	public void SetBackgroundColor(float r, float g, float b) {
		if(background) {
			background.GetComponent<ColorMix>().SetColor(r,g,b);
		}
	}
}
