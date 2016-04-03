using UnityEngine;
using System.Collections;

public class MonsterClass : MonoBehaviour {

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private int _force = 5;
    [SerializeField]
    private int _consistance = 5;
    [SerializeField]
    private int _sante = 50;
    [SerializeField]
    private CharactereClass _player;


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
}
