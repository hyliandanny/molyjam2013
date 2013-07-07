using UnityEngine;
using System.Collections;

public enum PickupType {
	R,
	G,
	B,
	Black
}
public class PickupClass : MonoBehaviour {
	public PickupType pickupType;
	
	ParticleSystem collectParticles;
	
	// Update is called once per frame
	void Update () {
	
		// Don't leak objects; destroy when they get out of the screen
		if (gameObject.transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x)
		{
			Destroy(gameObject);
		}
		
	}
	
	void OnTriggerEnter (Collider obj)
	{
		// Pickup has been picked up by player?
		if (obj is CharacterController)
		{
			collectParticles = GetComponentInChildren<ParticleSystem>() as ParticleSystem;
			collectParticles.Play();
			collectParticles.transform.parent = transform.parent;
			Destroy(collectParticles.transform.gameObject, 3f);
			
			Messenger.Invoke(typeof(PickupCollectedMessage),new PickupCollectedMessage(this));
			// have the manager process the pickup and handle everything a dumb pickup itself shouldn't know about
			
			// Your life is over, pickup.  Time to die.
			Destroy(gameObject);
		}
	}
}
