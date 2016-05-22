using UnityEngine;
using System.Collections;

public class InMeshColliderGenerator : MonoBehaviour {
    public MeshFilter targetMesh;
    public MeshCollider targetMeshCollider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateMeshCollider()
    {

        targetMeshCollider.sharedMesh = targetMesh.sharedMesh;
    }
}
