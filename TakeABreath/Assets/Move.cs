using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Rigidbody rg;

    [SerializeField] Camera mainCamera;

    Text tx;

    [SyncVar(hook = "OnColor")]
    public Color myColor;

    // Use this for initialization
    void Start () {
        rg = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            mainCamera.enabled = false;
            return;
        }

        yaw += 2 * Input.GetAxis("Mouse X");
        pitch -= 2 * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //if (Input.GetKeyDown(KeyCode.Space))

    }

    void FixedUpdate()
    {
        rg.AddForce(Input.GetAxis("Vertical") * this.transform.forward * 500 * Time.deltaTime, ForceMode.Acceleration);
        rg.AddForce(Input.GetAxis("Horizontal") * this.transform.right * 500 * Time.deltaTime, ForceMode.Acceleration);

        rg.velocity = Vector3.Lerp(rg.velocity, Vector3.zero, Time.deltaTime * 0.5f);
    }

    public override void OnStartLocalPlayer()
    {
        //base.OnStartLocalPlayer();
        Cursor.lockState = CursorLockMode.Confined;
        // Hide cursor when locking
        Cursor.visible = false;
    }

    void OnColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
        myColor = newColor;
    }

    public override void OnStartClient()
    {
        myColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetComponent<Renderer>().material.color = myColor;
    }

}
