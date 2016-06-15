using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour {
    BestiaryMonster[] monsters;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {

    }
}

[System.Serializable]
public struct BestiaryMonster
{
    string name;
    bool discovered;
    string[] spells;
}
