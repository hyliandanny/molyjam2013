using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	
	public AudioClip jump;
	public AudioClip doubleJump;
	public AudioClip pickup;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void OnPickup()
	{
		audio.PlayOneShot(pickup, 1.0f);
	}
	
	public void DidJump ()
	{
		audio.PlayOneShot(jump, 1.0f);
	}
	
	public void DidDoubleJump()
	{
		audio.PlayOneShot(doubleJump, 1.0f);
	}
}
