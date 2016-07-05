using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMain : MonoBehaviour {

    [SerializeField]
    PlayerManager _player;
    [SerializeField]
    Text _targetName;

    [SerializeField]
    Text _infoText;

    [SerializeField]
    Text _healthTargetText;
    [SerializeField]
    Image _healthTargetBar;
    [SerializeField]
    GameObject _healthTargetObject;


    [SerializeField]
    private GameObject _butonPossession;
    [SerializeField]
    private GameObject _butonDepossession;
    [SerializeField]
    private GameObject _butonAttack;

    private float rate = 3.5f;

    void Start()
    {
        //this._playerName.text = this._player.PlayerStats.Name;
        //this._myhp.text = this._player.VieTotal + " / " + this._player.VieMaxTotal;
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
        UIManager.instance.UpdateStatusLevel();
    }

    public void expBarInfo()
    {

        float myexp = (float)this._player.PlayerStats.Exp / (float)this._player.PlayerStats.ExpToLvlUp; //<== valeur entre 0 et 1

        string information = "";
        if (myexp >= 0.3f && myexp < 0.4f)
        {
            information = "<color=black>" + this._player.PlayerStats.Exp + "</color> / " + this._player.PlayerStats.ExpToLvlUp + " EXP";
        }
        else if (myexp >= 0.4f && myexp <= 0.45f)
        {
            information = "<color=black>" + this._player.PlayerStats.Exp + " / </color>" + this._player.PlayerStats.ExpToLvlUp + " EXP";
        }
        else if (myexp > 0.45f && myexp <= 0.55f)
        {
            information = "<color=black>" + this._player.PlayerStats.Exp + " / " + this._player.PlayerStats.ExpToLvlUp + "</color> EXP";
        }
        else if (myexp > 0.55f)
        {
            information = "<color=black>" + this._player.PlayerStats.Exp + " / " + this._player.PlayerStats.ExpToLvlUp + " EXP</color>";
        }
        else if(myexp < 0.3f)
        {
            information = this._player.PlayerStats.Exp + " / " + this._player.PlayerStats.ExpToLvlUp + " EXP";
        }
       // this._expText.text = information;
    }

    public void HealthBarDisable()
    {
        /*
        this._myhp.text = this._player.VieTotal + " / " + this._player.VieMaxTotal;
        this._healthBar.transform.localScale = new Vector3(0.0f, this._healthBar.transform.localScale.y, this._healthBar.transform.localScale.z);
        this._player.transform.position = new Vector3(0, 0.4f, 0);
        */
    }


    public void LifeTargetDisable()
    {
        this._healthTargetObject.gameObject.SetActive(false);
    }
    public void LifeTargetEnable(MonsterClass target)
    {
        this._healthTargetObject.gameObject.SetActive(true);
        this.healthBarTargetInfo(target);
        if (target.Player == null)
            this._targetName.text = target.Name;
        else
            this._targetName.text = target.Player.PlayerStats.Name;
    }

    public void healthBarTargetInfo(MonsterClass target)
    {
        if (target.Player == null)
        {
            Debug.Log((float)target.Sante);
            Debug.Log((float)target.SanteMax);

            this._healthTargetText.text = target.Sante.ToString();
            float itlife = (float)target.Sante / (float)target.SanteMax; //<== valeur entre 0 et 1

            Debug.Log(itlife);

            _healthTargetBar.fillAmount = Mathf.Lerp(_healthTargetBar.fillAmount, itlife, 5 * Time.deltaTime);

            //this._healthTargetBar.transform.localScale = new Vector3(Mathf.Clamp(itlife, 0f, 1f), this._healthTargetBar.transform.localScale.y, this._healthTargetBar.transform.localScale.z);
        }
        else
        {
            var targetPlayer = this._player.Target.Player;
            this._healthTargetText.text = targetPlayer.VieTotal.ToString();
            float itlife = (float)targetPlayer.VieTotal / (float)targetPlayer.VieMaxTotal; //<== valeur entre 0 et 1
            _healthTargetBar.fillAmount = Mathf.Clamp(itlife, 0f, 1f);
            //this._healthTargetBar.transform.localScale = new Vector3(Mathf.Clamp(itlife, 0f, 1f), this._healthTargetBar.transform.localScale.y, this._healthTargetBar.transform.localScale.z);
        }
    }
    
}
