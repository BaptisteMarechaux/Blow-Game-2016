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



    // Use this for initialization
    void Start () {
        this._nameFlottant.text = _me.Name;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void essaiPossession(MonsterClass monster)
    {
        if (_me.Level >= monster.Level && monster.Player == null)
        {
            this._monstrePossede = monster;
            monster.Player = _me;
            this._myTransform.position = monster.transform.position;

        }
    }
    

}
