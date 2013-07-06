using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (MeshCollider))]
public class PlatformGenerator : MonoBehaviour {
	static float depth = 5;
	//Parameters
	//Dimensions: These are in real world space
	//Length
	public float mLength;
	//Height
	public float mHeight;
	
	
	//Smoothness
	// 1 is for super smooth
	// 0 is for super jagged
	public float mSmoothness;
	//Variation
	// this is the height variation in the 
	public float mVariation;
	//frequency
	public float mFrequency;
	
	public void GeneratePlatform() {
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
		
		float planes = Mathf.Round(mLength);
		int segments = (int)planes - 1;
		Vector3[] vertices = new Vector3[6*segments];
		Vector2[] uvs = new Vector2[6*segments];
		int[] tris = new int[3*vertices.Length];

		float h1,h2;
		h1 = PlatformHeightGenerator.Instance().GetHeight(Terrain.Ground,transform.position.x);
		h2 = PlatformHeightGenerator.Instance().GetHeight(Terrain.Ground,transform.position.x+1);
		//for each slice
		for(int i = 0; i < segments; i++) {
			vertices[i*6]   = new Vector3(i,   0,  -depth); //front bottom	0
			vertices[1+i*6] = new Vector3(i,   h1, depth);  //back top		1
			vertices[2+i*6] = new Vector3(i,   h1, -depth); //front top		2
			vertices[3+i*6] = new Vector3(i+1, 0,  -depth); //front bottom	3
			vertices[4+i*6] = new Vector3(i+1, h2, depth);  //back top		4
			vertices[5+i*6] = new Vector3(i+1, h2, -depth); //front top		5
			
			uvs[i*6]   = new Vector2(((Vector3)vertices[i*6]).x,     	0);  //front bottom	0
			uvs[1+i*6] = new Vector2(((Vector3)vertices[1+i*6]).x,     ((Vector3)vertices[1+i*6]).y); //back top		1
			uvs[2+i*6] = new Vector2(((Vector3)vertices[2+i*6]).x,     ((Vector3)vertices[2+i*6]).y); //front top		2
			uvs[3+i*6] = new Vector2(((Vector3)vertices[3+i*6]).x, 0);  //front bottom	3
			uvs[4+i*6] = new Vector2(((Vector3)vertices[4+i*6]).x, ((Vector3)vertices[4+i*6]).y); //back top		4
			uvs[5+i*6] = new Vector2(((Vector3)vertices[5+i*6]).x, ((Vector3)vertices[5+i*6]).y); //front top		5
			
			tris[i*12]		= 0+i*6;
			tris[i*12+1] 	= 2+i*6;
			tris[i*12+2] 	= 3+i*6;
			tris[i*12+3] 	= 2+i*6;
			tris[i*12+4] 	= 5+i*6;
			tris[i*12+5] 	= 3+i*6;
			tris[i*12+6] 	= 2+i*6;
			tris[i*12+7] 	= 1+i*6;
			tris[i*12+8] 	= 5+i*6;
			tris[i*12+9] 	= 1+i*6;
			tris[i*12+10] 	= 4+i*6;
			tris[i*12+11] 	= 5+i*6;
			
			h1 = h2;
			h2 = PlatformHeightGenerator.Instance().GetHeight(Terrain.Ground,transform.position.x+i+2);
		}
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = tris;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
