using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    float speed  = 5.0f;
    public Transform target;
    float sensitivity = 0.5f;
    float fav = 0;

    void FixedUpdate()
    {
        transform.Translate(transform.worldToLocalMatrix.MultiplyVector(transform.forward) * fav * sensitivity);
            transform.LookAt(target);
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(target.position, this.transform.right, Input.GetAxis("Mouse Y") * -speed);
            if (Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(target.position.x, target.position.z)) < 1f)
                transform.RotateAround(target.position, this.transform.right, Input.GetAxis("Mouse Y") * speed);
            //transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, -90, 90), transform.eulerAngles.y, transform.eulerAngles.z);
            if(transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
            /*if((transform.eulerAngles.x < 90 || transform.eulerAngles.x > 270) && transform.eulerAngles.x > 180 && transform.eulerAngles.x -360 > -30)
            {
                transform.RotateAround(target.position, this.transform.right, Input.GetAxis("Mouse Y") * speed);
            }*/
        }
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
 
    void Update()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") >= 0 && Vector3.Distance(transform.position, target.position) > 3) || (Input.GetAxis("Mouse ScrollWheel") <= 0 && Vector3.Distance(transform.position, target.position) < 15))
            fav += Input.GetAxis("Mouse ScrollWheel");
        fav = Mathf.Lerp(fav, 0.0f, Time.deltaTime * 5f);
        fav = Mathf.Clamp(fav, -1.0f, 1.0f);
    }
}
