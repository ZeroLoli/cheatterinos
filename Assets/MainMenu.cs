using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    // Jump to scene or close the executable
    public void sceneSelect(int scene) {
        if (scene == 666) {
            Application.Quit();   
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
