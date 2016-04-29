using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebTexture : MonoBehaviour
{
    public bool KeyboardEnabled = true;
    public bool MouseEnabled = true;
    public bool Rotate = false;
    public bool HasFocus = true;
    public bool AlphaMask = false;

    private bool editingMode;

    [SerializeField]
    Transform cubePos;

    [SerializeField]
    GameObject cube1;
    [SerializeField]
    GameObject cube2;

    UWKWebView view;

    // Use this for initialization
    void Awake ()
    {

        view = gameObject.GetComponent<UWKWebView>();

        view.SetAlphaMask(AlphaMask);

        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().material.mainTexture = view.WebTexture;

        if (GetComponent<GUITexture>() != null)
            GetComponent<GUITexture>().texture = view.WebTexture;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (!MouseEnabled || !HasFocus)
            return;

        //Debug.DrawRay(cube1.transform.position, cube2.transform.position, Color.green);
        RaycastHit rcast;

        if (!cubePos)
        {
            if (GameObject.Find("Avatar(Clone)"))
                cubePos = GameObject.Find("Avatar(Clone)").transform;

        }
        else
        {
            editingMode = false;
            Debug.DrawRay(cubePos.transform.position, cubePos.forward, Color.green);
            if (Physics.Raycast(cubePos.transform.position, cubePos.forward, out rcast))
            {
                if (rcast.collider != GetComponent<MeshCollider>())
                    return;

                int x = (int)(rcast.textureCoord.x * (float)view.MaxWidth);
                int y = view.MaxHeight - (int)(rcast.textureCoord.y * (float)view.MaxHeight);

                Vector3 mousePos = new Vector3();
                mousePos.x = x;
                mousePos.y = y;
                view.ProcessMouse(mousePos);
                editingMode = true;
            }
        }

        /*Vector3 mousePos = new Vector3();
        mousePos.x = cubePos.position.x;
        mousePos.y = cubePos.position.y;*/
        //view.ProcessMouse(mousePos);
    }
        
    void OnGUI ()
    {
        if (!KeyboardEnabled || !HasFocus /*|| !editingMode*/)
            return;

        if (Event.current.isKey && editingMode)
        {
            view.ProcessKeyboard(Event.current);
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zone")
        {
            editingMode = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone")
        {
            editingMode = false;
        }
    }*/

}