using UnityEngine;
using System.Collections;
using System;

public class MonsterCharacter : MonoBehaviour {


    public float simpleAttackRange=1;
    public float simpleAttackPower=2;
    public float simpleAttackCurrentCoolDownLevel=1.0f;
    public float simpleAttackCoolDown=3.0f;
    public bool isReadyToAttack = true;
    public MonsterCharacter attackTarget;

    public int possessionExp; //Experience for possessing this monster
    public int battleExp; //Experience  for beating this monster
    public string monsterName;
    #region Stats
    public int monsterLevel;
    public int monsterMaxHP = 10;
    public int monsterHP = 10;
    public int monsterStr = 1; //Strength for physical based attacks
    public int monsterInt = 1; //Intelligence for Magic Based Attacks
    public int monsterRes = 1; //Resistance for Magic Based Attacks
    public int monsterDef = 1; //Defense for physical based attacks
    #endregion

    public FoolAIMonster IA;

    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    bool isAlive;

    [SerializeField]
    Collider mainCollider;
    [SerializeField]
    MeshRenderer mainMeshRenderer;

    float timer;
    float timerRespawnTime = 5.0f;
    public CapsuleCollider monsterCollider;

    public bool possessed;
    // Use this for initialization
    void Start () {
	    if(monsterCollider == null)
        {
            monsterCollider = GetComponent<CapsuleCollider>();
            if(monsterCollider ==null)
            {
                monsterCollider = new CapsuleCollider();
            }
        }

        monsterHP = monsterMaxHP;
        startPos = transform.position;
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(!isAlive)
        {
            timer += Time.deltaTime;
            if(timer>timerRespawnTime)
            {
                Respawn();
                timer = 0;
            }

            
        }
        else
        {
            if (simpleAttackCurrentCoolDownLevel < simpleAttackCoolDown)
            {
                simpleAttackCurrentCoolDownLevel += Time.deltaTime;
                if (simpleAttackCurrentCoolDownLevel >= simpleAttackCoolDown)
                    isReadyToAttack = true;
            }
        }
	}

    void Respawn()
    {
        isAlive = true;
        mainMeshRenderer.enabled = true;
        mainCollider.enabled = true;
        EnableAI();
        if (IA.EstAgresse)
            IA.changeStat();

        monsterHP = monsterMaxHP;
        transform.position = startPos;
        attackTarget = null;
    }

    public void AttackTarget(MonsterCharacter target, int strength)
    {
        target.TakeDamages(strength, this);

        //cooldown
        simpleAttackCurrentCoolDownLevel = 0;
        isReadyToAttack = false;
    }

    public void TakeDamages(int amount, MonsterCharacter attacker=null)
    {
        monsterHP -= amount - (monsterDef / 3);

        if(monsterHP <= 0)
        {
            DisableAI();
            isAlive = false;
            mainMeshRenderer.enabled = false;
            mainCollider.enabled = false;
        }

        if(attacker != null)
        {
            attackTarget = attacker;
        }
    }

    public void Possess()
    {
        attackTarget = null;
    }

    public void EnableAI()
    {
        IA.enabled = true;
    }

    public void DisableAI()
    {
        IA.enabled = false;
    }

   
}
