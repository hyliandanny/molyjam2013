using UnityEngine;
using System.Collections;

public class FillScreen : MonoBehaviour {
	
	public bool gamePlayScene = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera cam = Camera.main;
 
        float pos = (cam.nearClipPlane + 0.01f);
 
        //transform.position = cam.transform.position + cam.transform.forward * pos;
 
        float h = Mathf.Tan(cam.fov*Mathf.Deg2Rad*0.5f)*pos*112f;
 
        transform.localScale = new Vector3(h*cam.aspect,h,0f);
		
		if(gamePlayScene){
			Vector3 nScale = transform.localScale;
			nScale.x = nScale.x/5f;
			transform.localScale = nScale;
		}
		
	}
}
