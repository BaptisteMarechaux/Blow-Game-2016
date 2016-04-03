using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {


    [SerializeField]
    CharactereClass _me;
    [SerializeField]
    MonsterClass _monstrePossede;
    [SerializeField]
    private TextMesh _nameFlottant;
    [SerializeField]
    private Transform _myTransform;
    [SerializeField]
    private PossessionScript _butonPossession;

    public MonsterClass MonstrePossede
    {
        get
        {
            return _monstrePossede;
        }

        set
        {
            _monstrePossede = value;
        }
    }

    public CharactereClass Me
    {
        get
        {
            return _me;
        }

        set
        {
            _me = value;
        }
    }


    // Use this for initialization
    void Start () {
        this._nameFlottant.text = Me.Name;
    }
	
    public void essaiPossession(MonsterClass monster)
    {
        if (Me.Level >= monster.Level && monster.Player == null && MonstrePossede==null)
        {
            this.MonstrePossede = monster;
            monster.Player = Me;
            this._myTransform.position = monster.transform.position;
            this._butonPossession.Button.SetActive(false);
            this._butonPossession.enabled = false;
        }
    }
    
    public int getForce()
    {
        return this._monstrePossede.Force;
    }
}
