using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSectionTracker : MonoBehaviour {
	
	public static List<GameObject> levelSections = new List<GameObject>();
	public Transform player;
	public Transform spawnPoint;
	public static Vector3 spawnVector;
	public static float farthestPoint = 0f;
	public static LevelSectionTracker instance = null;
	
	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		spawnVector = spawnPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.isLoadingLevel) {
			if(player != null) {
				farthestPoint = player.position.x;
			}
		}
	}
	
	void OnLevelSegmentAdded(GameObject levelSection) {
		levelSections.Add(levelSection);
	}
	
	void OnLevelWasLoaded(int level) {
		if(level == 2) {
			//Do I need to do anything here?
		}
		else {
			foreach(GameObject levelSection in levelSections) {
				Destroy(levelSection);
			}
			levelSections = new List<GameObject>();
			if(instance != this)
				Destroy(gameObject);
		}
	}
}
