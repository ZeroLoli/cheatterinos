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

    private EyeXHost _eyeXHost;
    private IEyeXDataProvider<EyeXGazePoint> _gazePointProvider;
    //private EyeXFixationDataStream<EyeXFixationPoint> or something like that? _fixPointProvider;

    float rotationX = 0.0f;
    float rotationY = 0.0f;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        _eyeXHost = EyeXHost.GetInstance();
        _gazePointProvider = _eyeXHost.GetGazePointDataProvider
                    (Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
        _gazePointProvider.Start();
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

        // Gazepoint updating !! TRY FIXATION POINT INSTEAD?
        var gazePoint = _gazePointProvider.Last;

        // Camera rotation; either keyboard or gazepoint
        if (Input.GetKey(KeyCode.W) || (gazePoint.Screen.y > UnityEngine.Screen.height * 0.9)) {
            rotationY += speedY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.S) || (gazePoint.Screen.y < UnityEngine.Screen.height * 0.1)) {
            rotationY -= speedY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.A) || (gazePoint.Screen.x < UnityEngine.Screen.width * 0.2)) {
            rotationX -= speedX * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Input.GetKey(KeyCode.D) || (gazePoint.Screen.x > UnityEngine.Screen.width * 0.8)) {
            rotationX += speedX * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

    }
}