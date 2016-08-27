using UnityEngine;
using System.Collections;

public class drawing : MonoBehaviour
{
    public GameObject pen;
    public Material mat;
    public ArrayList nodeList = new ArrayList();
    private Vector3 penPos;
    private LineRenderer line;
    private bool penDown = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*	    if (Input.GetKey(KeyCode.Mouse0)) {
                    GameObject drawLine = new GameObject();
                    drawLine.transform.position = transform.position;
                    drawLine.AddComponent<LineRenderer>();
                    LineRenderer lr = drawLine.GetComponent<LineRenderer>();
                    lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
                    lr.SetColors(Color.black, Color.black);
                    lr.SetWidth(1.1f, 1.1f);
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, transform.position);
                }*/
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
            nodeList = new ArrayList();
            line.SetPosition(0, transform.localPosition);
            penDown = true;
        }
        if (penDown)
        {
            nodeList.Add(pen.transform.position);
            line.SetVertexCount(nodeList.Count);
            line.SetPosition(nodeList.Count-1, (Vector3)nodeList[nodeList.Count -1]);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            penDown = false;
        }
    }
}
