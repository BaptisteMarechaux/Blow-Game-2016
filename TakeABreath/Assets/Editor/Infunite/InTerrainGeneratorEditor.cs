using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(InTerrainGenerator))]
public class InTerrainGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        InTerrainGenerator terrainGen = (InTerrainGenerator)target;

        if(DrawDefaultInspector()) {
            if(terrainGen.autoUpdate)
            {
                terrainGen.DrawTerrainInEditor();
            }
        }

        if(GUILayout.Button("Generate Terrain"))
        {
            terrainGen.DrawTerrainInEditor();
        }
    }

}
