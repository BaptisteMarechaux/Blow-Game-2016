using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Move : NetworkBehaviour {

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    [SerializeField] Camera mainCamera;
    [SerializeField]AudioListener audioLis;
    [SerializeField]
    Transform mainCameraTransform;
    [SerializeField]
    GameObject shape;
    [SerializeField]
    Renderer headRenderer;

    Rigidbody rg;

    [SyncVar(hook = "OnColor")]
    public Color myColor;

    [SyncVar(hook = "OnRotate")]
    public Quaternion myRotation;

    // Use this for initialization
    void Start () {
        rg = this.GetComponent<Rigidbody>();
        if (!isLocalPlayer /*&& !isServer*/)
        {
            audioLis.enabled = false;
            mainCamera.enabled = false;
        }
        headRenderer.material.color = myColor;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        yaw += 2 * Input.GetAxis("Mouse X");
        pitch -= 2 * Input.GetAxis("Mouse Y");

        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) 
        {
            shape.transform.rotation = Quaternion.Lerp(shape.transform.rotation, mainCameraTransform.rotation, Time.deltaTime * 4.0f);
            myRotation = shape.transform.rotation;
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            // Hide cursor when locking
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rg.AddForce(Input.GetAxis("Vertical") * mainCameraTransform.forward * 5 * Time.deltaTime, ForceMode.VelocityChange);
        rg.AddForce(Input.GetAxis("Horizontal") * mainCameraTransform.right * 5 * Time.deltaTime, ForceMode.VelocityChange);
        //rg.AddForce(Input.GetAxis("Horizontal") * this.transform.right * 500 * Time.deltaTime, ForceMode.Impulse);
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            rg.velocity = Vector3.Lerp(rg.velocity, Vector3.zero, Time.deltaTime * 10f);
        rg.angularVelocity = Vector3.Lerp(rg.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
    }

    public override void OnStartLocalPlayer()
    {
        //base.OnStartLocalPlayer();
    }

    void OnColor(Color newColor)
    {
        headRenderer.material.color = newColor;
        myColor = newColor;
    }

    void OnRotate(Quaternion newRotation)
    {
        shape.transform.rotation = newRotation;
    }

    public override void OnStartClient()
    {
        //shape.GetComponent<Renderer>().material.color = myColor;
    }

}
