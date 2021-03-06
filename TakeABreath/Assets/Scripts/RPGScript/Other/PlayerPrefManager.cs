﻿using UnityEngine;
using System.Collections;

public class PlayerPrefManager : MonoBehaviour {

    [SerializeField]
    private bool eraseAllSave;

    // Use this for initialization
    void Start () {
        if (eraseAllSave)
            PlayerPrefs.DeleteAll();
	}
	
    //PLAYER
    public void SetNamePlayer(string name)
    {
        PlayerPrefs.SetString("Name",name);
    }
    public void SetValuePlayer(string name, int nb)
    {
        PlayerPrefs.SetInt(name,nb);
    }
	public void DebugLogPlayer()
	{
		Debug.Log (GetValue ("Name"));
		Debug.Log (GetValue ("Level"));
		Debug.Log (GetValue ("Exp"));
		Debug.Log (GetValue ("ExpMax"));
		Debug.Log (GetValue ("Vie"));
		Debug.Log (GetValue ("VieMax"));
		Debug.Log (GetValue ("Force"));
		Debug.Log (GetValue ("Constitution"));
		Debug.Log (GetValue ("Intelligence"));
		Debug.Log (GetValue ("Volonte"));
		Debug.Log (GetValue ("PtsMax"));
		Debug.Log (GetValue ("level"));
	}

    public string PlayerName()
    {
        return PlayerPrefs.GetString("Name");
    }
    public int GetValue(string name)
    {
        return PlayerPrefs.GetInt(name);
    }


    //QUEST
    public void CreateDiarieQuest()
    {
        PlayerPrefs.SetInt("Quest", 0);
    }


    public void UpdateQuest(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public bool Existing(string name)
    {
        return PlayerPrefs.HasKey(name);
    }
    
    //SAUVEGARDE....
    public void Save()
    {
        PlayerPrefs.Save();
    }

    //CLEANER
    public void CleanBookQuest()
    {
        PlayerPrefs.DeleteKey("Quest-Tuez5Golem");
        PlayerPrefs.DeleteKey("Quest-Tuez5Golem-Avancement");
        PlayerPrefs.DeleteKey("Quest-PossessionGolem");

        PlayerPrefs.DeleteKey("Quest");

    }

    public void CleanPlayer()
    {
        PlayerPrefs.DeleteKey("Name");
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Exp");
        PlayerPrefs.DeleteKey("ExpMax");
        PlayerPrefs.DeleteKey("Vie");
        PlayerPrefs.DeleteKey("VieMax");
        PlayerPrefs.DeleteKey("Force");
        PlayerPrefs.DeleteKey("Constitution");
        PlayerPrefs.DeleteKey("Intelligence");
		PlayerPrefs.DeleteKey("Volonte");
		PlayerPrefs.DeleteKey("PtsMax");
    }
}
