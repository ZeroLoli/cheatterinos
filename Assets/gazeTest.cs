using UnityEngine;
using System.Collections;

public class gazeTest : MonoBehaviour {
    private GazeAwareComponent _gazeAware;
	// Use this for initialization
	void Start () {
        _gazeAware = GetComponent<GazeAwareComponent>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_gazeAware.HasGaze)
        {
            transform.localScale += new Vector3(0.1f,0.1f,0.1f) * Time.deltaTime;
            transform.Rotate(new Vector3(5, 5, 0));
        }
	}
}
