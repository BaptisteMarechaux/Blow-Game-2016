using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ToolsForPlayer : NetworkBehaviour
{
    enum ToolMode { nothing, cube, text, image, sphere, pointer, inSelection};

    ToolMode tm;

    [SerializeField]
    GameObject prefab1;
    [SerializeField]
    GameObject prefab2;
    [SerializeField]
    GameObject prefab3;
    [SerializeField]
    GameObject prefab4;

    [SerializeField]
    GameObject pointer;
    [SerializeField]
    GameObject box;
    [SerializeField]
    GameObject sphere;

    Text tx;

    ToolMode previousTm = ToolMode.nothing;

    void Start()
    {
        tm = ToolMode.nothing;
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                tm = ToolMode.nothing;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                tm = ToolMode.cube;
                box.SetActive(true);
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                tm = ToolMode.text;
                box.SetActive(true);
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                tm = ToolMode.image;
                box.SetActive(true);
            }
            else
            {
                box.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                tm = ToolMode.pointer;
                pointer.SetActive(true);
            }
            else
            {
                pointer.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                tm = ToolMode.sphere;
                sphere.SetActive(true);
            }
            else
            {
                sphere.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            switch (tm)
            {
                case ToolMode.cube:
                    CmdSpawnCube();
                    //(this.transform.forward * 10, ForceMode.Impulse);
                    break;
                case ToolMode.text:
                    CmdSpawnText();
                    break;
                case ToolMode.image:
                    CmdSpawnImage();
                    break;
                case ToolMode.nothing:
                    break;
                case ToolMode.pointer:
                    break;
                case ToolMode.sphere:
                    CmdSpawnSphere();
                    break;
                default:
                    break;
            }
        }
    }

    public void TurnPlayerToSelection(bool on)
    {
        if (on)
        {
            previousTm = tm;
            tm = ToolMode.inSelection;
        }
        else
        {
            tm = previousTm;
        }
    }

    public bool IsInSelectionMode()
    {
        if(tm == ToolMode.inSelection)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //will auto-resize the texture dimensions.
        }
        return tex;
    }

    [Command]
    void CmdSpawnCube()
    {
        NetworkServer.Spawn(Instantiate(prefab1, this.transform.position + this.transform.forward * 2.5f, this.transform.rotation) as GameObject);
    }

    [Command]
    void CmdSpawnText()
    {
        GameObject g = (Instantiate(prefab2, this.transform.position + this.transform.forward * 2.5f, this.transform.rotation) as GameObject);
        NetworkServer.SpawnWithClientAuthority(g, this.gameObject);
    }

    [Command]
    void CmdSpawnImage()
    {
        GameObject g = (Instantiate(prefab3, this.transform.position + this.transform.forward * 3, this.transform.rotation) as GameObject);
        NetworkServer.Spawn(g);
    }

    [Command]
    void CmdSpawnSphere()
    {
        GameObject g = (Instantiate(prefab4, this.transform.position + this.transform.forward * 1, this.transform.rotation) as GameObject);
        g.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity + this.transform.forward * 10;
        Destroy(g, 8f);
        NetworkServer.Spawn(g);
    }
}
