using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonPlanning : MonoBehaviour
{
    public ProgressBar progressBar;
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
        foreach (StudentController studentController in students) {
            studentController.InitLabels();
        }
    }
	
	// Update is called once per frame
	void Update () {
	}



    public void PracticeTest() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperiencePracticeTest();
        }
        UpdateStudentLabels();
        progressBar.SetPlanSelected(true);
    }

    public void GroupActivity() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperienceGroupActivity();
        }
        UpdateStudentLabels();
        progressBar.SetPlanSelected(true);
    }

    public void Lecture() {
        foreach (StudentController controller in students) {
            controller.GetStudent().ExperienceLecture();
        }
        UpdateStudentLabels();
        progressBar.SetPlanSelected(true);
    }
}
