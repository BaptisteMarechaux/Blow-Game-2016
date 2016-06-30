using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestiaryListItem : MonoBehaviour {
    public RectTransform listItemRectTransform;
    public Image monsterImage;
    public Text monsterNameText;
    public Button button;
    public int index;

	// Use this for initialization
	void Start () {
        //button.onClick = 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickOnMonster()
    {
        UIManager.instance.DisplayMonster(index);
    }
}
