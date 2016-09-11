using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// Main class
/// </summary>
public class Game : MonoBehaviour
{
    public Canvas canvas;
    public Text text, scoreText;
    public GameObject debug, clockArm, player, busted, button, box, desk, examPrefab, rowPrefab, crossMark, infoPanel;
    enum State { STARTING, STARTED, ENDED };
    State state = State.STARTING;
    float duration;
    float deadline;
    public AudioClip bell, clock, toot;
    AudioSource audio;
    public cameraMovement cameramovement;
    public Teacher teacher;
    public List<GameObject> neighborDesks = new List<GameObject>();
    public List<GameObject> neighborCanvas = new List<GameObject>();
    private List<GameObject> checkboxes = new List<GameObject>();
    private int wrongAnswers;
    public List<GameObject> correctCheckboxes = new List<GameObject>();
    public Drawing drawing;
    public int questionAmount = 5;
    public TextAsset questionFile;

    void Start()
    {
        drawing.enabled = false;
        cameramovement.enabled = false;
        Application.runInBackground = true;
        Cursor.lockState = CursorLockMode.Confined;
        audio = GetComponent<AudioSource>();
        teacher.player = player;
        GenerateExam(); // create the player exam TODO also create the neighbour exams, this function returns the exam, toggle the correct answers on somehow
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (state == State.STARTED) {
            if (Time.time >= deadline)
            {
                audio.Stop();
                audio.PlayOneShot(bell);
                state = State.ENDED;
                cameramovement.enabled = false;
                teacher.enabled = false;
                teacher.transform.Rotate(Vector3.up * Vector3.Angle(teacher.transform.forward, teacher.transform.parent.forward));
                CheckExam();
            }
            if (teacher.spotted)
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
        Cursor.lockState = CursorLockMode.Locked;
        duration = float.Parse(text.text) * 60;
        deadline = Time.time + duration;
        debug.SetActive(false);
        button.SetActive(false);
        cameramovement.enabled = true;
        drawing.enabled = true;
        teacher.enabled = true;
        audio.clip = clock;
        audio.Play();
        state = State.STARTED;
    }

    /// <summary>
    /// Generates an exam and returns the exam
    /// </summary>
    /// <returns></returns>
    GameObject GenerateExam()
    {
        // empty parent object for positioning purposes (also contains a broken checkbox header, remove those and attach separately)
        GameObject exam = (GameObject)Instantiate(examPrefab, desk.transform.position + Vector3.up * /*(desk.transform.GetComponent<Collider>().bounds.extents.y + 0.01f)*/ 0.022f, Quaternion.identity);
                                                                                        //new Vector3(0, desk.transform.localScale.z / 2, 0), Quaternion.identity); 
        //load a bunch of random words from a text file and split on newline
        string[] questions = questionFile.text.Split('\n');

        // iterate through the number of questions wanted, layout optimized for 5
        for (int i = 0; i < questionAmount; i++)
        {
            // row object, contains three checkboxes and a question text field
            GameObject row = (GameObject)Instantiate(rowPrefab, exam.transform.position + Vector3.forward * (questionAmount / 2) * 0.0001f + Vector3.back * i * 0.15f, Quaternion.identity);

            // make up a question
            string question = "";
            for (int j = 0; j < UnityEngine.Random.Range(4, 6); j++) // 4-5 random words, 7 was too much with current words
                question += (question.Length == 0 ? "-" : " ") + questions[UnityEngine.Random.Range(0, questions.Length - 1)];
            question += "?"; // append a question mark for good measure
            row.transform.FindChild("Question").GetComponent<Text>().text = question; // set the text for the object

            // randomize which checkbox is correct TODO mark the correct answers on neighbour exams
            QuestionRow questionrow = row.GetComponent<QuestionRow>();
            for (int j = 0; j < row.transform.GetComponentsInChildren<BoxCollider>().Length; j++)
            {
                checkboxes.Add(row.transform.GetComponentsInChildren<BoxCollider>()[j].gameObject);
            }
            questionrow.rightAnswerBox = row.transform.GetComponentsInChildren<BoxCollider>()[UnityEngine.Random.Range(0, 3)].gameObject;
            correctCheckboxes.Add(questionrow.rightAnswerBox);
            checkboxes.Remove(questionrow.rightAnswerBox);

            row.transform.parent = exam.transform;
        }
       wrongAnswers = checkboxes.Count;
        // Generate copies of exam with correct answers marked on neighboring desks
        for (int i = 0; i < neighborDesks.Count; i++) {
            GameObject neighborExam = (GameObject)Instantiate(exam, neighborDesks[i].transform.position + Vector3.up * 0.025f, Quaternion.identity);
            neighborExam.transform.parent = neighborCanvas[i].transform;
            for (int j = 0; j < correctCheckboxes.Count; j++) {
                GameObject correctAnswer = (GameObject)Instantiate(crossMark, new Vector3(neighborDesks[i].transform.position.x, 0,  0) + correctCheckboxes[j].transform.position + Vector3.up * 0.001f, Quaternion.identity);
            }
        }
        exam.transform.parent = canvas.transform;
        
        return exam;
    }

    /// <summary>
    /// Example of how to check if something was checked TODO completely, really
    /// </summary>
    void CheckExam()
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("checking exam");
        Debug.Log(correctCheckboxes.Count);
        for (int i = 0; i < drawing.allNodes.Count; i++)
        {
            // make a raycast from each node, if you hit a box you are in it. and the correct answer box is removed from a collection dunno why.
            RaycastHit hit;
            Debug.DrawRay(drawing.allNodes[i], Vector3.down * 10);
            if (Physics.Raycast(drawing.allNodes[i]+Vector3.up * 0.2f, Vector3.down * 0.5f, out hit)) {
                Debug.Log(hit.transform.gameObject);
                if (correctCheckboxes.Contains(hit.transform.gameObject))
                {
                    Debug.Log("contained");
                    correctCheckboxes.Remove(hit.transform.gameObject);
                }
                if (checkboxes.Contains(hit.transform.gameObject))
                {
                    checkboxes.Remove(hit.transform.gameObject);
                }
            }
        }
        Debug.Log(correctCheckboxes.Count);
        int score = questionAmount - correctCheckboxes.Count - (wrongAnswers-checkboxes.Count);
        Debug.Log("your score: " + score);
        scoreText.text =  "\nCorrect answers: " + (questionAmount - correctCheckboxes.Count)
            + "\nWrong answers: " + (wrongAnswers-checkboxes.Count) + "\n- - - - -\nYour score: " + score
            + "\n\nR to restart or Esc to quit";
        infoPanel.SetActive(true);
        
        //if (correctCheckboxes.Count == 0) {
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //}
    }
}