using UnityEngine;
using System.Collections;

public class CharactereClass : MonoBehaviour {

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private int _force = 5;
    [SerializeField]
    private int _agilite = 5;
    [SerializeField]
    private int _intelligence = 5;
    [SerializeField]
    private int _consistance = 5;
    [SerializeField]
    private int _volonte = 5;
    [SerializeField]
    private int _sante = 50;
    [SerializeField]
    private int _mana = 10;

    private int _exp = 0;
    private int _expToLvlUp = 50;


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

    public int Agilite
    {
        get
        {
            return _agilite;
        }

        set
        {
            _agilite = value;
        }
    }

    public int Intelligence
    {
        get
        {
            return _intelligence;
        }

        set
        {
            _intelligence = value;
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

    public int Mana
    {
        get
        {
            return _mana;
        }

        set
        {
            _mana = value;
        }
    }

    public int addExp(int exp)
    {
        this._exp += exp;
        if(this._exp >= this._expToLvlUp)
        {
            this._level++;
            this._exp = this._exp % this._expToLvlUp;
            this._expToLvlUp *= 2;
            return this._level;
        }
        return 0;
    }




}


