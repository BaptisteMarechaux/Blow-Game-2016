using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quester : MonoBehaviour {

    [SerializeField]
    BookQuest book;

    [SerializeField]
    int ind = 0;

    private bool finish = false;

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
        if (ind < book.allQuests.Length)
        {
            quete = book.allQuests[ind];
        }
        
    }

    public void Finished()
    {
        finish = true;
    }
}
