using UnityEngine;
using System.Collections;

public class PickupGenerator : MonoBehaviour {
	
	public GameObject pickupPrefab;
	
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
}
