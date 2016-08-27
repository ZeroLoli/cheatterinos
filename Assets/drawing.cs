using UnityEngine;
using System.Collections.Generic;

public class Drawing : MonoBehaviour {
    public GameObject pen, desk;
    public Material mat;
    public List<Vector3> nodeList = new List<Vector3>();
    private Vector3 penPos;
    private LineRenderer line;
    private bool penDown = false;

    // Use this for initialization
    void Start() {

    }

    void Update() {
        // Drawing based on pen transform:
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = pen.transform.position;
            myLine.AddComponent<LineRenderer>();
            line = myLine.GetComponent<LineRenderer>();
            line.material = mat;
            line.SetVertexCount(0);
            line.SetColors(Color.black, Color.black);
            line.SetWidth(0.002f, 0.002f);
            line.useWorldSpace = true;
            nodeList = new List<Vector3>();
            line.SetPosition(0, transform.localPosition);
            penDown = true;
        }
        if (penDown) {
            nodeList.Add(pen.transform.position);
            line.SetVertexCount(nodeList.Count);
            line.SetPosition(nodeList.Count-1, (Vector3)nodeList[nodeList.Count -1]);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            penDown = false;
        }
    }
}
