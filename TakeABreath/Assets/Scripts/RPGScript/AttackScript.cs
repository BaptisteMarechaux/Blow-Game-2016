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
    public int Attack(MonsterClass target,int force)
    {
        if(Vector3.Distance(this.transform.position, target.transform.position) <= this.Range)
        {
            if(target.Player == null)
                target.TakeDamage(force,target.Defense);
            else
                target.TakeDamage(force, target.Defense+target.Player.Defense);
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
