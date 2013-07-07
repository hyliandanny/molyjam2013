using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelBuilder : MonoBehaviour {
	static LevelBuilder mInstance;
	public static LevelBuilder Instance {
		get {
			return mInstance;
		}
	}
	
	public float R {
		get {
			return r;
		}
	}
	public float G {
		get {
			return g;
		}
	}
	public float B {
		get {
			return b;
		}
	}
	
	public bool cheating = false;
	float r = .4f;
	float g = .4f;
	float b = .4f;
	
	public PlatformGenerator platformGeneratorPrefab;
	
	PlatformGenerator recentPlatformGenerator;
	int platformNumber = 0;
	float lastX = -30;
	float lastY = 0;
	bool gameLost = false;
	void Awake() {
		Messenger.AddListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.AddListener(typeof(PlayerDistanceMessage),HandlePlayerDistanceMessage);
		mInstance = this;
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.RemoveListener(typeof(PlayerDistanceMessage),HandlePlayerDistanceMessage);
	}
	// Use this for initialization
	void Start () {
		CreatePlatform();
		CreatePlatform();
	}
	
	// Update is called once per frame
	void Update () {
		r = Mathf.Max(0,r-0.1f*Time.deltaTime);
		g = Mathf.Max(0,g-0.1f*Time.deltaTime);
		b = Mathf.Max(0,b-0.1f*Time.deltaTime);
		
		if(cheating) {
			r = Mathf.Max(0.3f,r);
			g = Mathf.Max(0.2f,g);
			b = Mathf.Max(0.45f,b);
		}
		if(cheating && Input.GetKeyDown(KeyCode.C)) {
			r = Random.Range(.5f,1f);
			g = Random.Range(.5f,1f);
			b = Random.Range(.5f,1f);
		}
		recentPlatformGenerator.SetBackgroundColor(r,g,b);
		Messenger.Invoke(typeof(ColorMessage),new ColorMessage(r,g,b));
	}
	
	void CreatePlatform() {
		PlatformGenerator pg = (PlatformGenerator)Instantiate(platformGeneratorPrefab);
		pg.platformNumber = platformNumber++;
		pg.transform.parent = transform;
		pg.transform.position = new Vector3(lastX,0,0);
		pg.mLength = Random.Range(20,50);
		pg.GeneratePlatform();
		
		
		pg.transform.position = new Vector3(lastX,lastY - pg.StartY(),0);
		pg.SetForegroundColor(r,g,b);
		lastX = pg.EndX();
		lastY = pg.EndY();
		
		recentPlatformGenerator = pg;
		Messenger.Invoke(typeof(StageCreatedMessage),new StageCreatedMessage(pg.transform.position.x,pg.EndX(),r,g,b));
	}
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			float plus = 0.75f;
			if(message.Pickup.pickupType == PickupType.R) {
				r = Mathf.Min(1,r+plus);
			}
			else if(message.Pickup.pickupType == PickupType.G) {
				g = Mathf.Min(1,g+plus);
			}
			else if(message.Pickup.pickupType == PickupType.B) {
				b = Mathf.Min(1,b+plus);
			}
			Messenger.Invoke(typeof(ColorMessage),new ColorMessage(r,g,b));
		}
	}
	void HandlePlayerDistanceMessage(Message msg) {
		PlayerDistanceMessage message = msg as PlayerDistanceMessage;
		if(message != null) {
			Debug.Log(recentPlatformGenerator.EndX()-message.Distance);
			if(recentPlatformGenerator.EndX()-message.Distance < 15) {
				if(r > 0.1 || g > 0.1 || b > 0.1) {
					CreatePlatform();
				}
			}
			if(!gameLost && recentPlatformGenerator.EndX()-message.Distance < -10) {
				gameLost = true;
				Messenger.Invoke(typeof(GameLostMessage),new GameLostMessage());
			}
		}
	}
}
