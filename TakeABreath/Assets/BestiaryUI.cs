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
    
	// Use this for initialization
	void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void selectMonster(int index)
    {

    }
}
