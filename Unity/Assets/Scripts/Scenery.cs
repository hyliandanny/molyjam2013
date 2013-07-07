using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour {
	
	void Awake() {
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
	}
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
			GetComponent<ColorMix>().SetColor(message.R,message.G,message.B);
		}
	}
}
