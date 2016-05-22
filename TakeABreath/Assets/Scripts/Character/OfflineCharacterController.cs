using UnityEngine;
using System.Collections;

public class OfflineCharacterController : MonoBehaviour {
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float rotationSpeed = 10;
    float h, v;
    Vector3 transformPositionVector = new Vector3();

    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    float cameraDistance=10;
    [SerializeField]
    float cameraHeight=5;
    [SerializeField]
    float cameraSpeed=10;
    [SerializeField]
    float cameraRotationSpeed=10;

    //[SerializeField]
    //float cameraSpeed = 1.0f;

    Vector3 forward;
    Vector3 right;

    // Use this for initialization
    void Start () {
         
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        forward = mainCamera.transform.forward;
        right = new Vector3(forward.z, 0, -forward.x);

        transformPositionVector = (h * right + v * forward) * speed * Time.deltaTime;
        transformPositionVector.y = 0;
        //mainCamera.transform.Translate(new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime));
        transform.position = Vector3.MoveTowards(transform.position, transformPositionVector + transform.position, Time.deltaTime * speed);


        if (h != 0 || v != 0)
        {
            GetRotation(new Vector3(h, 0, v));
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z - cameraDistance), cameraSpeed * Time.deltaTime);
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, cameraNewPos.y, transform.position.z - cameraDistance), cameraSpeed * Time.deltaTime);
        }


        mainCamera.transform.LookAt(transform);
        //var lookRotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        //mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, lookRotation, cameraRotationSpeed * Time.deltaTime);    

        if (Input.GetMouseButton(1))
        {
            mainCamera.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
            mainCamera.transform.RotateAround(transform.position, mainCamera.transform.right, Input.GetAxis("Mouse Y") * -speed);
            /*if (Vector3.Distance(new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.z), new Vector3(transform.position.x, transform.position.z)) < 1f)
                mainCamera.transform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y") * speed);
            mainCamera.transform.eulerAngles = new Vector3(ClampAngle(mainCamera.transform.eulerAngles.x, -90, 90), mainCamera.transform.eulerAngles.y, mainCamera.transform.eulerAngles.z);
            if (mainCamera.transform.position.y < mainCamera.transform.position.y - 2)
            {
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y - 2, mainCamera.transform.position.z);
            }
            /*if((transform.eulerAngles.x < 90 || transform.eulerAngles.x > 270) && transform.eulerAngles.x > 180 && transform.eulerAngles.x -360 > -30)
            {
                transform.RotateAround(target.position, this.transform.right, Input.GetAxis("Mouse Y") * speed);
            }*/
        }
        else
        {
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z - cameraDistance), cameraSpeed * Time.deltaTime);
        }

    }

    void GetRotation(Vector3 toRotation)
    {
        Vector3 relativePos = mainCamera.transform.TransformDirection(toRotation);
        relativePos.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {
            if (angle > 180)
                angle -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;
        return angle;
    }
}
