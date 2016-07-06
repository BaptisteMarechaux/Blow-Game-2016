using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStatsMenu : MonoBehaviour {

    [SerializeField]
    PlayerPrefManager ppm;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Text nom;
    [SerializeField]
    Text niveau;
    [SerializeField]
    Text vie;
    [SerializeField]
    Text force;
    [SerializeField]
    Text constitution;
    [SerializeField]
    Text intelligence;
    [SerializeField]
    Text volonte;

    // Use this for initialization
    void Start () {
        UpdateValue();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Display()
    {
        Debug.Log(panel);
        if(panel.activeSelf)
            panel.SetActive(false);
        else
            panel.SetActive(true);
        UpdateValue();
    }

    public void UpdateValue()
    {
        nom.text = ppm.PlayerName();
		niveau.text = ppm.GetValue("Level").ToString();
        vie.text = ppm.GetValue("VieMax").ToString();
        force.text = ppm.GetValue("Force").ToString();
        constitution.text = ppm.GetValue("Constitution").ToString();
        intelligence.text = ppm.GetValue("Intelligence").ToString();
        volonte.text = ppm.GetValue("Volonte").ToString();
    }
}
