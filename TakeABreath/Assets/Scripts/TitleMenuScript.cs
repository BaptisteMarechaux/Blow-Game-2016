using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    [SerializeField]
    Toggle fullscreenToggle;

    [SerializeField]
    Button btHost;
    [SerializeField]
    Button btClient;

    [SerializeField]
    NetworkStartScript nss;


    [SerializeField]
    InputField inputFieldMatcName;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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

        if (!nss)
            //nss = GameObject.Find("NetworkManager").GetComponent<NetworkStartScript>();
            nss = NetworkStartScript.singleton;
        if (nss && btClient && btHost)
        {
            btHost.onClick.AddListener(delegate { nss.LaunchServer(); });
            btClient.onClick.AddListener(delegate { nss.LaunchClient(); });
        }
        inputFieldMatcName.onValueChanged.AddListener(ValueChangeCheck);

        if (PlayerPrefs.HasKey("VisitedLevels") == false)
        {
            PlayerPrefs.SetInt("VisitedLevels", 0);
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
	    if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.SetInt("First Play", 0);
            PlayerPrefs.SetInt("VisitedLevels", 0);
            PlayerPrefs.Save();
        }
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


    public void ValueChangeCheck(string value)
    {
        if (value == "")
            nss.matchName = "PJAnn";
        else
            nss.matchName = value;
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void StartSoloMode()
    {
        if(PlayerPrefs.GetInt("First Play") != 1)
        {
            
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
