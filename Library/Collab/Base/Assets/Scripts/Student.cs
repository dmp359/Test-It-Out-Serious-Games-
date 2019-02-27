using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student {

    private double score; // 0 through 100
    private double happiness; // 0 through 100

    // Some hidden factors and weights to affect learning
    private double baseGrade; // Correlated to how their scores will be good no matter what
    private double baseHappiness; // Correlated to how their happiness will be good no matter what
    private double stress; // How exaggerated happiness/scores will skew in either direction

    public Student() {

    }

    public Student(double _baseGrade, double _baseHappiness, double _stress) {
        baseGrade = _baseGrade;
        baseHappiness = _baseHappiness;
        stress = _stress;

    }

    public void SetScore(double _score)
    {
        score = _score;
    }
    public double GetScore()
    {
        return score;
    }
    public void SetHappiness(double _happiness)
    {
        happiness = _happiness;
    }
    public double GetHappiness()
    {
        return happiness;
    }

    public void SetStress(double _stress) {
        stress = _stress;
    }

    public double GetStress() {
        return stress;
    }

    // Simulate a practice test on this student
    public void ExperiencePracticeTest() {

    }

    // Simulate a fun, group activity on this student
    public void ExperienceGroupActivity() {

    }


    // Simulate a lecture on this student
    public void ExperienceLecture() {

    }


}
