using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Game : MonoBehaviour {
    public Canvas canvas;
    public GameObject debugCanvas;

    void Start(){
        if (Debug.isDebugBuild)
        {
            debugCanvas.SetActive(true);
        }
    }
    /// <summary>
    /// Starts the game counters and enables player controls
    /// </summary>
    public void startGame()
    {
        canvas.enabled = false;
    }
}
