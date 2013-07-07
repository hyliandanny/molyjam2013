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
			for(float x = message.StartX + Random.Range(1f,5f); x < message.EndX; x += Random.Range(8,15)) {
				//if(Random.Range(0f,1f) > 0.6f) {
					int i = Random.Range(0,pickupPrefabs.Length);
					RaycastHit hit;
					if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
						float y = hit.point.y + Random.Range(3,6);
						PickupClass pickup = (PickupClass)Instantiate(pickupPrefabs[i]);
						pickup.transform.position = new Vector3(x,y,0);
					}
				//}
			}
		}
	}
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			pickupsGotten++;
		}
	}
}
