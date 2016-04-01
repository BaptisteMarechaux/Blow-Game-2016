using UnityEngine;
using System.Collections;

public class InventoryClass : MonoBehaviour {

    [SerializeField]
    private int _gold = 0;

    public int Gold
    {
        get
        {
            return _gold;
        }

        set
        {
            _gold = value;
        }
    }

}
