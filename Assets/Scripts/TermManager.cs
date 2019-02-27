using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermManager : MonoBehaviour {

    public GameObject popUpLesson;
    public ProgressBar progressBar;
    public GameObject lessonPlanButton;
    public GameObject classroom;
    private bool isBoardOpen;
    private GameObject[] students;
    // Use this for initialization
    void Start () {
        students = GameObject.FindGameObjectsWithTag("Student");
        progressBar.SetPlanSelected(false);
        isBoardOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
        OpenLessons();
	}

    void OpenLessons()
    {
        if (isBoardOpen)
        {
            //classroom.SetActive(false);
            lessonPlanButton.SetActive(false);
            popUpLesson.SetActive(true);
        }
        else
        {
            //classroom.SetActive(true);
            foreach (GameObject student in students)
            {
                student.GetComponent<StudentController>().enabled = true;
            }
            popUpLesson.SetActive(false);
        }
    }

    public void OpenChalkboard()
    {
        foreach(GameObject student in students)
        {
            student.GetComponent<StudentController>().enabled = false;
        }
        isBoardOpen = true;
        //progressBar.SetPlanSelected(true);
    }

    public void SetIsBoardOpen(bool _isBoardOpen)
    {
        isBoardOpen = _isBoardOpen;
    }

    public bool GetIsBoardOpen()
    {
        return isBoardOpen;
    }
}
