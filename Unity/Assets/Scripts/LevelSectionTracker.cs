using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSectionTracker : MonoBehaviour {
	
	public static List<GameObject> levelSections = new List<GameObject>();
	public Transform player;
	public Transform spawnPoint;
	public static float farthestPoint = 0f;
	public static LevelSectionTracker instance = null;
	
	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
