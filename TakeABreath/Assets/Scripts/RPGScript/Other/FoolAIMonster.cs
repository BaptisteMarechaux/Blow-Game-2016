using UnityEngine;
using System.Collections;

public class FoolAIMonster : MonoBehaviour {

    [SerializeField]
    MonsterCharacter me;

    [SerializeField]
    float moveSpeed = 4;

    private bool estAgresse = false;
	private float _timer;

	void Start()
	{
		_timer = me.simpleAttackCoolDown;
	}

    public bool EstAgresse
    {
        get
        {
            return me.isAttacked;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (me.isAttacked)
        {
            me.transform.LookAt(me.attackTarget.transform);
            if (me.simpleAttackRange < Vector3.Distance(transform.position, me.attackTarget.transform.position))
            {
                me.transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            if (me.simpleAttackRange >= Vector3.Distance(this.transform.position, me.attackTarget.transform.position))
            {
				if (this._timer < this.me.simpleAttackCoolDown + 3) 
				{
					this._timer += Time.deltaTime;
					if (this._timer >= this.me.simpleAttackCoolDown + 3) 
					{
						if (me.isReadyToAttack) 
						{
							me.AttackTarget (me.attackTarget, me.monsterStr);
							_timer = 0;
						}
					}
				}
            }
        }
	}

    public void changeStat()
    {
        me.isAttacked = !EstAgresse;
    }
}
