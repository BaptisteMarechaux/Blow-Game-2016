using UnityEngine;
using System.Collections;

public class PlanetCharacterController : MonoBehaviour {

    [SerializeField]
    Rigidbody rigidbody;
    bool grounded;
    public float walkSpeed = 6;
    public float jumpForce;
    public LayerMask groundedMask;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float V = Input.GetAxis("Vertical");
        float H = Input.GetAxis("Horizontal");

        Vector3 moveDir = new Vector3(H, 0, V).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                Debug.Log("grounded");
                rigidbody.AddForce(transform.up * jumpForce);
            }
            else
            {
                Debug.Log("notgrounded");
            }
        }

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}
