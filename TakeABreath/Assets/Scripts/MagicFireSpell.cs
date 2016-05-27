using UnityEngine;
using System.Collections;

public class MagicFireSpell : MonoBehaviour {
    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    GameObject attackObject;
    [SerializeField]
    GameObject chargingParticles;

    [SerializeField]
    float chargingTime = 0.5f;
    [SerializeField]
    float attackDistance = 5.0f;
    [SerializeField]
    float attackSpeed = 10.0f;

    bool currentlyAttacking = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!currentlyAttacking)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                currentlyAttacking = true;
                StartCoroutine("StartAttack");
            }
        }
	    
	}

    IEnumerator StartAttack()
    {
        chargingParticles.transform.position = playerManager.transform.position;
        attackObject.transform.position = playerManager.transform.position;
        chargingParticles.SetActive(true);
        yield return new WaitForSeconds(chargingTime);
        chargingParticles.SetActive(false);
        attackObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        Debug.Log("moving");
        for(float i=0;i<5;i+=20*Time.deltaTime)
        {
            attackObject.transform.Translate(playerManager.transform.forward * 20 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.0f);
        attackObject.SetActive(false);
        currentlyAttacking = false;
        yield return new WaitForEndOfFrame();
    }

}
