using UnityEngine;
using System.Collections;

public class PlayerPrefManager : MonoBehaviour {

    [SerializeField]
    private bool eraseAllSave;

    // Use this for initialization
    void Start () {
        if (eraseAllSave)
            PlayerPrefs.DeleteAll();
		Debug.Log (GetValue ("level"));
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
