using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EventCommandActionType
{
    translate,
    rotate,
    scale,
    write

}

public enum ConditionType
{
    position,
    rotation,
    scale,
}

public class ScriptedEventManager : MonoBehaviour {
    public ScriptedEvent[] events;

    public bool activated;
    public string theString = "Some text";
    public int i;


	// Use this for initialization
	void Start () {

	}
	
}
