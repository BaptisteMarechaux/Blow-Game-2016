using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PossessionScript : MonoBehaviour {

    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private PlayerManager _player;
    [SerializeField]
    private GameObject _button;
    [SerializeField]
    private LayerMask _layer;

    private Ray ray;
    private RaycastHit hit;
    private MonsterClass monstre;

    // Update is called once per frame
    void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            ray = _cam.ScreenPointToRay(Input.mousePosition);
    
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, this._layer))
            {
                monstre = hit.collider.GetComponent<MonsterClass>();
                this._button.SetActive(true);
            }
        }
	}

    public void possess()
    {
        this._player.essaiPossession(monstre);
    }
}
