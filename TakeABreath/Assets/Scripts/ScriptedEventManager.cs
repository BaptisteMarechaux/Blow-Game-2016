using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public struct EventCommand
{
    EventCommandObjectType objectType;
    EventCommandActionType action;

    public void ExecuteCommand()
    {

    }
}

[System.Serializable]
public struct EventCommandObject
{

}

public enum EventCommandObjectType{
    gameObject,
    camera,
    text

}

public enum EventCommandActionType
{
    translate,
    rotate,
    scale,
    write

}

public class ScriptedEventManager : MonoBehaviour {
    public EventCommand[] events;

    public bool activated;
    public string theString = "Some text";
    public int i;
	// Use this for initialization
	void Start () {
	}
	
}
