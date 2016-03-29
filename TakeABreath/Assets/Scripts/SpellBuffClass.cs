using UnityEngine;
using System.Collections;

public class SpellBuffClass : MonoBehaviour {

    [SerializeField]
    private string _name = "Healdoken";
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int[] _requis = { 1, 0, 0, 0, 0, 0 }; //niveau, force, agi, int, cons, vol
    [SerializeField]
    private int[] _bonus = { 0, 0, 0, 0, 0, 10}; //force, agi, int, cons, vol, vie

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
