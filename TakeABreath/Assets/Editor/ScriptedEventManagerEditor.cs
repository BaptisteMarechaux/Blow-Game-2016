using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ScriptedEventManager))]
public class ScriptedEventManagerEditor : Editor {

    /*
	override public void OnInspectorGUI()
    {
        var scriptedEventManager = target as ScriptedEventManager;

        scriptedEventManager.activated = GUILayout.Toggle(scriptedEventManager.activated, "Activate");
        if(scriptedEventManager.activated)
        {
            scriptedEventManager.theString = EditorGUILayout.TextArea(scriptedEventManager.theString);
            scriptedEventManager.i = EditorGUILayout.IntSlider(scriptedEventManager.i, 0, 100);
        }
    }
    */
}