using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIscript : MonoBehaviour {

    [SerializeField]
    PlayerManager _player;
    [SerializeField]
    Text _targetName;
    [SerializeField]
    Text _infoText;
    [SerializeField]
    Text _lvlText;
    [SerializeField]
    Text _myhp;
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    Text _healthTargetText;
    [SerializeField]
    Image _healthTargetBar;
    [SerializeField]
    GameObject _healthTargetObject;
    [SerializeField]
    Text _expText;
    [SerializeField]
    Image _expBar;

    [SerializeField]
    private GameObject _butonPossession;
    [SerializeField]
    private GameObject _butonDepossession;
    [SerializeField]
    private GameObject _butonAttack;

    private float rate = 3.5f;

    void Start()
    {
        this._myhp.text = this._player.VieTotal + " / " + this._player.VieMaxTotal;
    }

    void Update()
    {
        if (this._infoText.text != "")
        {
            rate -= Time.deltaTime;
            if (rate <= 0)
            {
                this._infoText.text = " ";
                rate = 3.5f;
            }
        }
    }

    public void ButtonPossessEnable()
    {
        this._butonPossession.SetActive(true);
    }
    public void ButtonPossessDisable()
    {
        this._butonPossession.SetActive(false);
    }

    public void ButtonDepossessEnable()
    {
        this._butonDepossession.SetActive(true);
    }
    public void ButtonDepossessDisable()
    {
        this._butonDepossession.SetActive(false);
    }

    public void ButtonAttackEnable()
    {
        this._butonAttack.SetActive(true);
    }
    public void ButtonAttackDisable()
    {
        this._butonAttack.SetActive(false);
    }


    public void InfoTextUpdate(string str)
    {
        this._infoText.text = str;
    }

    public void levelUpdate()
    {
        _lvlText.text = _player.Me.Level.ToString();
    }

    public void expBarInfo()
    {
        float myexp = (float)this._player.Me.Exp / (float)this._player.Me.ExpToLvlUp; //<== valeur entre 0 et 1
        this._expBar.transform.localScale = new Vector3(Mathf.Clamp(myexp, 0f, 1f), this._expBar.transform.localScale.y, this._expBar.transform.localScale.z);
        
        string information = "";
        if (myexp >= 0.3f && myexp < 0.4f)
        {
            information = "<color=black>" + this._player.Me.Exp + "</color> / " + this._player.Me.ExpToLvlUp + " EXP";
        }
        else if (myexp >= 0.4f && myexp <= 0.45f)
        {
            information = "<color=black>" + this._player.Me.Exp + " / </color>" + this._player.Me.ExpToLvlUp + " EXP";
        }
        else if (myexp > 0.45f && myexp <= 0.55f)
        {
            information = "<color=black>" + this._player.Me.Exp + " / " + this._player.Me.ExpToLvlUp + "</color> EXP";
        }
        else if (myexp > 0.55f)
        {
            information = "<color=black>" + this._player.Me.Exp + " / " + this._player.Me.ExpToLvlUp + " EXP</color>";
        }
        else if(myexp < 0.3f)
        {
            information = this._player.Me.Exp + " / " + this._player.Me.ExpToLvlUp + " EXP";
        }
        this._expText.text = information;
    }

    public void HealthBarDisable()
    {
        this._myhp.text = this._player.VieTotal + " / " + this._player.VieMaxTotal;
        this._healthBar.transform.localScale = new Vector3(0.0f, this._healthBar.transform.localScale.y, this._healthBar.transform.localScale.z);
        this._player.transform.position = new Vector3(0, 0.4f, 0);
    }

    public void HealthBarUpdate()
    {
        this._myhp.text = this._player.VieTotal + " / " + this._player.VieMaxTotal;
        float mylife = (float)this._player.VieTotal / (float)this._player.VieMaxTotal; //<== valeur entre 0 et 1
        this._healthBar.transform.localScale = new Vector3(Mathf.Clamp(mylife, 0f, 1f), this._healthBar.transform.localScale.y, this._healthBar.transform.localScale.z);
    }

    public void LifeTargetDisable()
    {
        this._healthTargetObject.gameObject.SetActive(false);
    }
    public void LifeTargetEnable(MonsterClass target)
    {
        this._healthTargetObject.gameObject.SetActive(true);
        this.healthBarTargetInfo();
        if (target.Player == null)
            this._targetName.text = target.Name;
        else
            this._targetName.text = target.Player.Name;
    }

    public void healthBarTargetInfo()
    {
        this._healthTargetText.text = this._player.Target.Sante.ToString();
        float itlife = (float)this._player.Target.Sante / (float)this._player.Target.SanteMax; //<== valeur entre 0 et 1
        this._healthTargetBar.transform.localScale = new Vector3(Mathf.Clamp(itlife, 0f, 1f), this._healthTargetBar.transform.localScale.y, this._healthTargetBar.transform.localScale.z);
    }



}
