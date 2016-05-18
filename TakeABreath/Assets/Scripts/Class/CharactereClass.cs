using UnityEngine;
using System.Collections;


public class CharactereClass : MonoBehaviour {

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private int _exp = 0;
    [SerializeField]
    private int _expToLvlUp = 50;
    [SerializeField]
    private int _sante = 10;
    [SerializeField]
    private int _force = 1;
    [SerializeField]
    private int _intel = 1; //up les sorts
    [SerializeField]
    private int _volonte = 1; //diminue dégats des sorts
    [SerializeField]
    private int _defense = 1;
    [SerializeField]
    private int _santeMax = 10;
    [SerializeField]
    private PlayerPrefManager ppm;

    //private NetworkPlayer _playerId;

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

    public int Exp
    {
        get
        {
            return _exp;
        }
    }

    public int ExpToLvlUp
    {
        get
        {
            return _expToLvlUp;
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

    void OnEnable()
    {
        this._name = ppm.PlayerName();
        this._level = ppm.GetValue("Level");
        this._sante = ppm.GetValue("Vie");
        this._santeMax = ppm.GetValue("VieMax");
        this._exp = ppm.GetValue("Exp");
        this._expToLvlUp = ppm.GetValue("ExpMax");
        this._force = ppm.GetValue("Force");
        this._defense = ppm.GetValue("Constitution");
        this._intel = ppm.GetValue("Intelligence");
        this._volonte = ppm.GetValue("Volonte");
    }

    public int addExp(int exp)
    {
        this._exp += exp;
        if(this.Exp >= this.ExpToLvlUp)
        {
            levelUp();
            return this._level;
        }
        if (ppm.PlayerName() == this._name)
        {
            SaveStats();
        }
        return 0;
    }

    private void levelUp()
    {
        //faire apparaitre à l'écran une fenêtre pour choisir les stats à up (5-7 points dispo pour up)
        this._level++;
        this._exp = this.Exp % this.ExpToLvlUp;
        this._expToLvlUp *= 2;
        if (this.Exp >= this.ExpToLvlUp)
            levelUp();
        else
        {
            if(ppm.PlayerName() == this._name)
            {
                SaveStats();
            }
        }
    }

    private void SaveStats()
    {
        ppm.SetValuePlayer("Level", this._level);
        ppm.SetValuePlayer("Exp", this._exp);
        ppm.SetValuePlayer("ExpMax", this._expToLvlUp);
        ppm.SetValuePlayer("Vie", this._sante);
        ppm.SetValuePlayer("VieMax", this._santeMax);
        ppm.SetValuePlayer("Force", this._force);
        ppm.SetValuePlayer("Constitution", this._defense);
        ppm.SetValuePlayer("Intelligence", this._intel);
        ppm.SetValuePlayer("Volonte", this._volonte);
    }
}


