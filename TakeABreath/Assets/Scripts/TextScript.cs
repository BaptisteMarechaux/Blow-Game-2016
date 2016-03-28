using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextScript : NetworkBehaviour {

    GameObject canvas;
    [SerializeField]
    Text tx;

    [SyncVar(hook = "OnStr")]
    string str;
    private bool editingMode;

    [SerializeField]
    GameObject selectionFeedback;

    float pressTime;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas");
        transform.SetParent(canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClient)
            return;

        if (editingMode)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (tx.text.Length > 0)
                    tx.text = tx.text.Remove(tx.text.Length - 1);
            }
            else
            {
                tx.text = tx.text.Insert(tx.text.Length, Input.inputString);
            }
            if (Input.GetKey(KeyCode.Backspace))
            {
                pressTime += Time.deltaTime;
                if (pressTime > 0.5f)
                {
                    if (tx.text.Length > 0)
                        tx.text = tx.text.Remove(tx.text.Length - 1);
                }
            }
            else
            {
                pressTime = 0;
            }
                if (Input.GetKeyDown(KeyCode.Return))
                CmdChangeText(tx.text.Insert(tx.text.Length, "\n"));
            if(Input.GetKeyDown(KeyCode.Space))
                CmdChangeText(tx.text);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zone")
        {
            GetComponent<NetworkIdentity>().RemoveClientAuthority(GetComponent<NetworkIdentity>().clientAuthorityOwner);
            GetComponent<NetworkIdentity>().AssignClientAuthority(other.GetComponentInParent<Move>().connectionToClient); //other.GetComponentInParent<Move>().connectionToClient
            editingMode = true;
            selectionFeedback.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone")
        {
            editingMode = false;
            selectionFeedback.SetActive(false);
        }
    }

    [Command]
    void CmdChangeText(string txt)
    {
        str = txt;
        tx.text = str;
    }

    void OnStr(string netwStr)
    {
        tx.text = netwStr;
    }
}
