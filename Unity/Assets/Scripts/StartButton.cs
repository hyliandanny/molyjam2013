using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
	
	public GameObject canMan;
	Animator canimator;
	
	public Transform jumpPoint;	//where do I jump from
	public Transform jumpTo;	//where do I jump to
	
	// Use this for initialization
	void Start () {
		canimator = canMan.GetComponent<Animator>();
	}
	
	void Clicked() {
		//Application.LoadLevel(Application.loadedLevel+1);
		//tell the animation to run
		StartCoroutine(RunAway());
	}
	
	IEnumerator RunAway(){
		canimator.SetBool("Running", true);
		while(canMan.transform.position.x < jumpPoint.position.x){
			//keep running
			float runSpeed = 1f;
			canMan.transform.Translate(Vector3.left * runSpeed);
			yield return new WaitForSeconds(0);
		}
		
		canimator.SetBool("Jumping", true);
		while(canMan.transform.position.y < jumpTo.position.y){
			//keep running
			float runSpeed = 1f;
			canMan.transform.Translate(Vector3.up * runSpeed);
			yield return new WaitForSeconds(0);
		}
		
		Application.LoadLevel(Application.loadedLevel+1);
	}
}
