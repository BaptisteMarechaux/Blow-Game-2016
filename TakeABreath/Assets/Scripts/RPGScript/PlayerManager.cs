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

    private bool inPossession = false;

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


    // Use this for initialization
    void Start()
    {
        this._nameFlottant.text = Me.Name;
    }

    void Update()
    {
        if (inPossession)
        {
            this._myhp.text = this._monstrePossede.Sante + " / " + this.MonstrePossede.SanteMax;
            if (this._monstrePossede.Sante <= 0)
            {
                this._monstrePossede = null;
                this._myhp.text = " - ";
                inPossession = false;
                this._butonPossession.Button.SetActive(true);
                this._butonPossession.enabled = true;
            }
        }
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
            this._butonPossession.Button.SetActive(false);
            this._butonPossession.enabled = false;
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
