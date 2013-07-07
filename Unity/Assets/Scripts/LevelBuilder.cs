using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelBuilder : MonoBehaviour {
	public PlatformGenerator platformGeneratorPrefab;
	
	int platformNumber = 0;
	float lastX = -10;
	float lastY = 0;
	public List<PickupType> pickupTypes;
	List<PlatformGenerator> platforms;
	void Awake() {
		Messenger.AddListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.AddListener(typeof(NextPlatformReachedMessage),HandleNextPlatformReachedMessage);
		platforms = new List<PlatformGenerator>();
		pickupTypes = new List<PickupType>();
		
	}
	// Use this for initialization
	void Start () {
		CreatePlatform();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			CreatePlatform();
		}
	}
	
	void CreatePlatform() {
		PlatformGenerator pg = (PlatformGenerator)Instantiate(platformGeneratorPrefab);
		pg.platformNumber = platformNumber++;
		platforms.Add(pg);
		pg.transform.parent = transform;
		pg.transform.position = new Vector3(lastX,0,0);
		pg.mLength = Random.Range(40,80);
		pg.GeneratePlatform();
		
		
		pg.transform.position = new Vector3(lastX,lastY - pg.StartY(),0);
		
		lastX = pg.EndX();
		lastY = pg.EndY();
		
		Messenger.Invoke(typeof(StageCreatedMessage),new StageCreatedMessage(pg.transform.position.x,pg.EndX()));
	}
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			pickupTypes.Add(message.Pickup.pickupType);
			//only create platform once,
			if(pickupTypes.Count == 1) {
				CreatePlatform();
			}
			//if not modify the already created
			else {
			}
			platforms[platforms.Count-1].AddColor(message.Pickup.pickupType);
		}
	}
	void HandleNextPlatformReachedMessage(Message msg) {
		NextPlatformReachedMessage message = msg as NextPlatformReachedMessage;
		if(message != null) {
			pickupTypes.Clear();
		}
	}
}
