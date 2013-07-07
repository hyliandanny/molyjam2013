using UnityEngine;
using System.Collections;

public class LevelAudio : MonoBehaviour {
	float targetVolume;
	float volume;
	void Awake() {
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
		targetVolume = 0;
		volume = 0;
		GetComponent<AudioSource>().volume = 0;
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
	}
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
			targetVolume = (message.R + message.G + message.B)/3f;
		}
	}
	void Update() {
		if(targetVolume != volume) {
			volume = Mathf.MoveTowards(volume,targetVolume,Time.deltaTime);
			GetComponent<AudioSource>().volume = volume;
		}
	}
}
