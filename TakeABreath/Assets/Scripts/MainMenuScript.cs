using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    GameObject helperPanel;

    [SerializeField]
    GameObject menuPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPanel)
                menuPanel.SetActive(!menuPanel.activeSelf);
            Cursor.visible = !Cursor.visible;
        }
        //print(Input.inputString);
    }
    public void HideHelperPanel()
    {
        if (helperPanel)
            helperPanel.SetActive(!helperPanel.activeSelf);
    }

    public void ReturnToMenu()
    {
         GameObject.Find("NetworkManager").GetComponent<NetworkStartScript>().Quid();
        SceneManager.LoadScene("TitleScene");
    }
}
