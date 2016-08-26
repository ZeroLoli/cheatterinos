using UnityEngine;
using System.Collections;

public class Teacher : MonoBehaviour
{
    public Canvas canvas;
    public float fov = 15;
    public float radar = 180;
    public float speed = 10; // degrees/s

    public int direction = 1;

    void FixedUpdate()
    {
        if (Vector3.Angle(transform.forward, transform.parent.forward) >= radar/2)
        {
            direction *= -1;
            transform.Rotate(Vector3.up * direction * speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up * direction * speed * Time.deltaTime);
        }
    }
}
