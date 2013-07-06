using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject[] menuButtons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Vector3 inPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			foreach(GameObject menuButton in menuButtons) {
				inPos.z = menuButton.collider.bounds.center.z;
				if(menuButton.collider.bounds.Contains(inPos)) {
					menuButton.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
