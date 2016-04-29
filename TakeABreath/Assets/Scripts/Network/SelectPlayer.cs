using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectPlayer : MonoBehaviour {

    [SerializeField]
    GameObject _demandePseudo;
    [SerializeField]
    Text _pseudo;
    [SerializeField]
    bool _delete;

    // Use this for initialization
    void Start () {
	    if(PlayerPrefs.HasKey("Name") && !this._delete)
        {
            SceneManager.LoadScene("RPG UI", LoadSceneMode.Single);
        }
        else 
        {
            if (this._delete)
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
            _demandePseudo.SetActive(true);
        }
	}

    public void SaveName()
    {
        PlayerPrefs.SetString("Name", this._pseudo.text);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("ExpMax", 50);
        PlayerPrefs.SetInt("Vie", 10);
        PlayerPrefs.SetInt("VieMax", 10);
        PlayerPrefs.SetInt("Force", 1);
        PlayerPrefs.SetInt("Constitution", 1);
        PlayerPrefs.SetInt("Intelligence", 1);
        PlayerPrefs.SetInt("Volonte", 1);
        SceneManager.LoadScene("RPG UI", LoadSceneMode.Single);
    }
}
