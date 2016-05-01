using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GateForAuthority : NetworkBehaviour
{
    TchatScript ts;

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer &&  !isServer)
            return;
        ts = GameObject.Find("ChatInputField").GetComponent<TchatScript>();
        ts.gfa = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SendChangeText(string text)
    {
        CmdChangeText(text);
    }

    [Command]
    void CmdChangeText(string txt)
    {
        ts.ChangeText(txt);
        RpcUpdateChat();
    }

    [ClientRpc]
    void RpcUpdateChat()
    {
        ts.UpdateChat();
    }
}
