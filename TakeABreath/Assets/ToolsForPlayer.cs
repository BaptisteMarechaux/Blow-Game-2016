using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ToolsForPlayer : NetworkBehaviour
{
    enum ToolMode { cube, text, image};

    ToolMode tm;

    [SerializeField]
    GameObject prefab1;
    [SerializeField]
    GameObject prefab2;
    [SerializeField]
    GameObject prefab3;

    Text tx;

    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tm = ToolMode.cube;
        }else
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tm = ToolMode.text;
        }else
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            tm = ToolMode.image;
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
                default:
                    break;
            }
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
        NetworkServer.Spawn(Instantiate(prefab1, this.transform.position + this.transform.forward * 3, this.transform.rotation) as GameObject);
    }

    [Command]
    void CmdSpawnText()
    {
        GameObject g = (Instantiate(prefab2, this.transform.position + this.transform.forward * 3, this.transform.rotation) as GameObject);
        NetworkServer.SpawnWithClientAuthority(g, this.gameObject);
    }

    [Command]
    void CmdSpawnImage()
    {
        GameObject g = (Instantiate(prefab3, this.transform.position + this.transform.forward * 3, this.transform.rotation) as GameObject);
        NetworkServer.Spawn(g);
    }
}
