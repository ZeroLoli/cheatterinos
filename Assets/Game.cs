using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Game : MonoBehaviour {
    public Canvas canvas;
    public Text text;
    public GameObject debugCanvas, clockArm;
    enum State {STARTING, STARTED, ENDED};
    State state = State.STARTING;
    public bool isStarted = false;
    float duration;
    float deadline;

    void Start(){
        if (Debug.isDebugBuild)
        {
            debugCanvas.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (state == State.STARTED)
        {
            if (Time.time >= deadline)
                state = State.ENDED;
            else
                clockArm.transform.Rotate(Vector3.up * (Time.deltaTime / duration * 360), Space.Self);
        }
    }

    /// <summary>
    /// Starts the game counters and enables player controls
    /// </summary>
    public void startGame()
    {
        duration = float.Parse(text.text)*60;
        deadline = Time.time + duration;
        canvas.enabled = false;
        debugCanvas.SetActive(false);
        state = State.STARTED;
    }
}
