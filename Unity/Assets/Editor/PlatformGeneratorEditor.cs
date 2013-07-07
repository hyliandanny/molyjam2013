using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlatformGenerator))]
public class PlatformGeneratorEditor : Editor {
	void OnSceneGUI() {
		PlatformGenerator pg = (PlatformGenerator)target;
		Vector3 p = pg.transform.position;
		p.z = 0;
		pg.transform.position = p;
        pg.GeneratePlatform();
    }
}
