using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsMeter : MonoBehaviour
{
    public Text Text;

    private int frameCount = 0;
    private float nextUpdate = 0.0f;
    private float fps = 0.0f;
    private int updateRate = 4; // 4 updates per sec.

    public void Start()
    {
        nextUpdate = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (Time.time > nextUpdate)
        {
            nextUpdate += 1.0f / updateRate;
            fps = frameCount * updateRate;
            frameCount = 0;

            this.Text.text = "FPS: " + fps;
        }
    }
}