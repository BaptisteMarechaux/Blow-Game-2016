using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BestiaryUI : MonoBehaviour {
    [SerializeField]
    GameObject contentNode;

    [SerializeField]
    GameObject bestiaryItemPrefab;

    [SerializeField]
    Bestiary bestiary;

    [SerializeField]
    BestiaryListItem[] bestiaryItems;

    public delegate void clickOnMonster(int index);
    public Event monsterClicked;

    [SerializeField]
    Text selectedText;
    [SerializeField]
    Text selectedSpell1;
    [SerializeField]
    Text selectedSpell2;
    [SerializeField]
    Text selectedLocation;
    [SerializeField]
    Text selectedDescription;

    
	// Use this for initialization
	void Start () {
        /*
        bestiaryItems = new BestiaryListItem[bestiary.monsters.Length];
	    for(int i=0;i<bestiary.monsters.Length;i++)
        {
            bestiaryItems[i] = Instantiate<GameObject>(bestiaryItemPrefab).GetComponent<BestiaryListItem>();
            bestiaryItems[i].transform.SetParent(contentNode.transform, true);
            bestiaryItems[i].listItemRectTransform.localPosition = new Vector3(0, 0, 0);
            bestiaryItems[i].listItemRectTransform.localScale = Vector3.one;
            bestiaryItems[i].listItemRectTransform.offsetMin = new Vector2(0, -i*100+400);
            bestiaryItems[i].listItemRectTransform.offsetMax = new Vector2(0, -i*100+500);
            bestiaryItems[i].monsterNameText.text = bestiary.monsters[i].name;
            bestiaryItems[i].monsterImage.sprite = bestiary.monsters[i].sprite;
            bestiaryItems[i].button.onClick = new Button.ButtonClickedEvent();// UIManager.instance.DisplayMonster(i);
            //bestiaryItems[i].button.onClick.AddListener(clickOnMonster);
            
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void selectMonster(int index)
    {
        selectedText.text = bestiary.monsters[index].name;
        selectedSpell1.text = bestiary.monsters[index].spells[0];
        selectedSpell2.text = bestiary.monsters[index].spells[1];
        selectedLocation.text = bestiary.monsters[index].location;
        selectedDescription.text = bestiary.monsters[index].description;

    }
}
