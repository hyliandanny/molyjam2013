using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	
	public AudioClip jump;
	public AudioClip doubleJump;
	public AudioClip pickup;
	public AudioClip death;
	
	void Awake() {
		Messenger.AddListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.AddListener(typeof(GameLostMessage),HandleGameLostMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(PickupCollectedMessage),HandlePickupCollectedMessage);
		Messenger.RemoveListener(typeof(GameLostMessage),HandleGameLostMessage);
	}
	void HandlePickupCollectedMessage(Message msg) {
		PickupCollectedMessage message = msg as PickupCollectedMessage;
		if(message != null) {
			audio.PlayOneShot(pickup, 1.0f);
		}
	}
	
	public void DidJump ()
	{
		audio.PlayOneShot(jump, 1.0f);
	}
	
	public void DidDoubleJump()
	{
		audio.PlayOneShot(doubleJump, 1.0f);
	}
	
	public void HandleGameLostMessage (Message msg)
	{
		GameLostMessage message = msg as GameLostMessage;
		if(message != null) {
			audio.PlayOneShot (death, 1.0f);
		}
	}
}
