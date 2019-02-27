using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progress;
    public Text progressText;
    public ProgressReport progressReport;
    public TermManager termManager;
    private int stopValue;
    private int floatToInt;
    private bool displayCard;
    private bool midterm;
    private bool endterm;
    private bool isCardOff;
    private bool planSelected;
    private string[] months;
    private int monthNum;
    void Start()
    {
        displayCard = false;
        planSelected = false;
        isCardOff = false;
        months = new string[] { "August", "September", "October", "November", "December", "January", "February", "March", "April", "May", "June"};
        monthNum = 0;
        stopValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        CreateProgressBar();
        OpenProgressReport();
    }

    void CreateProgressBar()
    {
        floatToInt = Mathf.RoundToInt(progress.fillAmount * 100);
        progressText.text = months[monthNum];
        if(planSelected)
        {
            progress.fillAmount += Time.deltaTime * 0.019f;
        }

        //Debug.Log(Mathf.RoundToInt(progress.fillAmount * 100));
        //Debug.Log("Mod" + Mathf.RoundToInt(progress.fillAmount * 100) % 10);
        if (floatToInt % 10 == 0 && floatToInt != stopValue)
        {
            //Debug.Log("HIT");
            stopValue = floatToInt;
            planSelected = false;
            termManager.lessonPlanButton.SetActive(true);
            monthNum++;
            //Time.timeScale = 0;
        }
    }
    void OpenProgressReport()
    {
        floatToInt = Mathf.RoundToInt(progress.fillAmount * 100);
        if (floatToInt == 50 && !midterm)
        {
            midterm = true;
            displayCard = true;
            isCardOff = true;
        }
        if(floatToInt == 100 && !endterm)
        {
            endterm = true;
            displayCard = true;
            isCardOff = true;
        }
        if(displayCard)
        {
            progressReport.gameObject.SetActive(true);
            if (isCardOff)
            {
                progressReport.GetComponent<ProgressReport>().ReportCardGeneration();
                isCardOff = false;
            }
            progressReport.classRoom.SetActive(false);
            planSelected = false;
            termManager.lessonPlanButton.SetActive(false);
            //displayCard = false;
        }
        else
        {
            progressReport.gameObject.SetActive(false);
            progressReport.classRoom.SetActive(true);
        }

    }

    public void CloseProgressReport()
    {
        //Debug.Log("CLICKED");
        displayCard = false;
        planSelected = false;
        isCardOff = true;
        termManager.lessonPlanButton.SetActive(true);
    }

    public void SetPlanSelected(bool _planSelected)
    {
        planSelected = _planSelected;
    }

    public bool GetPlanSelected()
    {
        return planSelected;
    }
}
