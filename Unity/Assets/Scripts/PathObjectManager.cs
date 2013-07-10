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
	void CreateHappyObject(float x, Color color) {
		RaycastHit hit;
		if(Physics.Raycast(new Vector3(x,1000,0),Vector3.down,out hit)) {
			if(hit.transform.gameObject.GetComponent<PickupClass>()){
				//I hit a pickup
				return;
			}
			int i = Random.Range(0,prefabs.Length);
			GameObject go = (GameObject)Instantiate(prefabs[i]);
			go.AddComponent<PathObject>();
			go.transform.position = new Vector3(x,hit.point.y, 2);	//set the object behind the runner
			
			float rot = Vector3.Angle(Vector3.up, hit.normal);		//find the angle of the ground
			
			Vector3 tempAngles = go.transform.eulerAngles;			//is it leaning back or forward
			if(hit.normal.x > 0f){
				tempAngles.z -= rot;
			}else{
				tempAngles.z += rot;
			}
			
			go.transform.eulerAngles = tempAngles;					//set the object angle
			go.transform.localScale = Random.Range(1f,4f)* go.transform.localScale;			//give it a little random sizing
			go.transform.Translate(0, go.renderer.bounds.extents.y*0.8f, -5, Space.Self);	//move it up a bit so just the base is touching
			
			HSLColor hsl = HSLColor.FromRGBA(color);
			//Debug.Log("from " + color + " to " + hsl);
			hsl.h += 180f + 15f;
			if(hsl.h > 360f){
				hsl.h -= 360f;
			}
			Color compColor = hsl.ToRGBA();
			go.renderer.material.color = compColor;		//tint the object
		}
	}
	void HandleStageCreatedMessage(Message msg) {
		StageCreatedMessage message = msg as StageCreatedMessage;
		if(message != null) {
			float startX = message.StartX;
			float endX = message.EndX;
			
			float length = endX - startX;
			
			Color color = new Color(message.R, message.G, message.B, 1f);
			
			float d = 0;
			while(d+startX < endX) {
				d += Random.Range(5,7);
				CreateHappyObject(d+startX, color);
			}
		}
	}
}	/*
	HSV RGBtoHSV(Color rgb){
		float var_R = rgb.r;                     //RGB from 0 to 255
		float var_G = rgb.g;
		float var_B = rgb.b;
		
		float var_Min = Mathf.Min( var_R, var_G, var_B );    //Min. value of RGB
		float var_Max = Mathf.Max( var_R, var_G, var_B );    //Max. value of RGB
		float del_Max = var_Max - var_Min;             //Delta RGB value
		
		float L = ( var_Max + var_Min ) / 2f;
	
		HSV hsv = new HSV();
		float V = var_Max;
		float H = 0f;                                //HSL results from 0 to 1
		float S = 0f;
		
		if ( del_Max == 0f )                     //This is a gray, no chroma...
		{
		   H = 0f;                                //HSL results from 0 to 1
		   S = 0f;
		}
		else                                    //Chromatic data...
		{
		   if ( L < 0.5f ){
				S = del_Max / ( var_Max + var_Min );
			} else {
				S = del_Max / ( 2 - var_Max - var_Min );
			}
	
		   float del_R = ( ( ( var_Max - var_R ) / 6f ) + ( del_Max / 2f ) ) / del_Max;
		   float del_G = ( ( ( var_Max - var_G ) / 6f ) + ( del_Max / 2f ) ) / del_Max;
		   float del_B = ( ( ( var_Max - var_B ) / 6f ) + ( del_Max / 2f ) ) / del_Max;
	
	   		if( var_R == var_Max ) {
				H = del_B - del_G;
			} else if ( var_G == var_Max ){
				H = ( 1f / 3f ) + del_R - del_B;
			} else if ( var_B == var_Max ){
				H = ( 2f / 3f ) + del_G - del_R;
			}
	
			if ( H < 0f ) H += 1f;
			if ( H > 1f ) H -= 1f;
		}
		hsv.hue = H;
		hsv.saturation = S;
		hsv.val = V;
		
		return hsv;
	}
}

public struct HSV{
	public float hue;
	public float saturation;
	public float val;
}
*/