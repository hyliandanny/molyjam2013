using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	public GameObject[] menuItems;
	public float panSpeed;
	//public Camera miniMapCamera;

	// Use this for initialization
	void OnEnable () {
		Vector3 cameraPos = Camera.main.transform.position;
		cameraPos.x = LevelSectionTracker.instance.spawnPoint.position.x;
		cameraPos.y = LevelSectionTracker.instance.spawnPoint.position.y + 1f;
		Camera.main.transform.position = cameraPos;
		PlayerFollow camFollow = Camera.main.GetComponent<PlayerFollow>();
		camFollow.enabled = false;
		//miniMapCamera.gameObject.SetActive(false);
		StartCoroutine(PanCamera());
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Vector3 inPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			foreach(GameObject menuButton in menuItems) {
				inPos.z = menuButton.collider.bounds.center.z;
				if(menuButton.collider.bounds.Contains(inPos)) {
					menuButton.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
	IEnumerator PanCamera() {
		Vector3 cameraPos = Camera.main.transform.position;
		while(cameraPos.x < LevelSectionTracker.farthestPoint) {
			cameraPos.x += panSpeed * Time.deltaTime;
			Camera.main.transform.position = cameraPos;
			yield return new WaitForSeconds(0);
		}
		yield return new WaitForSeconds(2);
		OnEnable();
	}
}
