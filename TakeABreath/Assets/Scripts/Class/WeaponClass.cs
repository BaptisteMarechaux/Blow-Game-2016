using UnityEngine;
using System.Collections;

public class WeaponClass : MonoBehaviour {

    [SerializeField]
    private string _name = "wooden sword";
    [SerializeField]
    private bool _oneHand = true;
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int _priceToSell = 25;
    [SerializeField]
    private int _precision = 3;
    [SerializeField]
    private int _attakMin = 1;
    [SerializeField]
    private int _attakMax = 3;
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

    public bool OneHand
    {
        get
        {
            return _oneHand;
        }

        set
        {
            _oneHand = value;
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

    public int Precision
    {
        get
        {
            return _precision;
        }

        set
        {
            _precision = value;
        }
    }
    
    public int AttakMin
    {
        get
        {
            return _attakMin;
        }

        set
        {
            _attakMin = value;
        }
    }

    public int AttakMax
    {
        get
        {
            return _attakMax;
        }

        set
        {
            _attakMax = value;
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
