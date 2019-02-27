using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressReport : MonoBehaviour {

    public Sprite goodMark;
    public Sprite badMark;
    public Image teacherScoreImage;
    public Text droppedOutNum;
    public Text remainingNum;
    public Text aPlusNum;
    public Text StandingsText;
    public Text HappinessText;
    public Text ScoresText;
    public GameObject classRoom;
    private StudentController[] students;
    private Sprite teacherScore;
    private int droppedOutTotal;
    private int remainingTotal;
    private int aPlusTotal;
    private double happinessScores;
    private double testScores;
    private int totalStudents;
    // Use this for initialization
    void Start () {
        students = classRoom.GetComponentsInChildren<StudentController>();
        totalStudents = students.Length;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void DroppedOut()
    {
        foreach (StudentController controller in students)
        {
            if(controller.GetStudent().GetHappiness() <= 0)
            {
                droppedOutTotal++;
            }
        }
        droppedOutNum.text = droppedOutTotal.ToString();

    }

    void Remaining()
    {
        foreach (StudentController controller in students)
        {
            if (controller.GetStudent().GetHappiness() >= 0)
            {
                happinessScores += controller.GetStudent().GetHappiness();
                testScores += controller.GetStudent().GetScore();
            }
        }
        remainingTotal = totalStudents - droppedOutTotal;
        if((happinessScores/remainingTotal) >= 70)
        {
            HappinessText.text = "Your class is overall pretty happy!";
        }
        else
        {
            HappinessText.text = "Your students ";
        }

        if(testScores/remainingTotal >= 70)
        {
            ScoresText.text = "The overall test scores of your students is high";
        }
        else
        {
            ScoresText.text = "The overall test scores of your students is low";
        }
        remainingNum.text = remainingTotal.ToString();
    }
    
    void APlus()
    {
        foreach (StudentController controller in students)
        {
            // TODO: Tweak this since the score isn't conventional letter grade
            if (controller.GetStudent().GetScore() >= 93 && controller.GetStudent().GetHappiness() >= 0)
            {
                aPlusTotal++;
            }
        }
        aPlusTotal -= droppedOutTotal;
        aPlusNum.text = aPlusTotal.ToString();
    }

    void TeacherScore()
    {
        Debug.Log("HEY" + droppedOutTotal);
        if(droppedOutTotal > 6)
        {
            Debug.Log("HEY YOU FAILED");
            teacherScoreImage.sprite = badMark;
            StandingsText.text = "You are in bad standings with the administration";
        }
        else
        {
            teacherScoreImage.sprite = goodMark;
            StandingsText.text = "You are in good standings with the administration";
        }
    }

    public void ReportCardGeneration()
    {
        Start();
        DroppedOut();
        Remaining();
        APlus();
        TeacherScore();
        droppedOutTotal = 0;
        remainingTotal = 0;
        aPlusTotal = 0;
    }
}
