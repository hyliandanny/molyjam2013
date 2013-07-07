using UnityEngine;
using System.Collections;

public enum PickupType {
	R,
	G,
	B
}
public class PickupClass : MonoBehaviour {
	public PickupType pickupType;
	
	// Update is called once per frame
	void Update () {
	
		// Don't leak objects; destroy when they get out of the screen
		if (gameObject.transform.position.x <= 0.0f)
		{
		}
		
	}
	
	void OnTriggerEnter (Collider obj)
	{
		// Pickup has been picked up by player?
		if (obj is CharacterController)
		{
			Messenger.Invoke(typeof(PickupCollectedMessage),new PickupCollectedMessage(this));
			// have the manager process the pickup and handle everything a dumb pickup itself shouldn't know about
			
			// Your life is over, pickup.  Time to die.
			Destroy(gameObject);
		}
	}
}
