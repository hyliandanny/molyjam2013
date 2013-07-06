using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {
	
	public GameObject pickupPrefab;
	private int pickupsGotten = 0;
	public static PickupManager instance = null;
	
	void Awake() {
		if (instance == null)
			instance = this;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Generate the pickups
		
		// This is just a temporary debugging mechanism. Make this time-based.
		if (Input.GetKeyDown(KeyCode.J))
		{
			GameObject pickupObject = (GameObject)Instantiate(pickupPrefab);
			Vector3 pickupPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2f,0f));
			//pickupPosition.y = 0.0f;
			pickupPosition.z = 0.0f;
			pickupObject.transform.position = pickupPosition;
		
		}
	}
	
	// Do everything required for a pickup event outside of the pickup itself
	public void OnPickup (GameObject character) {
		// Keep score
		pickupsGotten++;
		
		// Handle music/sound
		character.SendMessage("OnPickup");
	}
}
