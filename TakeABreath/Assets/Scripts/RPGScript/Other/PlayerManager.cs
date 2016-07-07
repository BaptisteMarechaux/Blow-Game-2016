using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharactereClass _playerStats;
    [SerializeField]
    private MonsterClass _monstrePossede;
    [SerializeField]
    private TextMesh _nameFlottant;
    [SerializeField]
    private LayerMask _monstreLayer;
    [SerializeField]
    private LayerMask _questerLayer;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private BookQuest _bookQuest;

    Ray _ray;
    RaycastHit _hit;
    MonsterClass _target;

    [SerializeField]
    float damageDuration;
    [SerializeField]
    CameraShake cameraShake;
    [SerializeField]
    GameObject playerRenderer;
    [SerializeField]
	UIManager managerUI;
	[SerializeField]
	Chouette_forest quester;

    Color originalColor;

    //private NetworkPlayer _playerId;
    private bool inPossession = false;
    private float rate = 10;
    private int _vieTotal = 10;
    private int _vieMaxTotal = 10;
    private int _forceTotal = 1;
    private int _consTotal = 1;
    private int _intTotal = 1;
	private int _volTotal = 1;


    public Transform playerPosition;

    public Projector rangeProjector;

    public MonsterClass MonstrePossede
    {
        get
        {
            return _monstrePossede;
        }

        set
        {
            _monstrePossede = value;
        }
    }

    public CharactereClass PlayerStats
    {
        get
        {
            return _playerStats;
        }

        set
        {
            _playerStats = value;
        }
    }

    public MonsterClass Target
    {
        get
        {
            return _target;
        }
    }

    public int VieTotal
    {
        get
        {
            return _vieTotal;
        }

        set
        {
            _vieTotal = value;
        }
    }

    public int VieMaxTotal
    {
        get
        {
            return _vieMaxTotal;
        }

        set
        {
            _vieMaxTotal = value;
        }
    }

    public int ForceTotal
    {
        get
        {
            return _forceTotal;
        }

        set
        {
            _forceTotal = value;
        }
    }

    public int ConsTotal
    {
        get
        {
            return _consTotal;
        }

        set
        {
            _consTotal = value;
        }
    }

    public int IntTotal
    {
        get
        {
            return _intTotal;
        }

        set
        {
            _intTotal = value;
        }
    }

    public int VolTotal
    {
        get
        {
            return _volTotal;
        }

        set
        {
            _volTotal = value;
        }
    }

	public int PtsMax
	{
		get
		{
			return _playerStats.PtsMax;
		}

		set
		{
			_playerStats.PtsMax = value;
		}
	}


    // Use this for initialization
    void Start()
    {
        _nameFlottant.text = PlayerStats.Name;

        UIManager.instance.UpdateStatusLevel();
        UIManager.instance.UpdateStatusExp();
        UIManager.instance.UpdateInfoText("");

        VieTotal = PlayerStats.Sante;
        VieMaxTotal = PlayerStats.SanteMax;
        ForceTotal = PlayerStats.Force;
        ConsTotal = PlayerStats.Defense;
        IntTotal = PlayerStats.Intel;
        VolTotal = PlayerStats.Volonte;

        for (int i = 0; i < _bookQuest.allQuests.Length; i++)
        {
            //if (_bookQuest.allQuests[i])
            //{
            //    Quest q = _bookQuest.allQuests[i];
            //    UIManager.instance.DisplayActiveQuest(q.Title, q.Objectif);
            //}
        }

        //originalColor = playerRenderer.materials[0].color;
    }

    void Update()
    {

        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
			if (Physics.Raycast (_ray, out _hit, Mathf.Infinity, this._monstreLayer)) {
				_target = _hit.collider.GetComponent<MonsterClass> ();
				managerUI.DisplayTargetStatus (_target);
				managerUI.UpdateStatusTarget (_target);
			} 
			else if (Physics.Raycast (_ray, out _hit, Mathf.Infinity, this._questerLayer)) 
			{
				Quester q = _hit.collider.GetComponent<Quester> ();
				if (quester != null && quester.QuestId == 2) 
				{
					quester.Talk();
				}
			}
        }

        if (inPossession)
        {
            //this._myUI.HealthBarUpdate();
            if (this._target != null)
            {
                //faire apparaitre bouton attaque et barre de vie enemy
                UIManager.instance.DisplayAttackButton();
                UIManager.instance.DisplayTargetStatus(Target);
            }
            if (this._monstrePossede.Sante <= 0)
            {
                noBody();
            }
        }
        else if (_target != null && !inPossession)
        {
            //faire apparaitre bouton possession
            UIManager.instance.DisplayPossessButton();
            UIManager.instance.HideAttackButton();

            rangeProjector.orthographicSize = MonstrePossede.Attack.Range;
        }
        else if (_target != null && !inPossession)
        {
            UIManager.instance.HidePossessButton();
        }


        if (MonstrePossede != null)
        {
            this.MonstrePossede.transform.position = playerPosition.position;
            this.MonstrePossede.transform.rotation = playerPosition.rotation;

            UIManager.instance.UpdateAttackButton(MonstrePossede.Attack._timer / MonstrePossede.Attack.Cooldown);
        }

        if(Input.GetKeyDown(KeyCode.K))
            TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.L))
            AddExp(100);
    }

    private void noBody()
    {
        Depossession();
    }

    public void Depossession()
	{
		this._playerStats.addExp(this._monstrePossede.ExpToPossess);
        //Attribuer le transform
        this._monstrePossede.transform.parent = null;

        this._monstrePossede.EnableAI();
		this._monstrePossede.transform.Translate(this._monstrePossede.Player.transform.up.normalized * 3);
        //this._monstrePossede.transform.LookAt(this._monstrePossede.Player.transform);
        this._monstrePossede.GetComponent<CapsuleCollider>().enabled = true;
        this._monstrePossede.Player = null;
        this._monstrePossede = null;
        this.inPossession = false;

        playerRenderer.SetActive(true);

        this.VieTotal = PlayerStats.Sante;
        this.VieMaxTotal = PlayerStats.SanteMax;
        this.ForceTotal = PlayerStats.Force;
        this.ConsTotal = PlayerStats.Defense;
        this.IntTotal = PlayerStats.Intel;
        this.VolTotal = PlayerStats.Volonte;

		if (quester != null && quester.QuestId == 3)
			quester.QuestFinish ();
        
		//UI
        UIManager.instance.HidePossessButton();
        UIManager.instance.HideReleaseButton();

    }

    public void essaiPossession()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= 5)
        {
            if (PlayerStats.Level >= _target.Level && _target.Player == null && MonstrePossede == null)
            {
                this.MonstrePossede = this._target;
                this._target = null;

                this.MonstrePossede.Player = this;
                this.inPossession = true;
                //playerPosition.position = this.MonstrePossede.transform.position;
                this.MonstrePossede.transform.position = playerPosition.position+ new Vector3(0,22,0);
                this.MonstrePossede.transform.rotation = playerPosition.rotation;
                this.MonstrePossede.transform.parent = playerPosition;
                this._playerStats.addExp(this._monstrePossede.ExpToPossess);
                this._monstrePossede.DisableAI();
                this._monstrePossede.GetComponent<CapsuleCollider>().enabled = false;

                playerRenderer.SetActive(false);
                StatUpdateWithMonster();

                rangeProjector.gameObject.SetActive(true);

                //UI
                UIManager.instance.StartPossessAniamtion();
                UIManager.instance.UpdateInfoText("");
                UIManager.instance.UpdateStatusExp();
                UIManager.instance.HidePossessButton();
                UIManager.instance.DisplayReleaseButton();

                this._monstrePossede.transform.parent = PlayerStats.transform;

            }
            else if (PlayerStats.Level < this._target.Level)
            {
                UIManager.instance.UpdateInfoText("Niveau du monstre trop élevé!");
            }
        }
        else
        {
            UIManager.instance.UpdateInfoText("Créature trop loin.");
        }
        /*
        //ajouter la condition dans le if du dessus
        else if(monster.PlayerId != null)
        {
            this._myUI.InfoTextUpdate("Créature déjà possédée!");
        }
        */
    }

    public void StatUpdateWithMonster()
    {
        if (this._monstrePossede != null)
        {
            this.VieTotal = PlayerStats.Sante + MonstrePossede.Sante;
            this.VieMaxTotal = PlayerStats.SanteMax + MonstrePossede.SanteMax;
            this.ForceTotal = PlayerStats.Force + MonstrePossede.Force;
            this.ConsTotal = PlayerStats.Defense + MonstrePossede.Defense;
            this.IntTotal = PlayerStats.Intel + MonstrePossede.Intel;
            this.VolTotal = PlayerStats.Volonte + MonstrePossede.Volonte;
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= this._monstrePossede.Attack.Range && this._monstrePossede.Attack.Ready)
        {
            _monstrePossede.AttackTarget(_target,_forceTotal);
            if (_target.Sante <= 0)
            {
                //Victoire contre un monstre
                AddExp(_target.Exp);
                //Déréférencement du monstre dans le script
                _target = null;
                UIManager.instance.HideAttackButton();
                UIManager.instance.UpdateStatusExp();
                UIManager.instance.HideTargetStatus();
            }
        }
        else if(!this._monstrePossede.Attack.Ready)
        {
            UIManager.instance.UpdateInfoText("Attaque non prête!");
        }
        else
        {
            UIManager.instance.UpdateInfoText("Cible trop loin!");
        }
    }

    public void AddExp(int amount)
    {
        UIManager.instance.UpdateStatusLevel();
        UIManager.instance.UpdateStatusExp();
        if (_playerStats.addExp(amount) > 0)
        {
            UIManager.instance.DisplayLevelUp();
        }
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Zone") && quester != null)
		{	
			quester.DisableZone(col.GetComponent<ZoneQuest>().id);
		}
	}



    public void TakeDamage(int damage)
    {
        cameraShake.enabled = true;
        cameraShake.shakeDuration = 0.1f;
        StartCoroutine("TakeDamageWait");
        this._vieTotal -= damage - (this._consTotal / 3);

        if(this._vieTotal <= this.MonstrePossede.SanteMax)
        {
            this.MonstrePossede.TakeDamage(this.MonstrePossede.Sante - this._vieTotal);
        }
        
    }

    IEnumerator TakeDamageWait()
    {
        //playerRenderer.materials[0].color = Color.red;
        yield return new WaitForSeconds(damageDuration);
        //playerRenderer.materials[0].color = originalColor;
        StopCoroutine("TakeDamageWait");

    }
    
}
