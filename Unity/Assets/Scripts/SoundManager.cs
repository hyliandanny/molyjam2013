using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	
	public AudioClip jump;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void DidJump ()
	{
		audio.PlayOneShot(jump, 1.0f);
	}
}
