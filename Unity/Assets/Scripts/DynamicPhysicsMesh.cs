using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshCollider))]
public class DynamicPhysicsMesh : DynamicMesh {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	new public void Generate(Vector2[] curve) {
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		if(!mf.sharedMesh) {
			mf.sharedMesh = new Mesh();
		}
		MeshCollider mc = gameObject.GetComponent<MeshCollider>();
		if(!mc.sharedMesh) {
			mc.sharedMesh = mf.sharedMesh;
		}
		Mesh mesh = mf.sharedMesh;
		//create a cube for now
		mesh.Clear();
		
		int segments = curve.Length-1;
		Vector3[] vertices = new Vector3[4*segments];
		int[] tris = new int[3*vertices.Length];
 
		for(int i = 0; i < segments; i++) {
			vertices[i*4]   = new Vector3(((Vector2)curve[i]).x,   ((Vector2)curve[i]).y,  -depth);
			vertices[1+i*4] = new Vector3(((Vector2)curve[i]).x,   ((Vector2)curve[i]).y,   depth);
			vertices[2+i*4] = new Vector3(((Vector2)curve[i+1]).x, ((Vector2)curve[i+1]).y,-depth);
			vertices[3+i*4] = new Vector3(((Vector2)curve[i+1]).x, ((Vector2)curve[i+1]).y, depth);
			
			tris[i*6]		= 0+i*4;
			tris[i*6+1] 	= 1+i*4;
			tris[i*6+2] 	= 2+i*4;
			tris[i*6+3] 	= 2+i*4;
			tris[i*6+4] 	= 1+i*4;
			tris[i*6+5] 	= 3+i*4;
		}
        mesh.vertices = vertices;
        mesh.triangles = tris;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
}
