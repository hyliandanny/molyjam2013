using UnityEngine;
using System.Collections;

public class PlayerDistance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Messenger.Invoke(typeof(PlayerDistanceMessage),new PlayerDistanceMessage(transform.position.x));
	}
}
