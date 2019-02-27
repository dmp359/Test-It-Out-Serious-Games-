using UnityEngine;
using System.Collections;

// Module for doing statistics
public static class StatLibrary {

    // Generates a double within a guassian curve within standard deviation of 1
    // and mean of 0
    public static double NextGaussianDouble()
    {
        double u, v;
        double S;
        do
        {
            u = 2.0 * Random.value - 1.0;
            v = 2.0 * Random.value - 1.0;
            S = u * u + v * v;
        }
        while (S >= 1.0);
        float Sfloat = (float)S;
        double fac = Mathf.Sqrt(-2.0f * Mathf.Log(Sfloat) / Sfloat);
        return u * fac;
    }

    // Returns a random value with standard deviation and average provided
    public static double NextGaussianDoubleWithAvgStd(double std, double avg) {
        return NextGaussianDouble() * std + avg;
    }
}
