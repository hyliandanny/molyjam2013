using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	public GameObject[] menuItems;
	public float panSpeed;

	// Use this for initialization
	void Start () {
		Vector3 cameraPos = Camera.main.transform.position;
		cameraPos.x = LevelSectionTracker.spawnVector.x;
		Camera.main.transform.position = cameraPos;
		StartCoroutine(PanCamera());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			Vector3 inPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			foreach(GameObject menuItem in menuItems) {
				inPos.z = menuItem.collider.bounds.center.z;
				if(menuItem.collider.bounds.Contains(inPos)) {
					menuItem.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
		}
	}
	
	IEnumerator PanCamera() {
		while(Camera.main.transform.position.x < LevelSectionTracker.farthestPoint) {
			Vector3 cameraPos = Camera.main.transform.position;
			cameraPos.x += panSpeed * Time.deltaTime;
			Camera.main.transform.position = cameraPos;
			yield return new WaitForSeconds(0);
		}
		StopAllCoroutines();
		Start();
	}
}
