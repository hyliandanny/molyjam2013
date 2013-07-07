using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {
	
	public PickupClass[] pickupPrefabs;
	private int pickupsGotten = 0;
	public static PickupManager instance = null;
	
	void Awake() {
		Messenger.AddListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
		if (instance == null)
			instance = this;
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void HandleStageCreatedMessage(Message msg) {
		StageCreatedMessage message = msg as StageCreatedMessage;
		if(message != null) {
			
			float startX = Mathf.Max(5,message.StartX);
			float endX = message.EndX;
			float range = (endX-startX)/pickupPrefabs.Length;
			for(int i = 0; i < pickupPrefabs.Length; i++) {
				if(pickupPrefabs[i] != null) {
					float x = Mathf.Max(startX+i*range,startX+(i+1)*range);
					RaycastHit hit;
					if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
						float y = hit.point.y + Random.Range(1,5);
						PickupClass pickup = (PickupClass)Instantiate(pickupPrefabs[i]);
						pickup.transform.position = new Vector3(x,y,0);
					}
				}
			}
		}
	}
	
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			pickupsGotten++;
			// LOOTSIE
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
}
