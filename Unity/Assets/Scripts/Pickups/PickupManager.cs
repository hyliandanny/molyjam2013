using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {
	
	public GameObject pickupPrefab;
	private int pickupsGotten = 0;
	public static PickupManager instance = null;
	
	void Awake() {
		Messenger.AddListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
		if (instance == null)
			instance = this;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	void HandleStageCreatedMessage(Message msg) {
		StageCreatedMessage message = msg as StageCreatedMessage;
		if(message != null) {
			Debug.Log(message.StartX + " " + message.EndX);
			
			for(int i = 0; i < 3; i++) {
				float x = Mathf.Max(30,Random.Range(message.StartX,message.EndX));
				RaycastHit hit;
				if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
					float y = hit.point.y + Random.Range(1,5);
					GameObject pickupObject = (GameObject)Instantiate(pickupPrefab);
					pickupObject.transform.position = new Vector3(x,y,0);
				}
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
