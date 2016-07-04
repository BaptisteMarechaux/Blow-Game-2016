using UnityEngine;
using System.Collections;

public class SpellAttakClass : MonoBehaviour
{

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _range = 10;
    [SerializeField]
    private int _timer = 5; 
    [SerializeField]
    private int _degat;

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

    public int Range
    {
        get
        {
            return _range;
        }

        set
        {
            _range = value;
        }
    }

    public int Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
        }
    }
}
