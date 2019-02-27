using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTime : MonoBehaviour {

    [SerializeField] private float time;

	void Start () {
        time = 5f;
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
