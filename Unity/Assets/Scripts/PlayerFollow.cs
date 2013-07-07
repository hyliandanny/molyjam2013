using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {
	
	public Transform character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Me"+transform.position);
		Debug.Log("Target"+character.transform.position);
		Vector3 myPos = transform.position;
		myPos.x = character.position.x;
		transform.position = myPos;
	}
}
