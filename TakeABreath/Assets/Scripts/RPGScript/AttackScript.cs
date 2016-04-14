using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    private PlayerManager _player;
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private LayerMask _layer;
    [SerializeField]
    private GameObject _attackButon;
    [SerializeField]
    private Text _hpEnemy;
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    GameObject _lifeBarObject;

    private Ray ray;
    private RaycastHit hit;
    private MonsterClass _target = null;
    

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && _player.MonstrePossede != null)
        {
            ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, this._layer))
            {
                this._player.ButonPossession.Button.SetActive(false);
                this._target = hit.collider.GetComponent<MonsterClass>();
                this._attackButon.SetActive(true);
                this._lifeBarObject.SetActive(true);
            }
        }

        if (this._target != null)
        {
            healthBarTargetInfo();
            if (this._target.Sante == 0 || this._player.MonstrePossede.Sante == 0)
            {
                this._target = null;
                this._lifeBarObject.SetActive(false);
                this._attackButon.SetActive(false);
            }
        }
    }

    private void healthBarTargetInfo()
    {
        this._hpEnemy.text = this._target.Sante.ToString();
        float itlife = (float)this._target.Sante / (float)this._target.SanteMax; //<== valeur entre 0 et 1
        this._healthBar.transform.localScale = new Vector3(Mathf.Clamp(itlife, 0f, 1f), this._healthBar.transform.localScale.y, this._healthBar.transform.localScale.z);
    }


    public void Attaque()
    {
        //if(this._target.PlayerId != this._player.PlayerId)
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= 3)
        {
            this._target.TakeDamage(this._player.getForce());
            this._hpEnemy.text = this._target.Sante.ToString();
            if (!this._target.IsAlive)
            {
                this._hpEnemy.text = "-";
                this._player.Me.addExp(this._target.Exp);
                this._attackButon.SetActive(false);
            }
        }
        else
        {
            this._player.InfoText.text = "Cible trop loin.";
        }
    }
}
