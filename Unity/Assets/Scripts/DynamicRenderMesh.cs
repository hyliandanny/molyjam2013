using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshRenderer))]
public class DynamicRenderMesh : DynamicMesh {
	static float borderHeight = 0.5f;
	new public void Generate(Vector2[] curve) {
		int segments = curve.Length-1;
		Vector3[] vertices = new Vector3[4*segments];
		Vector2[] uvs = new Vector2[4*segments];
		int[] tris = new int[3*vertices.Length];
 
		for(int i = 0; i < segments; i++) {
			if(type == DynamicMeshType.Foreground) {
				vertices[i*4]   = new Vector3(((Vector2)curve[i]).x,   -30,  -depth);
				vertices[1+i*4] = new Vector3(((Vector2)curve[i]).x,   ((Vector2)curve[i]).y, -depth);
				vertices[2+i*4] = new Vector3(((Vector2)curve[i+1]).x, -30,  -depth);
				vertices[3+i*4] = new Vector3(((Vector2)curve[i+1]).x, ((Vector2)curve[i+1]).y, -depth);
			}
			else if(type == DynamicMeshType.Border) {
				vertices[i*4]   = new Vector3(((Vector2)curve[i]).x,   ((Vector2)curve[i]).y-borderHeight,  -depth-1);
				vertices[1+i*4] = new Vector3(((Vector2)curve[i]).x,   ((Vector2)curve[i]).y,				-depth-1);
				vertices[2+i*4] = new Vector3(((Vector2)curve[i+1]).x, ((Vector2)curve[i+1]).y-borderHeight,-depth-1);
				vertices[3+i*4] = new Vector3(((Vector2)curve[i+1]).x, ((Vector2)curve[i+1]).y, 			-depth-1);
			}
			
			uvs[i*4]   = new Vector2(((Vector2)curve[i]).x,   0);
			uvs[1+i*4] = new Vector2(((Vector2)curve[i]).x,   1);
			uvs[2+i*4] = new Vector2(((Vector2)curve[i+1]).x, 0);
			uvs[3+i*4] = new Vector2(((Vector2)curve[i+1]).x, 1);
			
			tris[i*6]		= 0+i*4;
			tris[i*6+1] 	= 1+i*4;
			tris[i*6+2] 	= 2+i*4;
			tris[i*6+3] 	= 2+i*4;
			tris[i*6+4] 	= 1+i*4;
			tris[i*6+5] 	= 3+i*4;
		}
		
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		if(!mf.sharedMesh) {
			mf.sharedMesh = new Mesh();
		}
		mf.sharedMesh.Clear();
		
		mf.sharedMesh.vertices = vertices;
		mf.sharedMesh.uv = uvs;
		mf.sharedMesh.triangles = tris;
        mf.sharedMesh.RecalculateNormals();
		mf.sharedMesh.RecalculateBounds();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
