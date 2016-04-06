using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class TitleMenuScript : MonoBehaviour {

    [SerializeField]
    Toggle fullscreenToggle;

    [SerializeField]
    Button btHost;
    [SerializeField]
    Button btClient;

    [SerializeField]
    NetworkStartScript nss;

    // Use this for initialization
    void Start()
    {
        if (Screen.fullScreen == true)
        {
            this.fullscreenToggle.isOn = true;
        }

        this.volumeSlider.minValue = 0f;
        this.volumeSlider.maxValue = 1f;
        if (PlayerPrefs.HasKey("Volume"))
        {
            this.RetrieveVolume();
        }

        if(!nss)
            nss = GameObject.Find("NetworkManager").GetComponent<NetworkStartScript>();
        if (nss && btClient && btHost)
        {
            btHost.onClick.AddListener(delegate { nss.LaunchServer(); });
            btClient.onClick.AddListener(delegate { nss.LaunchClient(); });
        }
    }

    // Méthode de définition du mode grand écran
    public void FullScreenSet()
    {
        if (this.fullscreenToggle.isOn == true)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    // Méthode appellée pour quitter l'application
    public void Exit()
    {
        // Le jeu se ferme
        Application.Quit();
    }

    [SerializeField]
    Slider volumeSlider;

    // Méthode de réglage de volume
    public void SetVolume()
    {
        // Le volume du jeu devient la valeur du slider
        AudioListener.volume = this.volumeSlider.value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", this.volumeSlider.value);
    }

    public void RetrieveVolume()
    {
        this.volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }
}
