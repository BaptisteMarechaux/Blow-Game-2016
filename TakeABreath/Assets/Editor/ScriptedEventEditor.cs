using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public enum EventConditionType
{
    position = 0,
    rotation = 1
}

[CustomEditor(typeof(ScriptedEvent))]
public class ScriptedEventEditor : Editor {
    public EventConditionType evCon;
    GUIContent evConLabel = new GUIContent("Condition Type", "A type of condition to trigger an Event");
    /*override public void OnInspectorGUI()
    {
        var scriptedEvent = target as ScriptedEvent;

        //scriptedEvent.conditionTriggers = EditorGUILayout.EnumPopup(new GUIContent("Condition Type", "A type of condition to trigger an Event"), evCon);
        evCon = (EventConditionType)EditorGUILayout.EnumPopup(evConLabel, evCon);
       
    }*/

}
