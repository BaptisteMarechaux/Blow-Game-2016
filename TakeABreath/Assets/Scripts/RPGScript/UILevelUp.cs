using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILevelUp : MonoBehaviour {

    [SerializeField]
    CharactereClass _player;
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


    public void OnEnable()
    {
        this._ptsForce = 0;
        this._ptsVie = 0;
        this._ptsVol = 0;
        this._ptsDefence = 0;
        this._ptsInt = 0;
        this._pts += this._ptsMax;
        this.UpdateUI();
        Debug.Log(this._pts);
    }


    public void AddVie()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVie++;
            this._player.Sante++;
            this._player.SanteMax++;
            this.UpdateUI();
        }
    }
    public void ReduceVie()
    {
        if (this._ptsVie > 0)
        {
            this._pts++;
            this._ptsVie--;
            this._player.SanteMax--;
            if (this._player.SanteMax > this._player.Sante)
                this._player.Sante = this._player.SanteMax;
            this.UpdateUI();
        }
    }

    public void AddForce()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsForce++;
            this._player.Force++;
            this.UpdateUI();
        }
    }
    public void ReduceForce()
    {
        if (this._ptsForce > 0)
        {
            this._pts++;
            this._ptsForce--;
            this._player.Force--;
            this.UpdateUI();
        }
    }

    public void AddDefence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsDefence++;
            this._player.Defense++;
            this.UpdateUI();
        }
    }
    public void ReduceDefence()
    {
        if (this._ptsDefence > 0)
        {
            this._pts++;
            this._ptsDefence--;
            this._player.Defense--;
            this.UpdateUI();
        }
    }

    public void AddIntelligence()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsInt++;
            this._player.Intel++;
            this.UpdateUI();
        }
    }
    public void ReduceIntelligence()
    {
        if (this._ptsInt > 0)
        {
            this._pts++;
            this._ptsInt--;
            this._player.Intel--;
            this.UpdateUI();
        }
    }

    public void AddVolonte()
    {
        if (this._pts > 0)
        {
            this._pts--;
            this._ptsVol++;
            this._player.Volonte++;
            this.UpdateUI();
        }
    }
    public void ReduceVolonte()
    {
        if (this._ptsVol > 0)
        {
            this._pts++;
            this._ptsVol--;
            this._player.Volonte--;
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
        this._mygameobject.SetActive(false);
    }

    private void UpdateUI()
    {
        this._lvlText.text = this._player.Level.ToString();
        this._points.text = this._pts.ToString();
        this._vie.text = this._player.SanteMax.ToString();
        this._force.text = this._player.Force.ToString();
        this._defense.text = this._player.Defense.ToString();
        this._intel.text = this._player.Intel.ToString();
        this._volon.text = this._player.Volonte.ToString();
    }
}
