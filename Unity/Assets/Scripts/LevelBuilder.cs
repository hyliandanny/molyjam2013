using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelBuilder : MonoBehaviour {
	public PlatformGenerator platformGeneratorPrefab;
	
	float lastX = -10;
	float lastY = 0;
	List<PlatformGenerator> platforms;
	void Awake() {
		platforms = new List<PlatformGenerator>();
		
	}
	// Use this for initialization
	void Start () {
		CreatePlatform();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			CreatePlatform();
		}
	}
	
	void CreatePlatform() {
		PlatformGenerator pg = (PlatformGenerator)Instantiate(platformGeneratorPrefab);
		platforms.Add(pg);
		pg.transform.parent = transform;
		pg.transform.position = new Vector3(lastX,0,0);
		pg.mLength = Random.Range(40,80);
		pg.GeneratePlatform();
		
		
		pg.transform.position = new Vector3(lastX,lastY - pg.StartY(),0);
		
		lastX = pg.EndX();
		lastY = pg.EndY();
		
		Messenger.Invoke(typeof(StageCreatedMessage),new StageCreatedMessage(pg.transform.position.x,pg.EndX()));
	}
}
