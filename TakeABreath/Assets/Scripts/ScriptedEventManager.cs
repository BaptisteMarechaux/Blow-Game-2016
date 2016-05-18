using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

public enum ConditionType
{
    position,

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

public class ScriptedEvent : MonoBehaviour
{
    ConditionType[] conditionTriggers;


    //bool VerifyCondition()
    //{
    //    var validated = false;
    //    for(int i=0;i<conditionTriggers.Length;i++)
    //    {
    //        if()
    //    }
    //}
}
