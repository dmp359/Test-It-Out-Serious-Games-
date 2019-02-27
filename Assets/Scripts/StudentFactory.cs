using UnityEngine;
using System.Collections;

public static class StudentFactory
{

    private static double AVERAGE_GRADE = 75;
    private static double GRADE_STD = 10;

    private static double AVERAGE_HAPPINESS = 50;
    private static double HAPPINESS_STD = 10;

    private static double AVERAGE_STRESS = 0;
    private static double STRESS_STD = 20;


    // Return a new student with random stats in an appropriate range
    public static Student GenerateStudent() {
        double grade = StatLibrary.NextGaussianDoubleWithAvgStd(GRADE_STD, AVERAGE_GRADE);
        double happiness = StatLibrary.NextGaussianDoubleWithAvgStd(HAPPINESS_STD, AVERAGE_HAPPINESS);
        double stress = StatLibrary.NextGaussianDoubleWithAvgStd(STRESS_STD, AVERAGE_STRESS);
        //Debug.Log("Making a student with grade " + grade + " and happiness " + happiness + " and stress " + stress);
        return new Student(grade, happiness, stress);
    }

    // Create a class of random students
    public static Student[] GenerateClass(int size) {
        Student[] students = new Student[size];
        for (int i = 0; i < size; i++) {
            students[i] = GenerateStudent();
        }
        return students;
    }

    public static void Simulate(Student[] students)
    {
        // Foreach student
        // Random lesson
        // 10 turns
        int randomLesson = 0;
        double[] scores = new double[students.Length];
        int count = 0;
        foreach (Student student in students)
        {
            // Simulate a game with 10 turns
            for (int i = 0; i < 10; i++)
            {
                if (student.GetHappiness() < 0)
                {
                    break;
                }
                randomLesson = Random.Range(0, 2);
                if (randomLesson <= 0)
                {
                    student.ExperiencePracticeTest();
                }
                else if (randomLesson == 1)
                {
                    student.ExperienceGroupActivity();
                }
                else
                {
                    student.ExperienceLecture();
                }

            }
            scores[count] = student.GetScore();
            count++;
        }
        double sum = 0, average;
        for (var i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        average = sum / scores.Length;
        Debug.Log(average);
    }
}
