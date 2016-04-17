using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TchatScript : NetworkBehaviour
{
    string pseudo = "Invité";

    [SyncVar(hook = "OnStr")]
    string str;

    [SerializeField]
    InputField text;
    [SerializeField]
    Text chatText;
    [SerializeField]
    RectTransform rectChat;
    [SerializeField]
    RectTransform rectText;


    private bool editingMode;

    float pressTime;

    public GateForAuthority gfa;

    // Use this for initialization
    void Start () {

    }

    public void AssigAuthority(NetworkConnection connectionToClient)
    {
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    public void ChangingValue()
    {
        editingMode = true;
    }

    public void EndChangingValue()
    {
        StartCoroutine(QuitEditingMode());
    }

    IEnumerator QuitEditingMode()
    {
        yield return new WaitForEndOfFrame();

        editingMode = false;
        print("end");
    }
	
	// Update is called once per frame
	void Update () {

        if (editingMode)
        {
            /*if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (text.text.Length > 0)
                    text.text = text.text.Remove(text.text.Length - 1);
            }
            else
            {
                text.text = text.text.Insert(text.text.Length, Input.inputString);
            }
            if (Input.GetKey(KeyCode.Backspace))
            {
                pressTime += Time.deltaTime;
                if (pressTime > 0.5f)
                {
                    if (text.text.Length > 0)
                        text.text = text.text.Remove(text.text.Length - 1);
                }
            }
            else
            {
                pressTime = 0;
            }*/
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gfa.SendChangeText(text.text);
                text.text = "";
            }
        }
    }

    public void ChangeText(string txt)
    {
        txt = txt.Insert(0, pseudo + " : ");
        str += txt.Insert(txt.Length, "\n");
        chatText.text = str;
    }

    public void UpdateChat()
    {
        rectChat.sizeDelta = new Vector2(rectChat.sizeDelta.x, rectChat.sizeDelta.y + 15);
        if (rectChat.sizeDelta.y > 45)
            rectChat.Translate(new Vector3(0, 15, 0));
    }

    void OnStr(string netwStr)
    {
        chatText.text = netwStr;
    }
}
