using UnityEngine;
using System.Collections;

public class ArmorClass : MonoBehaviour {

    [SerializeField]
    private string _name = "";
    [SerializeField]
    private bool _isShield = false;
    [SerializeField]
    private bool _isHelmet = false;
    [SerializeField]
    private int _defense = 1;
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int _priceToSell = 25;
    [SerializeField]
    private int[] _requis = { 1, 0, 0, 0, 0, 0 }; //niveau, force, agi, int, cons, vol
    [SerializeField]
    private int[] _bonus = { 0, 0, 0, 0, 0, 10 }; //force, agi, int, cons, vol, vie

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

    public bool IsShield
    {
        get
        {
            return _isShield;
        }

        set
        {
            _isShield = value;
        }
    }

    public bool IsHelmet
    {
        get
        {
            return _isHelmet;
        }

        set
        {
            _isHelmet = value;
        }
    }

    public int Price
    {
        get
        {
            return _price;
        }

        set
        {
            _price = value;
        }
    }

    public int PriceToSell
    {
        get
        {
            return _priceToSell;
        }

        set
        {
            _priceToSell = value;
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

    public int[] Requis
    {
        get
        {
            return _requis;
        }

        set
        {
            _requis = value;
        }
    }

    public int[] Bonus
    {
        get
        {
            return _bonus;
        }

        set
        {
            _bonus = value;
        }
    }
}
