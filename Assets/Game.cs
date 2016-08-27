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
    public Text text;
    public GameObject debug, clockArm, player, busted, button, box, desk, examPrefab, rowPrefab;
    enum State { STARTING, STARTED, ENDED };
    State state = State.STARTING;
    float duration;
    float deadline;
    public AudioClip bell, clock, toot;
    AudioSource audio;
    public cameraMovement cameramovement;
    public Teacher teacher;
    public List<GameObject> correctCheckboxes = new List<GameObject>();
    public Drawing drawing;
    public int questionAmount = 5;
    public TextAsset questionFile;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        audio = GetComponent<AudioSource>();
        if (Debug.isDebugBuild) // if in editor or debug build show some useful variables
        {
            debug.SetActive(true);
            cameramovement.enabled = true;
        }
        teacher.player = player;
        GenerateExam();
    }

    void FixedUpdate()
    {
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

    GameObject GenerateExam()
    {
        try
        {
            GameObject exam = (GameObject)Instantiate(examPrefab, desk.transform.position + Vector3.up * (desk.transform.GetComponent<Collider>().bounds.extents.y + 0.01f), Quaternion.identity);

            string[] questions = questionFile.text.Split('\n');
            Debug.Log(questions.Length);

            for (int i = 0; i < questionAmount; i++)
            {
                GameObject row = (GameObject)Instantiate(rowPrefab, exam.transform.position + Vector3.forward * (questionAmount / 2) * 0.0001f + Vector3.back * i * 0.15f, Quaternion.identity);

                string question = "";
                for (int j = 0; j < UnityEngine.Random.Range(4, 6); j++)
                    question += (question.Length == 0 ? "-" : " ") + questions[UnityEngine.Random.Range(0, questions.Length - 1)];
                question += "?";
                row.transform.FindChild("Question").GetComponent<Text>().text = question;

                QuestionRow questionrow = row.GetComponent<QuestionRow>();
                questionrow.rightAnswerBox = row.transform.GetComponentsInChildren<BoxCollider>()[UnityEngine.Random.Range(0, 3)].gameObject;
                correctCheckboxes.Add(questionrow.rightAnswerBox);

                row.transform.parent = exam.transform;
            }

            exam.transform.parent = canvas.transform;

            return exam;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return null;
    }

    void CheckExam()
    {
        for (int i = 0; i < drawing.nodeList.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(drawing.nodeList[i], Vector3.down, out hit))
            {
                if (correctCheckboxes.Contains(hit.transform.gameObject))
                    correctCheckboxes.Remove(hit.transform.gameObject);
            }
        }
    }
}
