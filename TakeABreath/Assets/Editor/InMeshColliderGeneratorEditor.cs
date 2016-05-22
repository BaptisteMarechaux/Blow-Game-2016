using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(InMeshColliderGenerator))]
public class InMeshColliderGeneratorEditor : Editor {

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
