using UnityEngine;
using System.Collections;

public class DeathBoxFollow : MonoBehaviour {
	
	public Transform character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 myPos = transform.position;
		myPos.x = character.position.x;
		//myPos.y = character.position.y;
		transform.position = myPos;
	}
}
