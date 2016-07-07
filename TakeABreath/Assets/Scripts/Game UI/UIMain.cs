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
    private Image _butonAttack;

    [SerializeField]
    Text MonsterPossessionBigText; //Texte qui va apparaitre au niveau central quand on possède un monstre pour bien faire comprendre qu'on possède un monstre

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
        this._butonDepossession.SetActive(false);
    }
    public void ButtonPossessDisable()
    {
        this._butonPossession.SetActive(false);
    }

    public void ButtonDepossessEnable()
    {
        this._butonDepossession.SetActive(true);
        this._butonPossession.SetActive(false);
    }
    public void ButtonDepossessDisable()
    {
        this._butonDepossession.SetActive(false);
    }

    public void ButtonAttackEnable()
    {
        this._butonAttack.gameObject.SetActive(true);
    }
    public void ButtonAttackDisable()
    {
        this._butonAttack.gameObject.SetActive(false);
    }


    public void InfoTextUpdate(string str)
    {
        this._infoText.text = str;
    }

    public void levelUpdate()
    {
        UIManager.instance.UpdateStatusLevel();
    }

    public void UpdateAttackButtonFill(float value)
    {
        _butonAttack.fillAmount = value;
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

    public void LifeTargetEnable(MonsterCharacter target)
    {
        this._healthTargetObject.gameObject.SetActive(true);
        this.healthBarTargetInfo(target);
        if (!target.possessed)
            this._targetName.text = target.monsterName;
        else
            this._targetName.text = UIManager.instance.player.playerName;
    }

    public void healthBarTargetInfo(MonsterClass target)
    {
        if (target.Player == null)
        {
            this._healthTargetText.text = target.Sante.ToString();
            float itlife = (float)target.Sante / (float)target.SanteMax; //<== valeur entre 0 et 1
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

    public void healthBarTargetInfo(MonsterCharacter target)
    {
            this._healthTargetText.text = target.monsterMaxHP.ToString();
            float itlife = (float)target.monsterHP / (float)target.monsterMaxHP; //<== valeur entre 0 et 1
            _healthTargetBar.fillAmount = Mathf.Lerp(_healthTargetBar.fillAmount, itlife, 5 * Time.deltaTime);
            //this._healthTargetBar.transform.localScale = new Vector3(Mathf.Clamp(itlife, 0f, 1f), this._healthTargetBar.transform.localScale.y, this._healthTargetBar.transform.localScale.z);
    }

    public void StartPossessAnimation()
    {
        MonsterPossessionBigText.gameObject.SetActive(true);
        // MonsterPossessionBigText.transform.localScale = Vector3.zero;
        MonsterPossessionBigText.CrossFadeAlpha(0, 0.0f, false);

        StartCoroutine("PossessAnimation");
    }

    public void StartUnPossessAnimation()
    {

    }

    IEnumerator PossessAnimation()
    {
        MonsterPossessionBigText.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(1.5f);
        MonsterPossessionBigText.CrossFadeAlpha(0, 0.5f, false);
        yield return null;
    }
    
}
