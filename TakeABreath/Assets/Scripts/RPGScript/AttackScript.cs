using UnityEngine;
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
                this._attackButon.SetActive(true);
            }
        }
    }

    public void Attaque()
    {
        _target.TakeDamage(this._player.getForce());
        if(!_target.IsAlive)
        {
            this._player.Me.addExp(_target.Exp);
            this._attackButon.SetActive(false);
        }
    }
}
