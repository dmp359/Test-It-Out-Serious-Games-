using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonPlanning : MonoBehaviour
{
    public ProgressBar progressBar;
    public TermManager termManager;
    private StudentController[] students;

    private void UpdateStudentLabels()
    {
        foreach (StudentController studentController in students) {
            studentController.UpdateLabels();
        }
    }

    // Use this for initialization
    void Start () {
        students = GetComponentsInChildren<StudentController>();
    }
	
	// Update is called once per frame
	void Update () {
	}



    public void PracticeTest() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperiencePracticeTest();
        }
        UpdateStudentLabels();
        termManager.SetIsBoardOpen(false);
        progressBar.SetPlanSelected(true);
    }

    public void GroupActivity() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperienceGroupActivity();
        }
        UpdateStudentLabels();
        termManager.SetIsBoardOpen(false);
        progressBar.SetPlanSelected(true);
    }

    public void Lecture() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperienceLecture();
        }
        UpdateStudentLabels();
        termManager.SetIsBoardOpen(false);
        progressBar.SetPlanSelected(true);
    }
}
