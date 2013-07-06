using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
public class DynamicMesh : MonoBehaviour {
	protected static float depth = 5;
	public DynamicMeshType type;
	virtual public void Generate(Vector2[] curve) {
	}
}