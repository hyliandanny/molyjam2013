using UnityEngine;
using System.Collections;

public class UVScroller : MonoBehaviour {

	public float xSpeed;
	public float ySpeed;
	
	float xOffset;
	float yOffset;
	
	// Update is called once per frame
	void Update () {
		xOffset += xSpeed * Time.deltaTime;
		yOffset += ySpeed * Time.deltaTime;
		
		renderer.material.mainTextureOffset = new Vector2(xOffset, yOffset);
	}
}
