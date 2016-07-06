using UnityEngine;
using System.Collections;

public class FoolAIMonster : MonoBehaviour {

    [SerializeField]
    MonsterClass me;

    [SerializeField]
    float moveSpeed = 4;

    private bool estAgresse = false;
	private float _timer;

	void Start()
	{
		_timer = me.Attack.Cooldown;
	}

    public bool EstAgresse
    {
        get
        {
            return estAgresse;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (EstAgresse)
        {
            me.transform.LookAt(me._target.transform);
            if (me.Attack.Range < Vector3.Distance(this.transform.position, me._target.transform.position))
            {
                me.transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            if (me.Attack.Range >= Vector3.Distance(this.transform.position, me._target.transform.position))
            {
				if (this._timer < this.me.Attack.Cooldown + 3) 
				{
					this._timer += Time.deltaTime;
					if (this._timer >= this.me.Attack.Cooldown+ 3) 
					{
						if (me.Attack.Ready) 
						{
							me.AttackTarget (me._target, me.Force);
							_timer = 0;
						}
					}
				}
            }
        }
	}

    public void changeStat()
    {
        this.estAgresse = !EstAgresse;
    }
}
