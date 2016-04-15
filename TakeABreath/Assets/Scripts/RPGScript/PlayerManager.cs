using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CharactereClass _me;
    [SerializeField]
    private MonsterClass _monstrePossede;
    [SerializeField]
    private TextMesh _nameFlottant;
    [SerializeField]
    private UIscript _myUI;
    [SerializeField]
    private LayerMask _layer;
    [SerializeField]
    private Camera _cam;

    private Ray _ray;
    private RaycastHit _hit;
    private MonsterClass _target;

    //private NetworkPlayer _playerId;
    private bool inPossession = false;
    private float rate = 10;

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

    public CharactereClass Me
    {
        get
        {
            return _me;
        }

        set
        {
            _me = value;
        }
    }

    public MonsterClass Target
    {
        get
        {
            return _target;
        }
    }


    // Use this for initialization
    void Start()
    {
        this._nameFlottant.text = Me.Name;
        this._myUI.levelUpdate();
        this._myUI.expBarInfo();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, this._layer))
            {
                _target = _hit.collider.GetComponent<MonsterClass>();
                Debug.Log(Vector3.Distance(this.transform.position, _target.transform.position));
            }
        }
        
        if (inPossession)
        {
            this._myUI.HealthBarUpdate();
            if (this._target != null)
            {
                //faire apparaitre bouton attaque et barre de vie enemy
                this._myUI.ButtonAttackEnable();
                this._myUI.LifeTargetEnable();
            }
            if (this._monstrePossede.Sante <= 0)
            {
                noBody();
            }
        }
        else if(_target!=null && !inPossession)
        {
            //faire apparaitre bouton possession
            this._myUI.ButtonPossessEnable();
            this._myUI.ButtonAttackDisable();
        }
    }

    private void noBody()
    {
        this._myUI.HealthBarUpdate();
        this._monstrePossede = null;
        inPossession = false;
    }

    public void essaiPossession()
    {
        if (Me.Level >= _target.Level && _target.Player == null && MonstrePossede == null)
        {
            this.MonstrePossede = this._target;
            this._target = null;

            this.MonstrePossede.Player = Me;
            this.inPossession = true;
            this.transform.position = this.MonstrePossede.transform.position;
            this._me.addExp(this._monstrePossede.ExpToPossess);

            //UI
            this._myUI.InfoTextUpdate("");
            this._myUI.expBarInfo();
            this._myUI.HealthBarUpdate();
            this._myUI.ButtonPossessDisable();
        }
        else if (Me.Level < this._target.Level)
        {
            this._myUI.InfoTextUpdate("Niveau du monstre trop élevé!");
        }
        /*
        //ajouter la condition dans le if du dessus
        else if(monster.PlayerId != null)
        {
            this._myUI.InfoTextUpdate("Créature déjà possédée!");
        }
        */
    }
    
    public void Attack()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= this._monstrePossede.Attack.Range)
        {
            this._monstrePossede.AttackTarget(this._target);
            if (this._target.Sante <= 0)
            {
                this._me.addExp(this._target.Exp);
                this._target = null;
                this._myUI.ButtonAttackDisable();
                this._myUI.expBarInfo();
                this._myUI.levelUpdate();
                this._myUI.LifeTargetDisable();
            }
        }
        else
        {
            this._myUI.InfoTextUpdate("Cible trop loin!");
        }
    }

}
