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
        if (me.isAttacked && me.attackTarget !=null)
        {
            me.transform.LookAt(me.attackTarget.transform);
            if (me.simpleAttackRange * 2.0f < Vector3.Distance(transform.position, me.attackTarget.transform.position))
            {
                //me.transform.position +=  transform.forward * moveSpeed * Time.deltaTime;
                //me.transform.LookAt(me.attackTarget.transform);
                me.transform.rotation = Quaternion.Lerp(me.transform.rotation,  Quaternion.LookRotation((me.attackTarget.transform.position-me.transform.position).normalized), 5*Time.deltaTime);
                me.transform.position = Vector3.MoveTowards(me.transform.position, me.attackTarget.transform.position, moveSpeed * Time.deltaTime);
            }

            if (me.simpleAttackRange >= Vector3.Distance(me.transform.position, me.attackTarget.transform.position))
            {
				if (me.simpleAttackCurrentCoolDownLevel < me.simpleAttackCoolDown + 3) 
				{
				    me.simpleAttackCurrentCoolDownLevel += Time.deltaTime;
					if (me.simpleAttackCurrentCoolDownLevel >= me.simpleAttackCoolDown + 3) 
					{
                        me.isReadyToAttack = true;
						me.AttackTarget (me.attackTarget, me.monsterStr);
						me.simpleAttackCurrentCoolDownLevel = 0;
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
