using UnityEngine;
using System.Collections;
using UnityEditor;

public class InTreeGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        InMeshColliderGenerator gObject = (InMeshColliderGenerator)target;

        if (DrawDefaultInspector())
        {

        }

        if (GUILayout.Button("Generate MeshCollider"))
        {
            gObject.GenerateMeshCollider();
        }
    }
}
