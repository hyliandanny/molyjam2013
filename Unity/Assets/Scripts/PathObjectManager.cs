using UnityEngine;
using System.Collections;

public class PathObjectManager : MonoBehaviour {
	public GameObject[] prefabs;
	void Awake() {
		Messenger.AddListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
	}
	void OnDestroy() {
		Messenger.RemoveListener(typeof(StageCreatedMessage),HandleStageCreatedMessage);
	}
	void CreateHappyObject(float x) {
		RaycastHit hit;
		if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
			int i = Random.Range(0,prefabs.Length);
			GameObject go = (GameObject)Instantiate(prefabs[i]);
			go.AddComponent<PathObject>();
			go.transform.position = new Vector3(x,hit.point.y + Random.Range (-30, 30), -10);
			float scaleFactor = Random.Range(0f, 5.01f); 
			go.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
			float rot = Random.value;
			if(hit.normal.x < 0) {
				rot = -rot;
			}
			go.transform.Translate(0, 0.5f, 0, Space.Self);
			rot = Vector3.Angle(Vector3.up, hit.normal);
			//go.transform.RotateAround(Vector3.forward,rot);
			
		}
	}
	void HandleStageCreatedMessage(Message msg) {
		StageCreatedMessage message = msg as StageCreatedMessage;
		if(message != null) {
			float startX = message.StartX;
			float endX = message.EndX;
			
			float length = endX - startX;
			
			float d = 0;
			while(d+startX < endX) {
				d += Random.Range(5,10);
				CreateHappyObject(d+startX);
			}
		}
	}
}
