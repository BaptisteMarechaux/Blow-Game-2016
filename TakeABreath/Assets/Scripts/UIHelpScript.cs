using UnityEngine;
using System.Collections;

public class UIHelpScript : MonoBehaviour {

    [SerializeField]
    GameObject helperPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helperPanel)
                helperPanel.SetActive(!helperPanel.activeSelf);
        }
        //print(Input.inputString);
	}
}
