using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScriptedEvent : MonoBehaviour {

    public ConditionType conditionTriggers;


    bool VerifyCondition()
    {
        var validated = true;
        /*for(int i=0;i<conditionTriggers.Length;i++)
        {
            if (conditionTriggers[i]  == ConditionType.position)
            {

            }
        }*/
        return validated;
    }
}
