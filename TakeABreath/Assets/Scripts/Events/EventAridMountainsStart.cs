using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EventAridMountainsStart : MonoBehaviour {

    [SerializeField]
    SimpleDialogManager dialogManager;

    public SimpleDialog[] introTextList;

    public Image blackPanel;

    public int readDialogue = 0;

    public Graphic textGraph;
    public Graphic backTextGraph;
    public GameObject nextButton;

    public GameObject mapCamera;

	// Use this for initialization
	void Start () {
        dialogManager.textList = introTextList[0].text;
        AridMountainStart();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AridMountainStart()
    {
        dialogManager.StartDialogue();
    }

    IEnumerator Begin()
    {

        yield return null;
    }

    public void NextDialogue()
    {
        var ind = dialogManager.ReadNextText();
        if (ind+1 > introTextList[0].text.Length)
        {
            
            if(readDialogue == 0)
            {
                blackPanel.CrossFadeAlpha(0, 0.5f, false);
                readDialogue += 1;
                dialogManager.readIndex = -1;
                dialogManager.textList = introTextList[readDialogue].text;
            }
        }
        else if(ind+1 > introTextList[1].text.Length && readDialogue == 1)
        {
            backTextGraph.CrossFadeAlpha(0, 0.5f, false);
            textGraph.CrossFadeAlpha(0, 0.5f, false);
            nextButton.SetActive(false);
            //mapCamera.SetActive(true);
        }

    }
}

[System.Serializable]
public struct SimpleDialog
{
    public string[] text;
}
