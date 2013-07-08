using UnityEngine;
using System.Collections;

public class PathObject : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		// Don't leak objects; destroy when they get out of the screen
		if (gameObject.transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x) {
			Destroy(gameObject);
		}	
	}
}
