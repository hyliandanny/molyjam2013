using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	public CharacterController2D thePlayer;
	public PlayerFollow camFollow;

	// Play the intro bit
	public IEnumerator PlayIntro () {
		yield return new WaitForSeconds(0);
		camFollow.enabled = false;
		Vector3 playerPos = transform.position;
		Vector3 camPos = Camera.main.transform.position;
		playerPos.z = camPos.z;
		Camera.main.transform.position = playerPos;
		float orthoSize = 0.1f;
		Camera.main.orthographicSize = orthoSize;
		int frameCount = 0;
		while(orthoSize < 10f) {
			yield return new WaitForSeconds(0);
			frameCount++;
			playerPos = transform.position;
			playerPos.z = camPos.z;
			orthoSize += 0.1f;
			playerPos.x += 0.07f * frameCount;
			playerPos.y += 0.015f * frameCount;
			Camera.main.orthographicSize = orthoSize;
			Camera.main.transform.position = playerPos;
			thePlayer.controller.SimpleMove(new Vector3(0,-0.07f,0));
		}
		camFollow.enabled = true;
		thePlayer.enabled = true;
		//thePlayer.controller.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
