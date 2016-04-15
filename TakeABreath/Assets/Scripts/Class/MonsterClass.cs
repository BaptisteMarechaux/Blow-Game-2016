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
    private int _santeMax = 50;
    [SerializeField]
    private int _exp = 10;
    [SerializeField]
    private int _expToPossess = 5;
    [SerializeField]
    private float _timerRespawn = 10;
    [SerializeField]
    private CharactereClass _player;
    [SerializeField]
    private TextMesh _textExp;
    [SerializeField]
    private AttackScript _attack;
    [SerializeField]
    private MeshRenderer _myMesh;
    [SerializeField]
    private Collider _myCollier;


    //private NetworkPlayer _playerId;

    private int _sante;
    private bool _isAlive = true;
    private Vector3 _startPos = new Vector3(0,0,0);
    private float timer = 0;


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

    public CharactereClass Player
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


    public void AttackTarget(MonsterClass target)
    {
        this._attack.Attack(target,this._force);
        if (!target.IsAlive)
        {
            target = null;
        }
    }

    public void TakeDamage(int damage)
    {
        this._sante -= damage;

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
        this._sante = this._santeMax;
        this.transform.position = this._startPos;
        this._myMesh.enabled = true;
        this._myCollier.enabled = true;
    }
}
