using UnityEngine;
using System.Collections;

public class handMovement : MonoBehaviour {
    public float minX = -0.2f;
    public float maxX = 0.4f;

    public float minZ = -0.2f;
    public float maxZ = 0.4f;
    public float speedX = 5.0f;
    public float speedZ = 5.0f;

    float posX;
    float posY;
    float posZ;

	// Use this for initialization
	void Start () {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKey(KeyCode.Mouse0) {
            handY -= speedY * Time.deltaTime;
            handY = Mathf.Clamp(handY, minY, maxY);
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }*/
        //else if (penY <= 0.7f) { 
        //    penY += speedY * Time.deltaTime;
        //    transform.Translate(new Vector3(0, 0, penY));
        //}
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.position = new Vector3(posX, posY - 0.1f, posZ);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.position = new Vector3(posX, posY, posZ);
        }

        //Hand movement (X+Z)
        posX += Input.GetAxis("Mouse X") * speedX * Time.deltaTime;
        posZ += Input.GetAxis("Mouse Y") * speedZ * Time.deltaTime;
        posX = Mathf.Clamp(posX, minX, maxX);
        posZ = Mathf.Clamp(posZ, minZ, maxZ);
        transform.position= new Vector3(posX, transform.position.y, posZ);
    }
}
