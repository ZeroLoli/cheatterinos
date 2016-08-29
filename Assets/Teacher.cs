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

    void FixedUpdate() {
        
        if (Vector3.Angle(transform.right, transform.parent.right) >= radar/2) { // swap directions
            direction *= -1;
            transform.Rotate(0, 0, Time.deltaTime * direction * speed);
        }
        else {
            transform.Rotate(0, 0, Time.deltaTime * direction * speed); // rotate the teach head
        }
        if (Vector3.Angle(transform.right, transform.parent.right) <= fov && Vector3.Angle(new Vector3(player.transform.forward.x, 0, player.transform.forward.z), Vector3.forward) >= hlimit) { // teach has caught player cheating
            spotted = true;
        }
    }
}
