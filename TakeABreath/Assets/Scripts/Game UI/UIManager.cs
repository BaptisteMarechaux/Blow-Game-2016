﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    [SerializeField]
    UIStatus statusUI;
    [SerializeField]
    UILevelUp levelUpCanvas;
    [SerializeField]
    GameObject menuCanvas;
    [SerializeField]
    UIQuest questUI;
    [SerializeField]
    UIMain mainUI;

    public CharactereClass playerStatus;
    public PlayerManager playerManager;

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


    #region display
    public void DisplayLevelUp(){ levelUpCanvas.Display();}

    public void DisplayAttackButton() { mainUI.ButtonAttackEnable(); }

    public void DisplayPossessButton() { mainUI.ButtonPossessEnable(); }

    public void DisplayReleaseButton() { mainUI.ButtonDepossessEnable(); }

    public void DisplayTargetStatus(MonsterClass target) { mainUI.LifeTargetEnable(target); }

    public void DisplayActiveQuest(string title, string goal) { questUI.ShowActiveQuest(title, goal); }

    public void DisplayQuest(string title, string description, string goal, string nameSave, BookQuest book) { questUI.ShowQuest(title, description, goal, nameSave, book); }

    public void HideLevelUp() { }

    public void HideAttackButton() { mainUI.ButtonAttackDisable(); }

    public void HidePossessButton() { mainUI.ButtonPossessDisable(); }

    public void HideReleaseButton() { mainUI.ButtonDepossessDisable(); }

    public void HideTargetStatus() { mainUI.LifeTargetDisable(); }

    #endregion


    #region update
    public void UpdateInfoText(string val) { mainUI.InfoTextUpdate(val); }
    public void UpdateStatusLevel() { statusUI.UpdateLevel(); }
    public void UpdateStatusExp() { statusUI.UpdateExp(); }
    #endregion
}