using UnityEngine;
using System.Collections;

public class ColorStatusBar : MonoBehaviour {
	
	public GameObject blue;
	public GameObject red;
	public GameObject green;
	public LensFlare flareEffect;
	Vector3 redS, blueS, greenS;
	bool blissedOutMode = false;
	
	void Awake() {
		Messenger.AddListener(typeof(PickupCollectedMessage), PickupCollected);
	}
	
	void OnDestroy() {
		Messenger.RemoveListener(typeof(PickupCollectedMessage), PickupCollected);
	}

	// Use this for initialization
	void Start () {
		redS = red.transform.localScale;
		redS.z = 0f;
		blueS = blue.transform.localScale;
		blueS.z = 0f;
		greenS = green.transform.localScale;
		greenS.z = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!blissedOutMode) {
			if(blue.transform.localScale.x >= 10f && red.transform.localScale.x >= 10f && green.transform.localScale.x >= 10f) {
				blissedOutMode = true;
				Messenger.Invoke(typeof(BlissedOutMessage), new BlissedOutMessage(true));
				flareEffect.enabled = true;
				StartCoroutine(EndBlissMode());
			}
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			if(!blissedOutMode) {
				blissedOutMode = true;
				Messenger.Invoke(typeof(BlissedOutMessage), new BlissedOutMessage(true));
				flareEffect.enabled = true;
				StartCoroutine(EndBlissMode());
			}
		}
	}
	
	IEnumerator EndBlissMode() {
		yield return new WaitForSeconds(15f);
		flareEffect.enabled = false;
		red.transform.localScale = redS;
		blue.transform.localScale = blueS;
		green.transform.localScale = greenS;
		blissedOutMode = false;
		Messenger.Invoke(typeof(BlissedOutMessage), new BlissedOutMessage(false));
	}
	
	void PickupCollected(Message msg) {
		PickupCollectedMessage myMsg = msg as PickupCollectedMessage;
		if(myMsg != null) {
			if (myMsg.Pickup.pickupType == PickupType.B && blue.transform.localScale.x < 10f) {
				Vector3 newScale = blue.transform.localScale;
				newScale += blueS *0.25f;
				blue.transform.localScale = newScale;
			}
			if (myMsg.Pickup.pickupType == PickupType.R && red.transform.localScale.x < 10f) {
				Vector3 newScale = red.transform.localScale;
				newScale += redS *0.25f;
				red.transform.localScale = newScale;
			}
			if (myMsg.Pickup.pickupType == PickupType.G && green.transform.localScale.x < 10f) {
				Vector3 newScale = green.transform.localScale;
				newScale += greenS *0.25f;
				green.transform.localScale = newScale;
			}
		}
	}
	
}
