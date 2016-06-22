using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour {
    [SerializeField]
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
    public string name;
    public string location;
    public Sprite sprite;
    public string description;
    public bool discovered;
    public string[] spells;
}
