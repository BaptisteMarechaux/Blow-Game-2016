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
    private int _consistance = 5;
    [SerializeField]
    private int _santeMax = 50;
    [SerializeField]
    private int _exp = 10;
    [SerializeField]
    private int _expToPossess = 5;
    [SerializeField]
    private CharactereClass _player;
    [SerializeField]
    private TextMesh _textExp;


    //private NetworkPlayer _playerId;


    private int _sante;
    private bool _isAlive = true;

    void Start()
    {
        this._sante = this.SanteMax;
    }

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;
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

    public int Consistance
    {
        get
        {
            return _consistance;
        }

        set
        {
            _consistance = value;
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

        set
        {
            _exp = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }

        set
        {
            _isAlive = value;
        }
    }

    public int SanteMax
    {
        get
        {
            return _santeMax;
        }

        set
        {
            _santeMax = value;
        }
    }

    public int ExpToPossess
    {
        get
        {
            return _expToPossess;
        }

        set
        {
            _expToPossess = value;
        }
    }

    public void TakeDamage(int damage)
    {
        this._sante -= damage;
        Debug.Log(this._sante);
        if (this._sante <= 0)
        {
            this._textExp.text = this._exp + " exp";
            this._isAlive = false;
            this.gameObject.SetActive(false);
        }
    }

}
