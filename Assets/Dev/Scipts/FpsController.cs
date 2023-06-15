using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    #region Variables for Calculate FPS
    public float pollingTime;
    private float time;
    private int frameCount;
    private TMPro.TextMeshProUGUI fpsText;
    #endregion

    #region Variables for Limit FPS
    public enum Limits
    {
        noLimit = 0, 
        limit30 = 30,
        limit60 = 60,
        limit120 = 120,
        limit240 = 240,
    }

    public Limits limit;
    #endregion

    private void Awake()
    {
        Application.targetFrameRate = (int)limit; //Limit Fps
    }

    void Start()
    {
        fpsText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        CalculateFPS();
    }

    private void CalculateFPS()
    {
        time += Time.deltaTime;
        frameCount++;
        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " FPS";
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
