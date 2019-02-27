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
    public Button continueButton;
    public Button playAgainButton;
    public Text endGameScore;
    private StudentController[] students;
    private int droppedOutTotal;
    private int remainingTotal;
    private int aPlusTotal;
    private double happinessScores;
    private double testScores;
    private int totalStudents;
    private int reportCardNum;
    // Use this for initialization
    void Start () {
        students = classRoom.GetComponentsInChildren<StudentController>();
        totalStudents = students.Length;
        reportCardNum = 0;
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
                testScores += controller.GetStudent().GetScorePercentage();
            }
        }
        int numFailures = 0;
        foreach (StudentController controller in students)
        {
            if (controller.GetStudent().GetScorePercentage() < 100 && controller.GetStudent().GetHappiness() > 0)
            {
                numFailures++;
            }
        }
        remainingTotal = totalStudents - droppedOutTotal;
        double averageHappiness = happinessScores / remainingTotal;
        if (averageHappiness >= 80)
        {
            HappinessText.text = "Your class is extremely happy with your teaching!";
        }
        else if (averageHappiness >= 60)
        {
            HappinessText.text = "Your class is pretty happy!";
        }
        else
        {
            HappinessText.text = "Your students are really sad. Lighten up!";
        }
        double averageTestScore = testScores / remainingTotal;
        if (averageTestScore >= 100 && numFailures < 4)
        {
            ScoresText.text = "Your students' test scores are great!";
        }
        else if (averageTestScore >= 50 && numFailures < 8)
        {
            ScoresText.text = "Your students' test scores could be better!";
        }
        else
        {
            ScoresText.text = "Your students' test scores are horrible!";
        }
        remainingNum.text = remainingTotal.ToString();
    }
    
    void APlus()
    {
        foreach (StudentController controller in students)
        {
            if (controller.GetStudent().GetScorePercentage() >= 100 && controller.GetStudent().GetHappiness() > 0)
            {
                aPlusTotal++;
            }
        }
        aPlusNum.text = aPlusTotal.ToString();
    }

    void TeacherScore()
    {
        int passing = 0; 
        foreach (StudentController controller in students)
        {
            if (controller.GetStudent().GetScorePercentage() >= 100 && controller.GetStudent().GetHappiness() > 0)
            {
                passing++;
            }
        }
        if (droppedOutTotal == 0 && passing >= 10)
        {
            teacherScoreImage.sprite = goodMark;
            StandingsText.text = "The administration is amazed at your performance!";
            endGameScore.text = "See You Next Year!";
        }
        else if (droppedOutTotal <= 2 && droppedOutTotal > 0 && passing >= 8)
        {
            teacherScoreImage.sprite = goodMark;
            StandingsText.text = "You are in good standings with the administration";
            endGameScore.text = "See You Next Year!";

        }
        else if (droppedOutTotal <= 4 && passing < 8)
        {
            teacherScoreImage.sprite = badMark;
            StandingsText.text = "You are in bad standings with the administration";
            endGameScore.text = "Your job is pending review";
        }
        else
        {
            teacherScoreImage.sprite = badMark;
            StandingsText.text = "The administration is fed up you!";
            endGameScore.text = "You're Fired!!";
        }
    }

    public void ReportCardGeneration()
    {
        if(reportCardNum == 1)
        {
            //Debug.Log("AAAAAAAAAAAA");
            EndGameScreen();
        }
        //Debug.Log("ENDGAME" + reportCardNum);
        Start();
        DroppedOut();
        Remaining();
        APlus();
        TeacherScore();
        reportCardNum++;
        happinessScores = 0;
        testScores = 0;
        droppedOutTotal = 0;
        remainingTotal = 0;
        aPlusTotal = 0;
    }

    void EndGameScreen()
    {
        continueButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(true);
        endGameScore.gameObject.SetActive(true);

    }
}
