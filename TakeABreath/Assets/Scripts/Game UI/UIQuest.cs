using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIQuest : MonoBehaviour {

    [SerializeField]
    GameObject questPannel;
    [SerializeField]
    Text questTitle;
    [SerializeField]
    Text questDescription;
    [SerializeField]
    Text questObjectif;
    [SerializeField]
    Button questButton;

    [SerializeField]
    GameObject suiviPannel;
    [SerializeField]
    Text suiviTitle;
    [SerializeField]
    Text suiviObjectif;
    
    // Use this for initialization
    void Start () {

	}
	
    public void ShowQuest(string title, string description, string objectif, string nameSave, BookQuest book)
    {
        questPannel.SetActive(true);
        questTitle.text = title;
        questDescription.text = description;
        questObjectif.text = objectif;
        questButton = questButton.GetComponent<Button>();
        questButton.onClick.AddListener(() => { book.ChageStatQuest(nameSave, 1); questPannel.SetActive(false); });
    }

    public void ShowActiveQuest(string title, string objectif)
    {
        suiviPannel.SetActive(true);
        suiviTitle.text = title;
        suiviObjectif.text = objectif;
    }
}
