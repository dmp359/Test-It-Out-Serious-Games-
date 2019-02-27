using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progress;
    public Text progressText;
    public GameObject progressReport;
    private int stopValue;
    private int floatToInt;
    private bool isCardOff;
    void Start()
    {
        isCardOff = true;
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
        progressText.text = floatToInt.ToString() + "%";
        progress.fillAmount += Time.deltaTime * 0.019f;
        Debug.Log(Mathf.RoundToInt(progress.fillAmount * 100));
        Debug.Log("Mod" + Mathf.RoundToInt(progress.fillAmount * 100) % 10);
        if (floatToInt % 10 == 0 && floatToInt != stopValue)
        {
            Debug.Log("HIT");
            stopValue = floatToInt;
            Time.timeScale = 0;
        }
    }
    void OpenProgressReport()
    {
        floatToInt = Mathf.RoundToInt(progress.fillAmount * 100);
        if (floatToInt == 20 || floatToInt == 30)
        {
            progressReport.SetActive(true);
            if(isCardOff)
            {
                progressReport.GetComponent<ProgressReport>().ReportCardGeneration();
                isCardOff = false;
            }
            progressReport.GetComponent<ProgressReport>().classRoom.SetActive(false);
        }
    }

    public void CloseProgressReport()
    {
        Time.timeScale = 1;
        progressReport.SetActive(false);
        progressReport.GetComponent<ProgressReport>().classRoom.SetActive(true);
        isCardOff = true;
    }
}
