using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	public CharacterController2D thePlayer;
	public PlayerFollow camFollow;

	// Play the intro bit
	public IEnumerator PlayIntro () {
		camFollow.enabled = false;
		Vector3 playerPos = transform.position;
		playerPos.z = Camera.main.transform.position.z;
		Camera.main.transform.position = playerPos;
		float orthoSize = 0.1f;
		Camera.main.orthographicSize = orthoSize;
		while(orthoSize < 10f) {
			yield return new WaitForSeconds(0);
			orthoSize += 0.1f;
			Camera.main.orthographicSize = orthoSize;
		}
		camFollow.enabled = true;
		thePlayer.enabled = true;
		thePlayer.controller.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
