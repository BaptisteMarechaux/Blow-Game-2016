using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quester : MonoBehaviour {
    
    BookQuest book;

    [SerializeField]
    int ind = 0;
    [SerializeField]
    Text questTitle;
    [SerializeField]
    Text questDescription;
    [SerializeField]
    Text questObjectif;


    private Quest quete;
	// Use this for initialization
	void Start () {
        if(ind < book.sizeList)
            quete = book.allQuests[ind];
	}

    public void ShowQuest()
    {
        questTitle.text = quete.Title;
        questDescription.text = quete.Description;
        questObjectif.text = quete.Objectif;
    }
}
