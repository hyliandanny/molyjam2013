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
			// Trigger the next stage queue theme
			
			Debug.Log("Pickup!");
			// Play the pickup sound
			
			// Remove this pickup
			Destroy(gameObject);
		}
	}
}
