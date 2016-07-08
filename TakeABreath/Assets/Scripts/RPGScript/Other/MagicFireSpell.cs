using UnityEngine;
using System.Collections;

public class MagicFireSpell : Spell {
    [SerializeField]
    GameObject chargingParticles;
    GameObject chargingParticlesInstance;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float chargingTime = 0.5f;
    [SerializeField]
    float attackDistance = 5.0f;
    [SerializeField]
    float attackSpeed = 10.0f;

    bool currentlyAttacking = false;

    public Collider spellCollider;
	// Use this for initialization
	void Start () {

	}

    public override void Initialize(GameObject _owner)
    {
        chargingParticlesInstance = Instantiate<GameObject>(chargingParticles);
        attackObject.SetActive(false);
        chargingParticles.SetActive(false);
        spellCollider = GetComponent<Collider>();
        owner = _owner;
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if(!currentlyAttacking)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                currentlyAttacking = true;
                StartCoroutine("StartAttack");
            }
        }

    */
	    
	}

    public override void LaunchSpell()
    {
        base.LaunchSpell();
        if (!currentlyAttacking)
        {
            chargingParticlesInstance.transform.parent = owner.transform;
            currentlyAttacking = true;
            StartCoroutine("StartAttack");
        }
    }

    IEnumerator StartAttack()
    {
        chargingParticlesInstance.transform.position = owner.transform.position;
        transform.position = owner.transform.position;
        chargingParticlesInstance.SetActive(true);
        yield return new WaitForSeconds(chargingTime);
        chargingParticlesInstance.SetActive(false);
        
        spellCollider.enabled = true;
        yield return new WaitForEndOfFrame();
        attackObject.SetActive(true);
        for (float i=0;i<5;i+=20*Time.deltaTime)
        {
            transform.Translate(owner.transform.forward * 20 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.0f);
        spellCollider.enabled = false;
        currentlyAttacking = false;
        attackObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

}
