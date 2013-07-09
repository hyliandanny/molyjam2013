using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {
	
	public PickupClass[] pickupPrefabs;
	private int pickupsGotten = 0;
	public static PickupManager instance = null;
	
	// Constants around pickup locations/sizes
	public float MIN_SCALE = 1;
	public float MAX_SCALE = 3;
	float scale = 1;
	public float MIN_HEIGHT = 7;
	public float MAX_HEIGHT = 15;
	float height = 6;
	
	
	void Awake() {
		Messenger.AddListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
		Messenger.AddListener(typeof(PickupCollectedMessage), HandlePickupCollectedMessage);
		Messenger.AddListener(typeof(BlissedOutMessage),HandleBlissedOutMessage);
		if (instance == null)
			instance = this;
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
		Messenger.RemoveListener (typeof(PickupCollectedMessage), HandlePickupCollectedMessage);
		Messenger.RemoveListener(typeof(BlissedOutMessage),HandleBlissedOutMessage);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	bool blissedOut = false;
	void HandleBlissedOutMessage(Message msg) {
		BlissedOutMessage message = msg as BlissedOutMessage;
		if(message != null) {
			blissedOut = message.blissingOut;
		}
	}
	void HandleStageCreatedMessage(Message msg) {
		StageCreatedMessage message = msg as StageCreatedMessage;
		if(message != null) {
			for(float x = message.StartX + Random.Range(1f,5f); x < message.EndX; x += Random.Range(8,15)) {
				int i = 0;
				//Black Sphere
				if(!blissedOut && Random.value >= .95) {
					i = pickupPrefabs.Length - 1;
				}
				//Other
				else {
					i = Random.Range(0,pickupPrefabs.Length - 1);
				}
				RaycastHit hit;
				if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
					float y = hit.point.y + Random.Range(MIN_HEIGHT,height);
					PickupClass pickup = (PickupClass)Instantiate(pickupPrefabs[i]);
					pickup.transform.position = new Vector3(x,y,0);
					pickup.transform.localScale = new Vector3(scale,scale,scale);
					pickup.transform.rotation = Random.rotation;
				}
			}
		}
	}
	
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			if(message.Pickup.pickupType != PickupType.Black)
				pickupsGotten++;
			
			// LOOTSIE
			Debug.Log ("Obtained " + pickupsGotten + " essences.");
			if (pickupsGotten == 10) {
				Lootsie.AchievementReached("TheColors");
			} else if (pickupsGotten == 20) {
				Lootsie.AchievementReached("DayTripper");
			} else if (pickupsGotten == 30) {
				Lootsie.AchievementReached("TheLongAndWindingRoad");
			} else if (pickupsGotten == 40) {
				Lootsie.AchievementReached("AcrossTheUniverse");
			} else if (pickupsGotten == 50) {
				Lootsie.AchievementReached("ThisColorTastesLikeGaming");
			}
			
		}
	}
	
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
			scale = MIN_SCALE+message.Percent*(MAX_SCALE-MIN_SCALE);
			height = MIN_HEIGHT+message.Percent*(MAX_HEIGHT-MIN_HEIGHT);
		}
	}
}
