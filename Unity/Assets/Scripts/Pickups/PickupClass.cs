using UnityEngine;
using System.Collections;

public class PickupClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
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
			// have the manager process the pickup and handle everything a dumb pickup itself shouldn't know about
			PickupManager.instance.OnPickup(obj.gameObject);
			
			// Your life is over, pickup.  Time to die.
			Destroy(gameObject);
		}
	}
}
