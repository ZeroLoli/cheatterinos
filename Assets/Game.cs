using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main class
/// </summary>
public class Game : MonoBehaviour
{
    public Canvas canvas;
    public Text text;
    public GameObject debug, clockArm, player, busted, button;
    enum State { STARTING, STARTED, ENDED };
    State state = State.STARTING;
    public bool isStarted = false;
    float duration;
    float deadline;
    public AudioClip bell, clock, toot;
    AudioSource audio;
    public cameraMovement cameramovement;
    public Teacher teacher;
    public float startTimer;
    private GazeAwareComponent buttonGaze;
    private bool buttonStare;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (Debug.isDebugBuild) // if in editor or debug build show some useful variables
        {
            debug.SetActive(true);
            cameramovement.enabled = true;
        }
        teacher.player = player;
        buttonGaze = button.GetComponent<GazeAwareComponent>();
    }

    void FixedUpdate()
    {
        if (buttonGaze.HasGaze && !buttonStare) {
            buttonStare = true;
            startTimer = Time.time;
        }
        if (!buttonGaze.HasGaze) {
            buttonStare = false;
        }
        if (buttonGaze.HasGaze && Time.time - startTimer >= 0.5f) {
            startGame();
        }

            if (state == State.STARTED)
        {
            if (Time.time >= deadline)
            {
                audio.Stop();
                audio.PlayOneShot(bell);
                state = State.ENDED;
                cameramovement.enabled = false;
                teacher.enabled = false;
                teacher.transform.Rotate(Vector3.up * Vector3.Angle(teacher.transform.forward, teacher.transform.parent.forward));
            }
            if(teacher.spotted)
            {
                state = State.ENDED;
                cameramovement.enabled = false;
                teacher.enabled = false;
                audio.Stop();
                audio.PlayOneShot(toot);
                busted.SetActive(true);
            }
            else
                clockArm.transform.Rotate(Vector3.up * (Time.deltaTime / duration * 360), Space.Self);
        }
    }

    /// <summary>
    /// Starts the game counters and enables player controls
    /// </summary>
    public void startGame()
    {
        duration = float.Parse(text.text) * 60;
        deadline = Time.time + duration;
        debug.SetActive(false);
        button.SetActive(false);
        cameramovement.enabled = true;
        teacher.enabled = true;
        audio.clip = clock;
        audio.Play();
        state = State.STARTED;
    }
}
