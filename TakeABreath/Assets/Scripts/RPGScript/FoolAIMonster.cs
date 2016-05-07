using UnityEngine;
using System.Collections;

public class FoolAIMonster : MonoBehaviour {

    [SerializeField]
    MonsterClass me;

    [SerializeField]
    float moveSpeed = 4;

    private bool estAgresse = false;
    // Update is called once per frame
	void FixedUpdate ()
    {
        if (estAgresse)
        {
            me.transform.LookAt(me._target.transform);
            if (me.Attack.Range < Vector3.Distance(this.transform.position, me._target.transform.position))
            {
                me.transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            if (me.Attack.Range >= Vector3.Distance(this.transform.position, me._target.transform.position))
            {
                if (me.Attack.Ready)
                    me.AttackTarget(me._target, me.Force);
            }
        }
	}

    public void changeStat()
    {
        estAgresse = !estAgresse;
    }
}
