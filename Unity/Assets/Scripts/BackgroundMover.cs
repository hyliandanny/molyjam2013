using UnityEngine;
using System.Collections;

public class BackgroundMover : MonoBehaviour {
	public float mTranslateSpeed;
	public PlatformGenerator platformGeneratorPrefab;
	PlatformGenerator recentPlatformGenerator;
	
	float totalLength = 0;
	float x = -30;
	float lastY = 0;
	
	void Awake() {
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
	}
	
	void Start () {
		CreatePlatform();
		CreatePlatform();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(mTranslateSpeed,0,0);
		float f = Camera.mainCamera.transform.position.x - (totalLength + transform.position.x);
		if(f > -45) {
			CreatePlatform();
		}
	}
	void CreatePlatform() {
		PlatformGenerator pg = (PlatformGenerator)Instantiate(platformGeneratorPrefab);
		pg.transform.parent = transform;
		pg.transform.localPosition = new Vector3(x,0,0);
		pg.mLength = Random.Range(20,50);
		pg.GeneratePlatform(x);
		
		float delta = 0;
		if(recentPlatformGenerator) {
			delta = recentPlatformGenerator.transform.localPosition.y + lastY-pg.mStartHeight;
		}
		pg.transform.localPosition = new Vector3(x,delta,0);
		
		x += pg.mLength;
		lastY = pg.mEndHeight;
		
		recentPlatformGenerator = pg;
		totalLength += pg.mLength;
	}
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
			recentPlatformGenerator.SetBackgroundColor(
				(0.9f * message.R), 
				(1.2f * message.G),
				(0.8f * message.B)
			);
			//recentPlatformGenerator.SetBackgroundColor(message.R,message.G,message.B);
		}
	}
}
