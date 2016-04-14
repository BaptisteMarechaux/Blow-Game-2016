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

    public int addExp(int exp)
    {
        this._exp += exp;
        if(this.Exp >= this.ExpToLvlUp)
        {
            levelUp();
            return this._level;
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
    }
}


