using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlatformGeneratorMenu : MonoBehaviour {
	[MenuItem ("CONTEXT/PlatformGenerator/Create Platform")]
	static void Create (MenuCommand command) {
        PlatformGenerator pg = (PlatformGenerator)command.context;
        pg.GeneratePlatform();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
