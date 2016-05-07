using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    private float _range = 3f;
    [SerializeField]
    private float _cooldown = 3f;

    private float _timer;
    private bool _ready = true;

    public float Range
    {
        get
        {
            return _range;
        }
    }
    public float Cooldown
    {
        get
        {
            return _cooldown;
        }
    }

    public bool Ready
    {
        get
        {
            return _ready;
        }
    }
    
    void Start()
    {
        this._timer = this._cooldown;
    }

    //fonction principale
    public int Attack(MonsterClass target,int force,MonsterClass agresseur)
    {
        //si on ne dépasse pas la range
        if(Vector3.Distance(this.transform.position, target.transform.position) <= this.Range)
        {
            //on regarde si le monstre est controlé par un joueur
            if(target.Player == null && agresseur != null)
                target.TakeDamage(force,target.Defense, agresseur);
            else if(target.Player == null && agresseur == null)
                target.TakeDamage(force, target.Defense);
            else
                target.Player.TakeDamage(force);
            
            //cooldown
            this._timer = 0;
            this._ready = false;
            return 0;
        }
        else
        {
            return -1;
        }
    }

    void Update()
    {
        //si une attaque à déjà eut lieu
        if(this._timer < this._cooldown)
        {
            this._timer += Time.deltaTime;
            if(this._timer >= this.Cooldown)
            {
                this._ready = true;
            }
        }
    }

    /*
    */
}
