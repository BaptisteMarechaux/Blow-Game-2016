using UnityEngine;
using System.Collections;

public class ScriptedEventStartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
