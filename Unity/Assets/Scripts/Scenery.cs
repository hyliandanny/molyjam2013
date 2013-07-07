using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {
	
	void Awake() {
		Messenger.AddListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.AddListener(typeof(NextPlatformReachedMessage),HandleNextPlatformReachedMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.RemoveListener(typeof(NextPlatformReachedMessage),HandleNextPlatformReachedMessage);
	}
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			ColorMix cm = GetComponent<ColorMix>();
			if(nextPlatformReached) {
				nextPlatformReached = false;
				cm.rValue = (message.Pickup.pickupType == PickupType.R);
				cm.gValue = (message.Pickup.pickupType == PickupType.G);
				cm.bValue = (message.Pickup.pickupType == PickupType.B);
			}
			else {
				if(message.Pickup.pickupType == PickupType.R) {
					cm.rValue = true;
				}
				else if(message.Pickup.pickupType == PickupType.G) {
					cm.gValue = true;
				}
				else if(message.Pickup.pickupType == PickupType.B) {
					cm.bValue = true;
				}
			}
			
		}
	}
	bool nextPlatformReached = true;
	void HandleNextPlatformReachedMessage(Message msg) {
		NextPlatformReachedMessage message = msg as NextPlatformReachedMessage;
		if(message != null) {
			nextPlatformReached = true;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
