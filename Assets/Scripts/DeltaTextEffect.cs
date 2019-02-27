using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTextEffect : MonoBehaviour {

    public GUIStyle gs;
    public string text;
    public Color colorToChange;
    public float movement;

    private float xOffset = -25f, yOffset = 55f;
    private float labelPosX;
    private float labelPosY;

    public void OnGUI() {
        GUI.Label(new Rect(labelPosX, labelPosY, 50, 50), text, gs);
    }

    void Start () {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        labelPosX = position.x + xOffset;
        labelPosY = Screen.height - position.y + yOffset;
        gs.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update () {
        gs.normal.textColor = colorToChange;
        //gs.normal.textColor = Color.Lerp(Color.white, colorToChange, Mathf.PingPong(Time.time, .25f));
        labelPosY -= movement;
    }

    //public IEnumerator FadeTextToFullAlpha(float t)
    //{
    //    Color i = new Color(gs.normal.textColor.r, gs.normal.textColor.g, gs.normal.textColor.b, 0);
    //    while (i.a < 1.0f)
    //    {
    //        i = new Color(i.r, i.g, i.b, i.a + (Time.deltaTime / t));
    //        gs.normal.textColor = i;
    //        yield return null;
    //    }
    //}

    //public IEnumerator FadeTextToZeroAlpha(float t)
    //{
    //    Color i = new Color(gs.normal.textColor.r, gs.normal.textColor.g, gs.normal.textColor.b, 1);
    //    while (i.a > 0.0f)
    //    {
    //        i = new Color(i.r, i.g, i.b, i.a + (Time.deltaTime / t));
    //        gs.normal.textColor = i;
    //        yield return null;
    //    }
    //}
}
