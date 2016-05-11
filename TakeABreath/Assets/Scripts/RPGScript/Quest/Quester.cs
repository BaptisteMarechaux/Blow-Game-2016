using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quester : MonoBehaviour {

    [SerializeField]
    BookQuest book;

    [SerializeField]
    int ind = 0;
    
    private Quest quete;

    public Quest Quete
    {
        get
        {
            return quete;
        }
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log(book.allQuests.Length);
        if (ind < book.allQuests.Length)
        {
            Debug.Log(book.allQuests[ind]);
            quete = book.allQuests[ind];
            Debug.Log(quete);
        }
    }

}
