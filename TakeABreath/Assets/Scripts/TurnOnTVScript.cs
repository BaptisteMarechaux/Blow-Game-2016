using UnityEngine;
using System.Collections;

public class TurnOnTVScript : MonoBehaviour {

    [SerializeField]
    Transform cubePos;
    [SerializeField]
    UWKWebView uwv;
    [SerializeField]
    WebTexture wt;
    [SerializeField]
    GameObject screen;
    [SerializeField]
    Material blackScreenMat;

    Renderer rend;

    bool isOn;


    // Use this for initialization
    void Start()
    {
        rend = screen.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rcast;

            if (GameObject.Find("Avatar(Clone)"))
                cubePos = GameObject.Find("Avatar(Clone)").transform;
            else
                cubePos = this.transform;

            Debug.DrawRay(cubePos.transform.position, cubePos.forward, Color.red);
            if (Physics.Raycast(cubePos.transform.position, cubePos.forward, out rcast))
            {
                if (rcast.collider != GetComponent<BoxCollider>())
                    return;

                isOn = !isOn;

                if (isOn)
                {
                    uwv.enabled = true;
                    wt.enabled = true;

                    this.transform.Translate(this.transform.forward * 0.1f, Space.World);
                }
                else
                {
                    uwv.enabled = false;
                    wt.enabled = false;

                    this.transform.Translate(this.transform.forward * -0.1f, Space.World);
                    rend.material = blackScreenMat;
                }

            }
        }
    }
}
