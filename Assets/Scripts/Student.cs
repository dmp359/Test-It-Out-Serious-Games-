using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student {

    private List<double> scoreHistory; // 0 through 100, size of n months
    private double score; // current score;
    private List<double> happinessHistory; // 0 through 100, size of n months
    private double happiness; // current happiness;

    // Some hidden factors and weights to affect learning
    private double baseGrade; // Correlated to how their scores will be good no matter what
    private double baseHappiness; // Correlated to how their happiness will be good no matter what
    private double stress; // How exaggerated happiness/scores will skew in either direction

    private double GROUP_ENJOYMENT_FACTOR = 10f;
    private double TEST_EXPERIENCE_FACTOR = 20f;
    private double LECTURE_ENJOYMENT_FACTOR = 10f;
    private int GOAL_SCORE = 150;
    public Student() {
    }

    public Student(double _baseGrade, double _baseHappiness, double _stress) {
        scoreHistory = new List<double>();
        happinessHistory = new List<double>();

        baseGrade = _baseGrade;
        baseHappiness = _baseHappiness;
        happiness = baseHappiness;
        score = baseGrade;
        stress = _stress;
        SaveScore();
        SaveHappiness();
    }

    public void SaveScore() {
        scoreHistory.Add(score);
    }

    public void SaveHappiness() {
        happinessHistory.Add(happiness);
    }

    public void SetScore(double _score)
    {
        if (_score < 0) {
            score = 0;
            SaveScore();
            return;
        }
        score = _score;
        SaveScore();
    }

    public double GetScore()
    {
        return score;
    }

    public string GetScoreString() {
        return GetScorePercentage().ToString() + "%";
    }

    public string GetScoreDeltaString()
    {
        return Mathf.Round((float)GetScoreDelta() / GOAL_SCORE * 100).ToString() + "%";
    }

    public int GetScorePercentage() {
        return (int)Mathf.Round((float)GetScore() / GOAL_SCORE * 100);
    }

    public void SetHappiness(double _happiness)
    {
        happiness = _happiness;
        SaveHappiness();
    }

    public double GetHappiness()
    {
        return happiness;
    }

    public string GetHappinessString() {
        return Mathf.Round((float)GetHappiness()).ToString();
    }

    public string GetHappinessDeltaString() {
        return Mathf.Round((float)GetHappinessDelta()).ToString();
    }

    public void SetStress(double _stress) {
        stress = _stress;
    }

    public double GetStress() {
        return stress;
    }

    public string GetStressString() {
        return Mathf.Round((float)GetStress()).ToString();
    }
    
    // Simulate a practice test on this student
    // Lowers happiness but always increases scores drastically
    public void ExperiencePracticeTest() {
        if (happiness <= 0) {
            return;
        }
        double happinessDelta = Mathf.Abs((float) stress) + Random.value * TEST_EXPERIENCE_FACTOR;
        double potentialHappiness = (GetHappiness() - happinessDelta > 0) ? (GetHappiness() - happinessDelta) : 0;
        SetHappiness(potentialHappiness);

        double scoreDelta = Mathf.Abs((float) stress) + Random.value * TEST_EXPERIENCE_FACTOR;
        SetScore(GetScore() + scoreDelta);
    }

    // Simulate a fun, group activity on this student
    // Increase happiness but randomly affect test scores. Bias to lowering them.
    public void ExperienceGroupActivity() {
        if (happiness <= 0) {
            return;
        }
        double happinessDelta = Mathf.Abs((float)stress) + Random.value * GROUP_ENJOYMENT_FACTOR;
        SetHappiness(GetHappiness() + happinessDelta);

        double scoreDelta = stress + Random.value * GROUP_ENJOYMENT_FACTOR;
        SetScore(GetScore() + scoreDelta);
    }


    // Simulate a lecture on this student
    // Random happiness and increase test scores a little
    public void ExperienceLecture() {
        if (happiness <= 0) {
            return;
        }
        float randomVal = Random.value;
        double h;
        double happinessDelta = Mathf.Abs((float)stress) + randomVal * LECTURE_ENJOYMENT_FACTOR;
        double s = Mathf.Abs((float)stress) + Random.value * LECTURE_ENJOYMENT_FACTOR;

        h = GetHappiness() + happinessDelta;

        // Nerfing lecture by making it a 25% chance to half happiness (i.e. boredom?)
        // and also lower test score
        if (randomVal <= 0.25f) {
            if (GetHappiness() > 10) {
                h /= 2;
                s *= -1;
            } else {
                h = 0;
            }
        }

        SetHappiness(h);

        SetScore(GetScore() + s);
    }

    // Returns the difference between current happiness and happiness from last turn
    public double GetHappinessDelta() {
        return happinessHistory.Count < 2 ? happiness : happiness - happinessHistory[happinessHistory.Count - 2];
    }

    public double GetScoreDelta() {
        return scoreHistory.Count < 2 ? score : score - scoreHistory[scoreHistory.Count - 2];
    }
}
