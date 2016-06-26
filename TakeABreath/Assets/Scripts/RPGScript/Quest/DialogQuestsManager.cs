using UnityEngine;
using System.Collections;

public class DialogQuestsManager : MonoBehaviour {

    [SerializeField]
    SimpleDialogManager dialogManager;

    [SerializeField]
    GameObject panelDialog;

    //toujours faire pair = avant quêtes, impair = après 
    public SimpleDialog[] dialogQuest;

    public int idQuest = 0;

    [System.Serializable]
    public struct SimpleDialog
    {
        public string[] text;
    }

    public void Start()
    {
        //utiliser display() plutot après l'introduction
        Display();
    }


    //utiliser pour voir la première phrase des quêtes
    public void Display()
    {
        dialogManager.textList = dialogQuest[0].text;
        dialogManager.StartDialogue();
    }


    public void NextDialogue()
    {
        var ind = dialogManager.ReadNextText();
        if (ind > dialogQuest[0].text.Length)
        {
            
        }
        else
        {
            panelDialog.SetActive(false);
        }
    }
}
