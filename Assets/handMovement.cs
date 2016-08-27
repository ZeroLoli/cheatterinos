using UnityEngine;
using System.Collections;

public class handMovement : MonoBehaviour
{
    public Material mat;
    public GameObject paper, pen, desk;
    public ArrayList nodeList;

    public float minX;
    public float maxX = 0.4f;

    public float minZ = -0.2f;
    public float maxZ = 0.4f;

    public float speedX = 3.0f;
    public float speedZ = 3.0f;

    private float maxXZ;
    private float maxZX;

    private bool drawing;
    private LineRenderer line;
    float posX, posY, posZ;

    // Use this for initialization
    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        minX = desk.transform.position.x - 0.24f;
        maxX = desk.transform.position.x + 0.54f;
        minZ = desk.transform.position.z - 0.54f;
        maxZ = desk.transform.position.z + 0.36f;


    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Mouse0) {
            handY -= speedY * Time.deltaTime;
            handY = Mathf.Clamp(handY, minY, maxY);
            transform.Translate(Vector3.down * Time.deltaTime, Space.World);
        }*/
        //else if (penY <= 0.7f) { 
        //    penY += speedY * Time.deltaTime;
        //    transform.Translate(new Vector3(0, 0, penY));
        //}

        // Hand movement (Y), drawing

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*    GameObject myLine = new GameObject();
                myLine.AddComponent<LineRenderer>();
                LineRenderer line = myLine.GetComponent<LineRenderer>();
                line.material = mat;
                line.SetColors(Color.black, Color.black);
                line.SetWidth(0.01f, 0.01f);
                nodeList = new ArrayList();
                line.SetVertexCount(0);
                nodeList.Add(pen.transform.position);
                drawing = true;*/
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = new Vector3(posX, posY - 0.005f, posZ);
            /*GL.PushMatrix();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(new Vector3(posX, posY, posZ));
            GL.Vertex(new Vector3(posX, posY, posZ));
            GL.End();
            GL.PopMatrix();*/
            //paper.AddComponent<LineRenderer>();
            //line = pen.GetComponent<LineRenderer>();
            //DrawLine(pen.transform.position, new Vector3(pen.transform.position.x, paper.transform.position.y, pen.transform.position.z) /*+ new Vector3(0,0,0.01f)*/, Color.black, mat);
        }
        /*if (drawing) {
            nodeList.Add(pen.transform.position);
            line.SetVertexCount(nodeList.Count);
            line.SetPosition(nodeList.Count - 1, (Vector3)nodeList[nodeList.Count - 1]);
        }*/
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.position = new Vector3(posX, posY, posZ);
            //    drawing = false;
        }

        // Hand movement (X+Z)
        posX += Input.GetAxis("Mouse X") * speedX * Time.deltaTime;
        posZ += Input.GetAxis("Mouse Y") * speedZ * Time.deltaTime;
        // Fun times vvvvvvvvv
        //maxXZ = maxX - Mathf.Log(posZ);
        //maxZX = maxZ - Mathf.Log(posX);
        // End of fun ^^^^^^^^
        //maxXZ = maxX - transform.localPosition.x * transform.localPosition.z;
        //maxZX = maxZ - transform.localPosition.x * transform.localPosition.z;
        //maxXZ = Mathf.Clamp(maxXZ, minX, maxX);
        //maxZX = Mathf.Clamp(maxZX, minZ, maxZ);
        //posX = Mathf.Clamp(posX, minX, maxXZ);
        //posZ = Mathf.Clamp(posZ, minZ, maxZX);
        posX = Mathf.Clamp(posX, minX, maxX);
        posZ = Mathf.Clamp(posZ, minZ, maxZ);

        transform.position = new Vector3(posX, transform.position.y, posZ);
    }

    //void DrawLine(Vector3 start, Vector3 end, Color color, Material mat) {
    //    GameObject drawLine = new GameObject();
    //    drawLine.transform.position = start;
    //    drawLine.AddComponent<LineRenderer>();
    //    LineRenderer lr = drawLine.GetComponent<LineRenderer>();
    //    lr.material = mat; //new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
    //    //lr.lightProbeUsage = 0;
    //    lr.SetColors(color, color);
    //    lr.SetWidth(0.01f, 0.01f);
    //    lr.SetPosition(0, start);
    //    lr.SetPosition(1, end);
    //}
}
