using UnityEngine;
using System.Collections;



public class cameraMovement : MonoBehaviour {

    public float minX = -80.0f;
    public float maxX = 80.0f;

    public float minY = -35.0f;
    public float maxY = 35.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    public float speedX = 50.0f;
    public float speedY = 50.0f;

    float rotationX = 0.0f;
    float rotationY = 0.0f;

    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
	}

    // Update is called once per frame
    void Update() {
        // Mouse movement (mouse used for handmovement now)
        /*if (!Input.GetMouseButton(0)) {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
        }
		*/

        // Basic keyboard movement
        if (Input.GetKey(KeyCode.W)) {
            rotationY += speedY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            rotationY -= speedY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            rotationX -= speedX * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            rotationX += speedX * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

    }
}