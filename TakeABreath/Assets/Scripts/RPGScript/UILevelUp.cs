using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILevelUp : MonoBehaviour {

    [SerializeField]
    PlayerManager _player;
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

    private int _pts = 0;
    private int _ptsMax = 5;
    private int _ptsVie = 0;
    private int _ptsForce = 0;
    private int _ptsDefence = 0;
    private int _ptsInt = 0;
    private int _ptsVol = 0;
    private CharactereClass _myPlayer;
    
    public void OnEnable()
    {
        this._myPlayer = this._player.Me;
        this._ptsForce = 0;
        this._ptsVie = 0;
        this._ptsVol = 0;
        this._ptsDefence = 0;
        this._ptsInt = 0;
        this._pts += this._ptsMax;
        this.UpdateUI();
    }


    public void AddVie()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVie++;
            this._myPlayer.Sante++;
            this._myPlayer.SanteMax++;
            this.UpdateUI();
        }
    }
    public void ReduceVie()
    {
        if (this._ptsVie > 0)
        {
            this._pts++;
            this._ptsVie--;
            this._myPlayer.SanteMax--;
            if (this._myPlayer.SanteMax > this._myPlayer.Sante)
                this._myPlayer.Sante = this._myPlayer.SanteMax;
            this.UpdateUI();
        }
    }

    public void AddForce()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsForce++;
            this._myPlayer.Force++;
            this.UpdateUI();
        }
    }
    public void ReduceForce()
    {
        if (this._ptsForce > 0)
        {
            this._pts++;
            this._ptsForce--;
            this._myPlayer.Force--;
            this.UpdateUI();
        }
    }

    public void AddDefence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsDefence++;
            this._myPlayer.Defense++;
            this.UpdateUI();
        }
    }
    public void ReduceDefence()
    {
        if (this._ptsDefence > 0)
        {
            this._pts++;
            this._ptsDefence--;
            this._myPlayer.Defense--;
            this.UpdateUI();
        }
    }

    public void AddIntelligence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsInt++;
            this._myPlayer.Intel++;
            this.UpdateUI();
        }
    }
    public void ReduceIntelligence()
    {
        if (this._ptsInt > 0)
        {
            this._pts++;
            this._ptsInt--;
            this._myPlayer.Intel--;
            this.UpdateUI();
        }
    }

    public void AddVolonte()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVol++;
            this._myPlayer.Volonte++;
            this.UpdateUI();
        }
    }
    public void ReduceVolonte()
    {
        if (this._ptsVol > 0)
        {
            this._pts++;
            this._ptsVol--;
            this._myPlayer.Volonte--;
            this.UpdateUI();
        }
    }

    public void RemiseAZero()
    {
        while(this._ptsForce == 0)
        {
            this._ptsForce--;
            this._pts++;
        }
        while (this._ptsVie == 0)
        {
            this._ptsVie--;
            this._pts++;
        }
        while (this._ptsVol == 0)
        {
            this._ptsVol--;
            this._pts++;
        }
        while (this._ptsDefence == 0)
        {
            this._ptsDefence--;
            this._pts++;
        }
        while (this._ptsInt == 0)
        {
            this._ptsInt--;
            this._pts++;
        }
        this.UpdateUI();
    }

    public void Validate()
    {
        this._player.StatUpdateWithMonster();
        this._mygameobject.SetActive(false);
    }

    private void UpdateUI()
    {
        this._lvlText.text = this._myPlayer.Level.ToString();
        this._points.text = this._pts.ToString();
        this._vie.text = this._myPlayer.SanteMax.ToString();
        this._force.text = this._myPlayer.Force.ToString();
        this._defense.text = this._myPlayer.Defense.ToString();
        this._intel.text = this._myPlayer.Intel.ToString();
        this._volon.text = this._myPlayer.Volonte.ToString();
    }
}
