using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharactereClass _me;
    [SerializeField]
    private MonsterClass _monstrePossede;
    [SerializeField]
    private TextMesh _nameFlottant;
    [SerializeField]
    private UIscript _myUI;
    [SerializeField]
    private UIQuest _UIquest;
    [SerializeField]
    private GameObject _myLevelUI;
    [SerializeField]
    private LayerMask _monstreLayer;
    [SerializeField]
    private LayerMask _questerLayer;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private BookQuest _bookQuest;

    private Ray _ray;
    private RaycastHit _hit;
    private MonsterClass _target;

    //private NetworkPlayer _playerId;
    private bool inPossession = false;
    private float rate = 10;
    private int _vieTotal = 10;
    private int _vieMaxTotal = 10;
    private int _forceTotal = 1;
    private int _consTotal = 1;
    private int _intTotal = 1;
    private int _volTotal = 1;

    public delegate void monsterPossessed();
    public static event monsterPossessed OnMonsterPossessed;

    public delegate void monsterReleased();
    public static event monsterReleased OnMonsterReleased;

    public delegate void monsterMouseEnter();
    public static event monsterMouseEnter OnMonsterMouseEnter;

    public delegate void monsterMouseLeave();
    public static event monsterMouseLeave OnMonsterMouseLeave;

    public Transform playerPosition;

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

    public MonsterClass Target
    {
        get
        {
            return _target;
        }
    }

    public int VieTotal
    {
        get
        {
            return _vieTotal;
        }

        set
        {
            _vieTotal = value;
        }
    }

    public int VieMaxTotal
    {
        get
        {
            return _vieMaxTotal;
        }

        set
        {
            _vieMaxTotal = value;
        }
    }

    public int ForceTotal
    {
        get
        {
            return _forceTotal;
        }

        set
        {
            _forceTotal = value;
        }
    }

    public int ConsTotal
    {
        get
        {
            return _consTotal;
        }

        set
        {
            _consTotal = value;
        }
    }

    public int IntTotal
    {
        get
        {
            return _intTotal;
        }

        set
        {
            _intTotal = value;
        }
    }

    public int VolTotal
    {
        get
        {
            return _volTotal;
        }

        set
        {
            _volTotal = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        this._nameFlottant.text = Me.Name;
        this._myUI.levelUpdate();
        this._myUI.expBarInfo();
        this.VieTotal = Me.Sante;
        this.VieMaxTotal = Me.SanteMax;
        this.ForceTotal = Me.Force;
        this.ConsTotal = Me.Defense;
        this.IntTotal = Me.Intel;
        this.VolTotal = Me.Volonte;

        for (int i = 0; i < _bookQuest.allQuests.Length; i++)
        {
            if (_bookQuest.allQuests[i].Number == -1)
            {
                Quest q = _bookQuest.allQuests[i];
                _UIquest.SuiviActivQuest(q.Title,q.Objectif);
            }
        }
    }

    void Update()
    {

        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, this._monstreLayer))
            {
                _target = _hit.collider.GetComponent<MonsterClass>();
            }
            else if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, this._questerLayer))
            {
                Quester q = _hit.collider.GetComponent<Quester>();
                if(q.Quete != null)
                    this._UIquest.ShowQuest(q.Quete.Title, q.Quete.Description, q.Quete.Objectif, q.Quete.NameSave, _bookQuest);
            }
        }

        if (inPossession)
        {
            this._myUI.HealthBarUpdate();
            if (this._target != null)
            {
                //faire apparaitre bouton attaque et barre de vie enemy
                this._myUI.ButtonAttackEnable();
                this._myUI.LifeTargetEnable(Target);
            }
            if (this._monstrePossede.Sante <= 0)
            {
                noBody();
            }
        }
        else if (_target != null && !inPossession)
        {
            //faire apparaitre bouton possession
            this._myUI.ButtonPossessEnable();
            this._myUI.ButtonAttackDisable();
        }
        else if (_target != null && !inPossession)
        {
            this._myUI.ButtonPossessDisable();
        }


        if (MonstrePossede != null)
        {
            this.MonstrePossede.transform.position = playerPosition.position;
            this.MonstrePossede.transform.rotation = playerPosition.rotation;
        }
    }

    private void noBody()
    {
        Depossession();
    }

    public void Depossession()
    {
        //Attribuer le transform
        this._monstrePossede.transform.parent = null;

        this._monstrePossede.EnableAI();
        this._monstrePossede.Player = null;
        this._monstrePossede = null;
        this.inPossession = false;

        this.VieTotal = Me.Sante;
        this.VieMaxTotal = Me.SanteMax;
        this.ForceTotal = Me.Force;
        this.ConsTotal = Me.Defense;
        this.IntTotal = Me.Intel;
        this.VolTotal = Me.Volonte;

        //UI
        this._myUI.HealthBarDisable();
        this._myUI.ButtonDepossessDisable();

    }

    public void essaiPossession()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= 5)
        {
            if (Me.Level >= _target.Level && _target.Player == null && MonstrePossede == null)
            {
                this.MonstrePossede = this._target;
                this._target = null;

                this.MonstrePossede.Player = this;
                this.inPossession = true;
                //playerPosition.position = this.MonstrePossede.transform.position;
                this.MonstrePossede.transform.position = playerPosition.position;
                this.MonstrePossede.transform.rotation = playerPosition.rotation;
                this.MonstrePossede.transform.parent = playerPosition;
                this._me.addExp(this._monstrePossede.ExpToPossess);
                this._monstrePossede.DisableAI();

                StatUpdateWithMonster();

                //UI
                this._myUI.InfoTextUpdate("");
                this._myUI.expBarInfo();
                this._myUI.HealthBarUpdate();
                this._myUI.ButtonPossessDisable();
                this._myUI.ButtonDepossessEnable();

                this._monstrePossede.transform.parent = Me.transform;
                OnMonsterPossessed();

            }
            else if (Me.Level < this._target.Level)
            {
                this._myUI.InfoTextUpdate("Niveau du monstre trop élevé!");
            }
        }
        else
        {
            this._myUI.InfoTextUpdate("Créature trop loin.");
        }
        /*
        //ajouter la condition dans le if du dessus
        else if(monster.PlayerId != null)
        {
            this._myUI.InfoTextUpdate("Créature déjà possédée!");
        }
        */
    }

    public void StatUpdateWithMonster()
    {
        if (this._monstrePossede != null)
        {
            this.VieTotal = Me.Sante + MonstrePossede.Sante;
            this.VieMaxTotal = Me.SanteMax + MonstrePossede.SanteMax;
            this.ForceTotal = Me.Force + MonstrePossede.Force;
            this.ConsTotal = Me.Defense + MonstrePossede.Defense;
            this.IntTotal = Me.Intel + MonstrePossede.Intel;
            this.VolTotal = Me.Volonte + MonstrePossede.Volonte;
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= this._monstrePossede.Attack.Range && this._monstrePossede.Attack.Ready)
        {
            this._monstrePossede.AttackTarget(this._target,this._forceTotal);
            if (this._target.Sante <= 0)
            {
                this._myUI.levelUpdate();
                if (this._me.addExp(this._target.Exp) > 0)
                {
                    this._myLevelUI.SetActive(true);
                }
                this._target = null;
                this._myUI.ButtonAttackDisable();
                this._myUI.expBarInfo();
                this._myUI.LifeTargetDisable();
            }
        }
        else if(!this._monstrePossede.Attack.Ready)
        {
            this._myUI.InfoTextUpdate("Attaque non prête!");
        }
        else
        {
            this._myUI.InfoTextUpdate("Cible trop loin!");
        }
    }

    public void TakeDamage(int damage)
    {

        this._vieTotal -= damage - (this._consTotal / 3);

        if(this._vieTotal <= this.MonstrePossede.SanteMax)
        {
            this.MonstrePossede.TakeDamage(this.MonstrePossede.Sante - this._vieTotal);
        }
        
    }
    
}
