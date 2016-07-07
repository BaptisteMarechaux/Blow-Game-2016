using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField]
    PlayerPrefManager ppm;

    [SerializeField]
    public MonsterCharacter possessedMonster;
    [SerializeField]
    private TextMesh floatingName;
    [SerializeField]
    private LayerMask monsterLayerMask;
    [SerializeField]
    private LayerMask questerLayerMask;
    [SerializeField]
    public Camera cam;

    public BookQuest boolQuest;

    Ray ray;
    RaycastHit hit;
    MonsterCharacter targetMonster;

    [SerializeField]
    float damageDuration;
    [SerializeField]
    CameraShake cameraShake;
    [SerializeField]
    GameObject playerRenderer;
    [SerializeField]
    Chouette_forest quester;

    Color originalColor;

    #region Stats
    public string playerName;
    public int playerLevel = 1;
    public int playerExp = 1;
    public int playerExpToLvUp = 1;
    public int playerMaxHP = 10;
    public int playerHP = 10;
    public int playerStr = 1; //Strength for physical based attacks
    public int playerInt = 1; //Intelligence for Magic Based Attacks
    public int playerRes = 1; //Resistance for Magic Based Attacks
    public int playerDef = 1; //Defense for physical based attacks

    public int ptsMax = 5;
    #endregion

    private bool isPossessingMonster = false;
    public int totalMaxHP = 10;
    public int totalHP=10;
    public int totalStr = 1;
    public int totalInt = 1; //Total Intelligence
    public int totalDef = 1;
    public int totalRes = 1; //Total Magic Resistance

    public Projector rangeProjector;


    Vector3 spontaneousVector3;
    // Use this for initialization
    void Start () {
        ///Stats Initialization-------------
        playerName = ppm.PlayerName() != "" ? ppm.PlayerName() : "Name";
        playerLevel = ppm.GetValue("Level") != 0 ? ppm.GetValue("Level") : 1;
        playerHP = ppm.GetValue("Vie") != 0 ? ppm.GetValue("Vie") : 5;
        playerMaxHP = ppm.GetValue("VieMax") != 0 ? ppm.GetValue("VieMax") : 10;
        playerExp = ppm.GetValue("Exp") != 0 ? ppm.GetValue("Exp") : 0;
        playerExpToLvUp = ppm.GetValue("ExpMax") != 0 ? ppm.GetValue("ExpMax") : 50;
        playerStr = ppm.GetValue("Force") != 0 ? ppm.GetValue("Force") : 1;
        playerDef = ppm.GetValue("Constitution") != 0 ? ppm.GetValue("Constitution") : 1;
        playerInt = ppm.GetValue("Intelligence") != 0 ? ppm.GetValue("Intelligence") : 1;
        playerRes = ppm.GetValue("Volonte") != 0 ? ppm.GetValue("Volonte") : 1;
        ptsMax = ppm.GetValue("PtsMax") != 0 ? ppm.GetValue("PtsMax") : 5;
        ///---------------------------------

        floatingName.text = playerName;
        UIManager.instance.UpdateStatusLevel();
        UIManager.instance.UpdateStatusExp();
        UIManager.instance.UpdateInfoText("");

	}
	
	// Update is called once per frame
	void Update () {
        PossessionProcess();

        UIManager.instance.UpdateStatusUI();
        if(isPossessingMonster && possessedMonster.attackTarget !=null)
        {
            UIManager.instance.UpdateStatusTarget(possessedMonster.attackTarget);

            if(Vector3.Distance(transform.position, possessedMonster.attackTarget.transform.position) > 2*possessedMonster.simpleAttackRange)
            {
                RemoveTarget();
            }
        }
	}

    void OnEnable()
    {
        ///Stats Initialization-------------
        playerName = ppm.PlayerName() != "" ? ppm.PlayerName() : "Name";
        playerLevel = ppm.GetValue("Level") != 0 ? ppm.GetValue("Level") : 1;
        playerHP = ppm.GetValue("Vie") != 0 ? ppm.GetValue("Vie") : 5;
        playerMaxHP = ppm.GetValue("VieMax") != 0 ? ppm.GetValue("VieMax") : 10;
        playerExp = ppm.GetValue("Exp") != 0 ? ppm.GetValue("Exp") : 0;
        playerExpToLvUp = ppm.GetValue("ExpMax") != 0 ? ppm.GetValue("ExpMax") : 50;
        playerStr = ppm.GetValue("Force") != 0 ? ppm.GetValue("Force") : 1;
        playerDef = ppm.GetValue("Constitution") != 0 ? ppm.GetValue("Constitution") : 1;
        playerInt = ppm.GetValue("Intelligence") != 0 ? ppm.GetValue("Intelligence") : 1;
        playerRes = ppm.GetValue("Volonte") != 0 ? ppm.GetValue("Volonte") : 1;
        ptsMax = ppm.GetValue("PtsMax") != 0 ? ppm.GetValue("PtsMax") : 5;
        ///---------------------------------
    }

    void PossessionProcess()
    {
        //On Click Events---------------------------------------------------------
        if(Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, monsterLayerMask))
            {
                if (isPossessingMonster) //Case where we are possessing a monster
                {
                    possessedMonster.attackTarget = hit.collider.GetComponent<MonsterCharacter>();
                    possessedMonster.attackTarget.GetComponent<Renderer>().material.color = Color.red;
                    UIManager.instance.DisplayAttackButton();

                    //Display And Update Target Monster Status
                    // -- Display Status
                    UIManager.instance.DisplayTargetStatus(possessedMonster.attackTarget);
                }
                else //Case where we are not possessing any monster
                {
                    targetMonster = hit.collider.GetComponent<MonsterCharacter>();
                    if (targetMonster != null && targetMonster != possessedMonster)
                    {
                        UIManager.instance.DisplayPossessButton();
                        //Update and display information about targeted monster
                        //UIManager.instance.UpdateStatusTarget();
                        //UIManager.instance.DisplayTargetStatus();
                    }
                }
                
            }
            else if(Physics.Raycast(ray, out hit, questerLayerMask))
            {
                Quester q = hit.collider.GetComponent<Quester>();
                if (quester != null && quester.QuestId == 2)
                {
                    quester.Talk();
                }
            }
            else
            {
                targetMonster = null;
            }
        }

        //-----------------------------------------------------------------------
        if (isPossessingMonster)
        {
            possessedMonster.transform.position = transform.position;
            possessedMonster.transform.rotation = transform.rotation;
            UIManager.instance.UpdateAttackButton(possessedMonster.simpleAttackCurrentCoolDownLevel / possessedMonster.simpleAttackCoolDown);

            if (possessedMonster.attackTarget != possessedMonster && possessedMonster.attackTarget != null)
            {
                rangeProjector.orthographicSize = Mathf.Lerp(rangeProjector.orthographicSize, possessedMonster.simpleAttackRange, 10 * Time.deltaTime);
            }
            else
            {
                rangeProjector.orthographicSize =  Mathf.Lerp(rangeProjector.orthographicSize, 0, 10 * Time.deltaTime);
            }
        }
    }

    public void PossessMonster()
    {
        if(Vector3.Distance(transform.position, targetMonster.transform.position) < 6)
        {
            if(playerLevel >= targetMonster.monsterLevel)
            {
                possessedMonster = targetMonster;
                targetMonster = null;
                isPossessingMonster = true;
                possessedMonster.possessed = true;
                possessedMonster.monsterCollider.enabled = false;
                possessedMonster.DisableAI();
                possessedMonster.transform.rotation = transform.rotation;
                StartCoroutine("PossessMonsterProcess", transform.position);
            }
            else
            {
                UIManager.instance.UpdateInfoText("Niveau du monstre trop élevé");
            }
        }
    }

    public void ReleaseMonster()
    {
        AddExperience(possessedMonster.possessionExp);

        possessedMonster.transform.parent = null;
        spontaneousVector3 = transform.position + Vector3.up * 3;
        rangeProjector.orthographicSize = 0;
        rangeProjector.gameObject.SetActive(false);
        UIManager.instance.HideAttackButton();
        StartCoroutine("ReleaseMonsterProcess", spontaneousVector3);

    }

    public void AddExperience(int amount)
    {
        playerExp += amount;
        if(playerExp > playerExpToLvUp)
        {
            levelUp();
            UIManager.instance.DisplayLevelUp();
        }

        if(ppm.PlayerName() == playerName)
        {
            SaveStats();
        }

        UIManager.instance.UpdateStatusLevel();
        UIManager.instance.UpdateStatusExp();
    }

    void levelUp()
    {
        //faire apparaitre à l'écran une fenêtre pour choisir les stats à up (5-7 points dispo pour up)
        playerLevel++;
        playerExp = playerExp % playerExpToLvUp;
        playerExpToLvUp *= 2;
        if (playerExp >= playerExpToLvUp)
            levelUp();
        else
        {
            SaveStats();
        }
    }

    public void SaveStats()
    {
        ppm.SetValuePlayer("Level", playerLevel);
        ppm.SetValuePlayer("Exp", playerExp);
        ppm.SetValuePlayer("ExpMax", playerExpToLvUp);
        ppm.SetValuePlayer("Vie", playerHP);
        ppm.SetValuePlayer("VieMax", playerMaxHP);
        ppm.SetValuePlayer("Force", playerStr);
        ppm.SetValuePlayer("Constitution", playerDef);
        ppm.SetValuePlayer("Intelligence", playerInt);
        ppm.SetValuePlayer("Volonte", playerRes);
        ppm.SetValuePlayer("PtsMax", ptsMax);
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, possessedMonster.attackTarget.transform.position) <= possessedMonster.simpleAttackRange && possessedMonster.isReadyToAttack)
        {
            possessedMonster.AttackTarget(possessedMonster.attackTarget, totalStr);
            if (possessedMonster.attackTarget.monsterHP <= 0)
            {
                //Victoire contre un monstre
                AddExperience(possessedMonster.attackTarget.battleExp);
                //Déréférencement du monstre dans le script
                possessedMonster.attackTarget = null;
                UIManager.instance.HideAttackButton();
                UIManager.instance.UpdateStatusExp();
                UIManager.instance.HideTargetStatus();
            }
        }
        else if (!possessedMonster.isReadyToAttack)
        {
            UIManager.instance.UpdateInfoText("Attaque non prête!");
        }
        else
        {
            UIManager.instance.UpdateInfoText("Cible trop loin!");
        }
    }

    void MergeMonsterStats()
    {
        if(possessedMonster != null)
        {
            totalMaxHP = playerMaxHP + possessedMonster.monsterHP;
            totalHP = playerHP + possessedMonster.monsterHP;
            totalStr = playerStr + possessedMonster.monsterStr;
            totalInt = playerInt + possessedMonster.monsterInt;
            totalRes = playerRes + possessedMonster.monsterRes;
            totalDef = playerDef + possessedMonster.monsterDef;
        }
    }

    void TakeDamages(int amount)
    {
        cameraShake.enabled = true;
        cameraShake.shakeDuration = 0.1f;
        StartCoroutine("TakeDamageWait");
        totalHP -= amount - (totalDef / 3);

        if (totalHP <= possessedMonster.monsterMaxHP)
        {
            possessedMonster.TakeDamages(possessedMonster.monsterHP - totalHP);
        }
    }

    public void RemoveTarget()
    {
        possessedMonster.attackTarget.GetComponent<Renderer>().material.color = Color.white;
        possessedMonster.attackTarget = null;
        UIManager.instance.HideAttackButton();
        UIManager.instance.HideTargetStatus();
    }

    IEnumerator PossessMonsterProcess(Vector3 targetVec)
    {
        while (Vector3.Lerp(possessedMonster.transform.position, transform.position, 5*Time.fixedDeltaTime) != transform.position ){
            possessedMonster.transform.position = Vector3.Lerp(possessedMonster.transform.position, transform.position, 5 * Time.fixedDeltaTime);
            yield return new WaitForEndOfFrame();
        }
       
        possessedMonster.transform.parent = transform;
        AddExperience(possessedMonster.possessionExp);
        playerRenderer.SetActive(false);

        Debug.Log(rangeProjector.orthographicSize);
        Debug.Log(possessedMonster.simpleAttackRange);
        rangeProjector.orthographicSize = 0;
        rangeProjector.gameObject.SetActive(true);
        MergeMonsterStats();

        UIManager.instance.StartPossessAniamtion();
        UIManager.instance.UpdateInfoText("");
        UIManager.instance.UpdateStatusExp();
        UIManager.instance.DisplayReleaseButton();
        yield return null;
    }

    IEnumerator ReleaseMonsterProcess(Vector3 targetVec)
    {

        //transform.Translate(Vector3.up * 10);
        possessedMonster.EnableAI();
        possessedMonster.monsterCollider.enabled = true;
        possessedMonster.possessed = false;
        possessedMonster = null;
        isPossessingMonster = false;


        playerRenderer.SetActive(true);
        totalMaxHP = playerMaxHP;
        totalHP = playerHP;
        totalStr = playerStr;
        totalInt = playerInt;
        totalDef = playerDef;
        totalRes = playerRes;

        //UI
        
        UIManager.instance.HidePossessButton();
        UIManager.instance.HideReleaseButton();

        yield return null;
    }

    IEnumerator TakeDamageWait()
    {
        //playerRenderer.materials[0].color = Color.red;
        yield return new WaitForSeconds(damageDuration);
        //playerRenderer.materials[0].color = originalColor;
        StopCoroutine("TakeDamageWait");

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Zone") && quester != null)
        {
            quester.DisableZone(col.GetComponent<ZoneQuest>().id);
        }
    }
}
