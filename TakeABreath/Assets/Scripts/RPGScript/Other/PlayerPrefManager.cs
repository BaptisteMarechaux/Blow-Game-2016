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
    { PlayerPrefs.SetInt("BookQuest", 1); }

    public void CreateQuest(string name)
    {
        PlayerPrefs.SetInt(name, -1);
    }

    public void UpdateQuest(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public bool Existing(string name)
    {
        return PlayerPrefs.HasKey(name);
    }


    public void Save()
    {
        PlayerPrefs.Save();
    }


    public void CleanBookQuest()
    {
        PlayerPrefs.DeleteKey("Quest-Tuez5Golem");
        PlayerPrefs.DeleteKey("Quest-Tuez5Golem-Avancement");
        PlayerPrefs.DeleteKey("Quest-PossessionGolem");
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
    }
}