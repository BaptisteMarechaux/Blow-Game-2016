using UnityEngine;
using System.Collections;

public class PotionClass : MonoBehaviour {

    [SerializeField]
    private string _name = "Chicken";
    [SerializeField]
    private int _price = 50;
    [SerializeField]
    private int _priceToSell = 25;
    [SerializeField]
    private int[] _bonus = { 15, 0 }; //vie,mana

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
