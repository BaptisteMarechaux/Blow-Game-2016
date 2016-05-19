using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    [SerializeField]
    GameObject statusCanvas;
    [SerializeField]
    UILevelUp levelUpCanvas;
    [SerializeField]
    GameObject menuCanvas;
    [SerializeField]
    GameObject questCanvas;

    [SerializeField]
    public CharactereClass playerStatus;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayLevelUp()
    {
        levelUpCanvas.Display();
    }

    public void HideAllPanels()
    {

    }
}
