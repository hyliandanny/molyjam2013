using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorPlanetGenerator : MonoBehaviour {
	
	
	List<Color> allColors = new List<Color>();
	
	void Awake() {
		Messenger.AddListener(typeof(StageCreatedMessage), StageCreated);
		allColors = new List<Color>();
		
	//	GenerateRainbow();
	//	CreatePlanet();
		renderer.enabled = false;
	}
	
	void OnDestroy() {
		Messenger.RemoveListener(typeof(StageCreatedMessage), StageCreated);
	}
	
	void StageCreated(Message msg) {
		StageCreatedMessage myMsg = msg as StageCreatedMessage;
		if(myMsg != null) {
//			Debug.Log("Got Message");
			allColors.Add(new Color(myMsg.R, myMsg.G, myMsg.B));
		}
	}
	
	void GenerateRainbow(){
		for(int i = 0; i < 25; i++){
			allColors.Add(new Color(Random.value, Random.value, Random.value, 1f)); 
		}
	}
	
	public void CreatePlanet(){
		if(allColors.Count == 0){
			allColors.Add(Color.black);
		}
		renderer.enabled = true;
		int texSize = Mathf.Max(allColors.Count, 512);	//make sure the texture is at least 512, but up to as big as the color size
		
		Texture2D texture = new Texture2D(texSize, texSize);
        renderer.material.mainTexture = texture;
		
		int lineWidth = (int)Mathf.Round(((float)texture.width/2f) / allColors.Count);
	//	Debug.Log("Line width " + lineWidth + ", texture size " + texSize + ", allColors " + allColors.Count);
		//rescale the colors to be the right width
		List<Color> newColors = new List<Color>(texture.width/2);
		for(int i = 0; i < allColors.Count; i++){
			for(int j = 0; j < lineWidth; j++){
				newColors.Add(allColors[i]);
			}
		}
		allColors = newColors;
		
		int radius = 0;		//the current pixel radius
		Vector2 center = new Vector2(texture.width/2, texture.height/2);
		
        int y = 0;
        while (y < texture.height) {
            int x = 0;
            while (x < texture.width) {
				Color _color = new Color(1,1,1,0);
				Vector2 pixelVector = new Vector2(x, y);
                int colorIndex = (int)(pixelVector - center).magnitude;
				if(colorIndex >= 0 && colorIndex < allColors.Count){
					if(allColors[colorIndex] != null){
						_color = allColors[colorIndex];
					}
				}
                texture.SetPixel(x, y, _color);
                ++x;
            }
            ++y;
        }
        texture.Apply();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.L)){
			Messenger.Invoke(typeof(GameLostMessage), new GameLostMessage());
		}
	}
}
