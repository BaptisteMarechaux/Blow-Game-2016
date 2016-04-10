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

    private Ray ray;
    private RaycastHit hit;
    private MonsterClass _target;
    

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && _player.MonstrePossede != null)
        {
            ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, this._layer))
            {
                this._target = hit.collider.GetComponent<MonsterClass>();
                this._hpEnemy.text = this._target.Sante.ToString();
                this._attackButon.SetActive(true);
            }
        }
        if (this._target != null)
        {
            if (this._target.Sante == 0 || this._player.MonstrePossede.Sante == 0)
            {
                this._target = null;
                this._attackButon.SetActive(false);
            }
        }
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
