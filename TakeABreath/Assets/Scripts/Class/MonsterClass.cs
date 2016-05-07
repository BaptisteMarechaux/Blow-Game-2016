using UnityEngine;
using System.Collections;

public class MonsterClass : MonoBehaviour {

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _type;
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private int _force = 5;
    [SerializeField]
    private int _defense = 5;
    [SerializeField]
    private int _intel = 5;
    [SerializeField]
    private int _volonte = 5;
    [SerializeField]
    private int _santeMax = 50;
    [SerializeField]
    private int _exp = 10;
    [SerializeField]
    private int _expToPossess = 5;
    [SerializeField]
    private float _timerRespawn = 10;
    [SerializeField]
    private PlayerManager _player;
    [SerializeField]
    private TextMesh _textExp;
    [SerializeField]
    private AttackScript _attack;
    [SerializeField]
    private SkinnedMeshRenderer _myMesh;
    [SerializeField]
    private Collider _myCollier;
    [SerializeField]
    private FoolAIMonster _myIA;


    //private NetworkPlayer _playerId;

    private int _sante;
    private bool _isAlive = true;
    private Vector3 _startPos = new Vector3(0,0,0);
    private float timer = 0;
    private bool _isHunted = false;
    public MonsterClass _target = null;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }
    }

    public int Force
    {
        get
        {
            return _force;
        }

        set
        {
            _force = value;
        }
    }

    public int Sante
    {
        get
        {
            return _sante;
        }

        set
        {
            _sante = value;
        }
    }

    public PlayerManager Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }

    public int Exp
    {
        get
        {
            return _exp;
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }

    public int SanteMax
    {
        get
        {
            return _santeMax;
        }
    }

    public int ExpToPossess
    {
        get
        {
            return _expToPossess;
        }
    }

    public AttackScript Attack
    {
        get
        {
            return _attack;
        }
    }

    public bool IsHunted
    {
        get
        {
            return _isHunted;
        }

        set
        {
            _isHunted = value;
        }
    }

    public int Defense
    {
        get
        {
            return _defense;
        }

        set
        {
            _defense = value;
        }
    }

    public int Intel
    {
        get
        {
            return _intel;
        }

        set
        {
            _intel = value;
        }
    }

    public int Volonte
    {
        get
        {
            return _volonte;
        }

        set
        {
            _volonte = value;
        }
    }

    public FoolAIMonster MyIA
    {
        get
        {
            return _myIA;
        }

        set
        {
            _myIA = value;
        }
    }

    void Start()
    {
        this._sante = this.SanteMax;
        this._startPos = this.transform.position;
    }

    void Update()
    {
        if(!this._isAlive)
        {
            timer += Time.deltaTime;
            if (timer >= this._timerRespawn)
            {
                Respawn();
                timer = 0;
            }
        }
    }


    public void AttackTarget(MonsterClass target, int force)
    {
        this._attack.Attack(target,force,this);
        target.IsHunted = true;
        if (!target.IsAlive)
        {
            target = null;
        }
    }

    public void TakeDamage(int damage,int def=0,MonsterClass agresseur = null)
    {
        this._sante -= damage - (def/3);

        if(this._target == null && agresseur != null)
        {
            this._target = agresseur;
            this.MyIA.changeStat();
            Debug.Log(this._target);
        }

        if (this._sante <= 0)
        {
            this._textExp.text = this._exp + " exp";
            this._isAlive = false;
            this._myMesh.enabled = false;
            this._myCollier.enabled = false;
        }
    }

    public void Respawn()
    {
        this._isAlive = true;
        this.MyIA.changeStat();
        this._sante = this._santeMax;
        this.transform.position = this._startPos;
        this._myMesh.enabled = true;
        this._myCollier.enabled = true;
        this._target = null;
    }
}
