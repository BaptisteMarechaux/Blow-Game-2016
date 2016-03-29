using UnityEngine;
using System.Collections;

public class SpellAttakClass : MonoBehaviour
{

    [SerializeField]
    private string _name = "Healdoken";
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int[] _requis = { 1, 0, 0, 0, 0, 0 }; //niveau, force, agi, int, cons, vol
    [SerializeField]
    private int _degat = 10;

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

    public int Degat
    {
        get
        {
            return _degat;
        }

        set
        {
            _degat = value;
        }
    }
    
}
