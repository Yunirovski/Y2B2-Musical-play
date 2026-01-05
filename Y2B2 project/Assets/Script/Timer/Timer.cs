using UnityEngine;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI startPauseText;

    private float timeLeft = 0f;
    private bool isRunning = false;

    void Update()
    {
        // Countdown logic
        if (isRunning && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                isRunning = false;
                UpdateStartButtonText();
                TimeEnd();
            }
            UpdateUI();
        }
    }

    // --- Main Control Buttons ---

    public void ToggleStartPause()
    {
        if (timeLeft > 0 || isRunning)
        {
            isRunning = !isRunning;
            UpdateStartButtonText();
        }
    }

    public void ResetTimer()
    {
        isRunning = false;
        timeLeft = 0;
        UpdateStartButtonText();
        UpdateUI();
    }

    // Time Adjustment Buttons 

    public void AddHour() { AdjustTime(3600); }
    public void SubHour() { AdjustTime(-3600); }
    public void AddMin() { AdjustTime(60); }
    public void SubMin() { AdjustTime(-60); }
    public void AddSec() { AdjustTime(1); }
    public void SubSec() { AdjustTime(-1); }

    // Internal logic to change time
    private void AdjustTime(float seconds)
    {
        if (isRunning) return; // Prevent change while running
        timeLeft += seconds;
        if (timeLeft < 0) timeLeft = 0;
        UpdateUI();
    }

    // --- UI Helpers ---

    void UpdateStartButtonText()
    {
        if (startPauseText == null) return;
        startPauseText.text = isRunning ? "Pause" : "Start";
    }
    //Show time in HH:MM:SS format
    void UpdateUI()
    {
        int h = Mathf.FloorToInt(timeLeft / 3600);
        int m = Mathf.FloorToInt((timeLeft % 3600) / 60);
        int s = Mathf.FloorToInt(timeLeft % 60);
        timeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", h, m, s);
    }

    void TimeEnd()
    {
        Debug.Log("TIME END TRIGGERED");
    }
}