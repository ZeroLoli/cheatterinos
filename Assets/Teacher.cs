using UnityEngine;

/// <summary>
/// Teacher class
/// </summary>
public class Teacher : MonoBehaviour
{
    public float fov = 15;
    public float radar = 180;
    public float speed = 10; // degrees/s
    public int direction = 1;
    public bool spotted = false;
    public GameObject player;
    public float vlimit, hlimit;

    void FixedUpdate()
    {
        if (Vector3.Angle(transform.forward, transform.parent.forward) >= radar/2) // swap directions
        {
            direction *= -1;
            transform.Rotate(Vector3.up * direction * speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up * direction * speed * Time.deltaTime); // rotate the teach head
        }
        if (Vector3.Angle(transform.forward, transform.parent.forward) <= fov && Vector3.Angle(player.transform.forward, Vector3.forward) >= hlimit) // teach has caught player cheating
            spotted = true;
    }
}
