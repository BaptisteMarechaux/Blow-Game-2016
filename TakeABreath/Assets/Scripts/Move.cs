using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    [SerializeField] Camera mainCamera;
    [SerializeField]AudioListener audioLis;

    Rigidbody rg;

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
        rg.AddForce(Input.GetAxis("Vertical") * this.transform.forward * 5 * Time.deltaTime, ForceMode.VelocityChange);
        rg.AddForce(Input.GetAxis("Horizontal") * this.transform.right * 5 * Time.deltaTime, ForceMode.VelocityChange);
        //rg.AddForce(Input.GetAxis("Horizontal") * this.transform.right * 500 * Time.deltaTime, ForceMode.Impulse);
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            rg.velocity = Vector3.Lerp(rg.velocity, Vector3.zero, Time.deltaTime * 10f);
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
        if (!isLocalPlayer)
            audioLis.enabled = false;
    }

}
