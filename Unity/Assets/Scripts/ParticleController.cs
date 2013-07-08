using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	
	public ParticleSystem[] allParticles;
	float countdown;		//when do I start emitting?
	Color mColor;
	
	// Use this for initialization
	void Awake () {
		allParticles = GetComponentsInChildren<ParticleSystem>() as ParticleSystem[];
		countdown = Random.Range(.5f, 2f);
		Messenger.AddListener(typeof(ColorMessage), SetColor);
	}
	
	void OnDestroy() {
		Messenger.RemoveListener(typeof(ColorMessage), SetColor);
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if(countdown <= 0f){
			countdown = Random.Range(5f, 20f);
			int r = Random.Range(0, allParticles.Length);
			allParticles[r].startColor = mColor;
			
			//allParticles[r].gameObject.particleEmitter.renderer.material.SetColor("_TintColor", mColor);
			allParticles[r].Emit(1);
		//	Debug.Log("emitting from: " + allParticles[r]);
		}
	}
	
	void SetColor(Message msg) {
		ColorMessage myMsg = msg as ColorMessage;
		if(myMsg != null) {
			mColor = new Color(myMsg.R, myMsg.G, myMsg.B);
		//	Debug.Log(mColor);
		}
	}
}
