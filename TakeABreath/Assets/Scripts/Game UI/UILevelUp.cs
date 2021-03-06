﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILevelUp : MonoBehaviour {

    [SerializeField]
    Text _lvlText;
    [SerializeField]
    Text _points;
    [SerializeField]
    Text _vie;
    [SerializeField]
    Text _force;
    [SerializeField]
    Text _defense;
    [SerializeField]
    Text _intel;
    [SerializeField]
    Text _volon;
    [SerializeField]
    GameObject _mygameobject;
	[SerializeField]
	Chouette_forest chouette;

	private int _ptsMax = 5;
	private int _pts = 0;
    private int _ptsVie = 0;
    private int _ptsForce = 0;
    private int _ptsDefence = 0;
    private int _ptsInt = 0;
    private int _ptsVol = 0;
    private PlayerCharacter _myPlayer;
    
    void Start()
    {
        this._myPlayer = UIManager.instance.player;
        this._ptsForce = 0;
        this._ptsVie = 0;
        this._ptsVol = 0;
        this._ptsDefence = 0;
        this._ptsInt = 0;
		_ptsMax = _myPlayer.ptsMax;
        this.UpdateUI();
    }

    public void Display()
    {
        _mygameobject.SetActive(true);
        this._ptsForce = 0;
        this._ptsVie = 0;
        this._ptsVol = 0;
        this._ptsDefence = 0;
        this._ptsInt = 0;
		this._pts += UIManager.instance.player.ptsMax;
        this.UpdateUI();
    }

    public void AddVie()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVie++;
            this.UpdateUI();
        }
    }
    public void ReduceVie()
    {
        if (this._ptsVie > 0)
        {
            this._pts++;
            this._ptsVie--;
            this.UpdateUI();
        }
    }

    public void AddForce()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsForce++;
            this.UpdateUI();
        }
    }
    public void ReduceForce()
    {
        if (this._ptsForce > 0)
        {
            this._pts++;
            this._ptsForce--;
            this.UpdateUI();
        }
    }

    public void AddDefence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsDefence++;
            this.UpdateUI();
        }
    }
    public void ReduceDefence()
    {
        if (this._ptsDefence > 0)
        {
            this._pts++;
            this._ptsDefence--;
            this.UpdateUI();
        }
    }

    public void AddIntelligence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsInt++;
            this.UpdateUI();
        }
    }
    public void ReduceIntelligence()
    {
        if (this._ptsInt > 0)
        {
            this._pts++;
            this._ptsInt--;
            this.UpdateUI();
        }
    }

    public void AddVolonte()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVol++;
            this.UpdateUI();
        }
    }
    public void ReduceVolonte()
    {
        if (this._ptsVol > 0)
        {
            this._pts++;
            this._ptsVol--;
            this.UpdateUI();
        }
    }

    public void RemiseAZero()
    {
        while(this._ptsForce > 0)
        {
            this._ptsForce--;
            this._pts++;
        }
        while (this._ptsVie > 0)
        {
            this._ptsVie--;
            this._pts++;
        }
        while (this._ptsVol > 0)
        {
            this._ptsVol--;
            this._pts++;
        }
        while (this._ptsDefence > 0)
        {
            this._ptsDefence--;
            this._pts++;
        }
        while (this._ptsInt > 0)
        {
            this._ptsInt--;
            this._pts++;
        }
        this.UpdateUI();
    }

    public void Validate()
    {
        this._myPlayer.playerHP += _ptsVie;
        this._myPlayer.playerMaxHP += _ptsVie;
        this._myPlayer.playerRes += _ptsVol;
        this._myPlayer.playerInt += _ptsInt;
        this._myPlayer.playerDef += _ptsDefence;
        this._myPlayer.playerStr += _ptsForce;
        UIManager.instance.playerManager.StatUpdateWithMonster();			
		if (_pts > 0)
			UIManager.instance.playerManager.PtsMax += _pts;
		_myPlayer.SaveStats();
		if (chouette != null && chouette.QuestId == 4) 
		{
			chouette.QuestFinish ();
		}
        this._mygameobject.SetActive(false);
    }

    private void UpdateUI()
    {
		UIManager.instance.UpdateStatusUI ();
        this._lvlText.text = this._myPlayer.playerLevel.ToString();
        this._points.text = this._pts.ToString();
        this._vie.text = (_myPlayer.playerMaxHP + _ptsVie).ToString();
        this._force.text = (_myPlayer.playerStr + _ptsForce).ToString();
        this._defense.text = (_myPlayer.playerDef + _ptsDefence).ToString();
        this._intel.text = (_myPlayer.playerInt + _ptsInt).ToString();
        this._volon.text = (_myPlayer.playerRes + _ptsVol).ToString();
    }

}
