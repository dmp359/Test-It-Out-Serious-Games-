using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StudentController : MonoBehaviour
{

    public GameObject UpArrowPrefab;
    public GameObject DownArrowPrefab;

    public GUIStyle gs;
    private Student student;
    private string status; // for debugging
    private string scoreString; // show score under character

    private float xOffset = 27f, yOffset = 55f;
    private float scoreXOffset = 27f, scoreYOffset = 25f;

    private float indicatorXOffset = 1f;

    private Animator anim;

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        // For debugging
        //GUI.Label(new Rect(position.x - xOffset, Screen.height - position.y + yOffset, 150, 50), status);
        GUI.Label(new Rect(position.x - scoreXOffset, Screen.height - position.y + scoreYOffset * 2, scoreXOffset * 2, scoreYOffset), scoreString, gs);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        student = StudentFactory.GenerateStudent();
    }

    void Update()
    {
    }

    public Student GetStudent()
    {
        return student;
    }

    public void InitLabels()
    {
        if (student == null)
        {
            return;
        }
        status = string.Format("Happiness : {0}\nScore : {1}", student.GetHappinessString(), student.GetScoreString());
        scoreString = student.GetScoreString();
    }

    public void UpdateLabels()
    {
        // Student has lost all happiness
        if (student.GetHappiness() <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .3f);
            anim.speed = 0;
            return;
        }
        // For debugging
        status = string.Format("Happiness : {0}\nScore : {1}", student.GetHappinessString(), student.GetScoreString());
        scoreString = student.GetScoreString();

        CheckEmotion();
        CheckScore();
    }

    private void CheckEmotion()
    {
        // Check if student lost happiness. If so, start crying at the speed of current sad level
        float happinessDelta = (float)student.GetHappinessDelta();
        float happy = (float)student.GetHappiness();

        if (happinessDelta < 0)
        {
            anim.SetTrigger("Cry");
        }
        else if (happinessDelta > 0)
        {
            anim.SetTrigger("Cheer");
        }

        StartCoroutine(BackToIdleAfterTime(5f)); // Play emotion animation for 5 seconds

        if (happinessDelta < 0)
        {
            anim.SetFloat("EmotionDelta", happinessDelta / 100);
        }
        else
        {
            anim.SetFloat("EmotionDelta", happinessDelta / 100 + 0.2f);
        }
        // https://answers.unity.com/questions/993435/adding-tint-to-a-sprite.html
        gameObject.GetComponent<SpriteRenderer>().color = new Color(happy / 100f, happy / 100f, happy / 100f, happy / 100f + .3f);
    }

    IEnumerator BackToIdleAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // If student is under 20 happy, sleep. Otherwise go back to normal idle
        if (student.GetHappiness() <= 20)
        {
            anim.SetTrigger("SleepIdle");
        }
        else if (student.GetHappiness() >= 80)
        {
            anim.SetTrigger("CheerIdle");
        }
        else
        {
            anim.SetTrigger("BackToIdle");
        }
    }

    private void CheckScore()
    {
        double scoreDelta = student.GetScoreDelta();
        if (scoreDelta < 0)
        {
            GameObject down = Instantiate(DownArrowPrefab, new Vector3(transform.position.x + indicatorXOffset, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            down.GetComponent<DeltaTextEffect>().text = student.GetScoreDeltaString();
            down.GetComponent<DeltaTextEffect>().colorToChange = Color.red;
            down.GetComponent<DeltaTextEffect>().movement = -.2f;
        }
        else
        {
            GameObject up = Instantiate(UpArrowPrefab, new Vector3(transform.position.x + indicatorXOffset, transform.position.y, transform.position.z), Quaternion.identity);
            up.GetComponent<DeltaTextEffect>().text = "+" + student.GetScoreDeltaString();
            up.GetComponent<DeltaTextEffect>().colorToChange = Color.green;
            up.GetComponent<DeltaTextEffect>().movement = .2f;
        }
        // TODO: Handle 0?
    }
}
