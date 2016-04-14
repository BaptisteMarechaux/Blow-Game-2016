using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharactereClass _me;
    [SerializeField]
    private MonsterClass _monstrePossede;
    [SerializeField]
    private TextMesh _nameFlottant;
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private PossessionScript _butonPossession;


    //private NetworkPlayer _playerId;

    [SerializeField]
    Text _infoText;
    [SerializeField]
    Text _myhp;
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    Image _expBar;
    [SerializeField]
    Text _expText;


    private bool inPossession = false;
    private float rate = 10;

    public MonsterClass MonstrePossede
    {
        get
        {
            return _monstrePossede;
        }

        set
        {
            _monstrePossede = value;
        }
    }

    public CharactereClass Me
    {
        get
        {
            return _me;
        }

        set
        {
            _me = value;
        }
    }

    public Text InfoText
    {
        get
        {
            return _infoText;
        }

        set
        {
            _infoText = value;
        }
    }

    public PossessionScript ButonPossession
    {
        get
        {
            return _butonPossession;
        }
    }

    // Use this for initialization
    void Start()
    {
        this._nameFlottant.text = Me.Name;
        this._expText.text = this._me.Exp + " / " + this._me.ExpToLvlUp + " EXP";
    }

    void Update()
    {
        expBarInfo();
        if (inPossession)
        {
            healthBarInfo();
            if (this._monstrePossede.Sante <= 0)
            {
                noBody();
            }
        }
        if(this.InfoText.text != "")
        {
            rate -= Time.deltaTime;
            if (rate <= 0)
            {
                this.InfoText.text = " ";
                rate = 10;
            }
        }
    }


    private void expBarInfo()
    {
        float myexp = (float)this._me.Exp / (float)this._me.ExpToLvlUp; //<== valeur entre 0 et 1
        this._expBar.transform.localScale = new Vector3(Mathf.Clamp(myexp, 0f, 1f), this._expBar.transform.localScale.y, this._expBar.transform.localScale.z);

        string information = this._me.Exp + " / " + this._me.ExpToLvlUp + " EXP";
        if (myexp >= 0.35f && myexp < 0.4f)
        {
            information = "<color=black>"+this._me.Exp + "</color> / " + this._me.ExpToLvlUp + " EXP";
        }
        else if(myexp > 0.41f && myexp <= .45f)
        {
            information = "<color=black>" + this._me.Exp + " / </color>" + this._me.ExpToLvlUp + " EXP";
        }
        else if (myexp > 0.45f && myexp <= .55f)
        {
            information = "<color=black>" + this._me.Exp + " / " + this._me.ExpToLvlUp + "</color> EXP";
        }
        else if (myexp > .55f)
        {
            this._expText.text = "<color=black>"+information+"</color>";
        }
        this._expText.text = information;
    }

    private void healthBarInfo()
    {
        this._myhp.text = this._monstrePossede.Sante + " / " + this.MonstrePossede.SanteMax;
        float mylife = (float)this._monstrePossede.Sante / (float)this.MonstrePossede.SanteMax; //<== valeur entre 0 et 1
        this._healthBar.transform.localScale = new Vector3(Mathf.Clamp(mylife,0f,1f), this._healthBar.transform.localScale.y, this._healthBar.transform.localScale.z) ;
    }

    private void noBody()
    {
        this._monstrePossede = null;
        this._myhp.text = " - ";
        inPossession = false;
        this.ButonPossession.Button.SetActive(true);
        this.ButonPossession.enabled = true;

    }

    public void essaiPossession(MonsterClass monster)
    {
        Debug.Log(Vector3.Distance(this.transform.position, monster.transform.position));
        if (Me.Level >= monster.Level && monster.Player == null && MonstrePossede == null)
        {
            this.InfoText.text = "";
            this.MonstrePossede = monster;
            monster.Player = Me;
            inPossession = true;
            this._myhp.text = this._monstrePossede.Sante + " / " + this.MonstrePossede.SanteMax;
            this._myTransform.position = monster.transform.position;
            this.ButonPossession.Button.SetActive(false);
            this.ButonPossession.enabled = false;
            this._me.addExp(this._monstrePossede.ExpToPossess);
        }
        else if (Me.Level < monster.Level)
        {
            //mettre un timer pour ce texte
            this.InfoText.text = "Niveau du monstre trop élevé!";
        }
        /*
        //ajouter la condition dans le if du dessus
        else if(monster.PlayerId != null)
        {
            this._infoText.text = "Créature déjà possédée!";
        }
        */
    }

    public int getForce()
    {
        return this._monstrePossede.Force;
    }
}
