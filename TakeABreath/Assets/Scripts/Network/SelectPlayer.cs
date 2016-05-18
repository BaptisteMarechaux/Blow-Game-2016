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
    [SerializeField]
    private PlayerPrefManager ppm;

    // Use this for initialization
    void Start () {
	    if(ppm.Existing("Name") && !this._delete)
        {
            //SceneManager.LoadScene("RPG UI", LoadSceneMode.Single);

        }
        else 
        {
            if (this._delete)
            {
                ppm.CleanPlayer();
            }
            _demandePseudo.SetActive(true);
        }
	}

    public void SaveName()
    {
        ppm.SetNamePlayer(this._pseudo.text);
        ppm.SetValuePlayer("Level", 1);
        ppm.SetValuePlayer("Exp", 0);
        ppm.SetValuePlayer("ExpMax", 50);
        ppm.SetValuePlayer("Vie", 10);
        ppm.SetValuePlayer("VieMax", 10);
        ppm.SetValuePlayer("Force", 1);
        ppm.SetValuePlayer("Constitution", 1);
        ppm.SetValuePlayer("Intelligence", 1);
        ppm.SetValuePlayer("Volonte", 1);
        //SceneManager.LoadScene("RPG UI", LoadSceneMode.Single);
    }
}
