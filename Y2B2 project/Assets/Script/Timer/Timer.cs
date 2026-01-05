using UnityEngine;
using TMPro; 

public class SimpleTimer : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    private float timeLeft = 0f;
    private bool isPaused = true;

    // Run every frame
    void Update()
    {
        if (!isPaused && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                isPaused = true;
                TimeEnd(); // Trigger end
            }
            UpdateUI();
        }
    }

    // --- Button Functions ---

    public void StartTimer() { isPaused = false; }
    public void PauseTimer() { isPaused = true; }
    public void ResetTimer() { isPaused = true; timeLeft = 0; UpdateUI(); }

    // Change time (h=hours, m=minutes, s=seconds)
    public void AddTime(string unit, int amount)
    {
        if (unit == "h") timeLeft += amount * 3600;
        if (unit == "m") timeLeft += amount * 60;
        if (unit == "s") timeLeft += amount;

        if (timeLeft < 0) timeLeft = 0;
        UpdateUI();
    }

    // Update the text on screen
    void UpdateUI()
    {
        int h = Mathf.FloorToInt(timeLeft / 3600);
        int m = Mathf.FloorToInt((timeLeft % 3600) / 60);
        int s = Mathf.FloorToInt(timeLeft % 60);
        timeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", h, m, s);
    }

    // The trigger you asked for
    void TimeEnd()
    {
        Debug.Log("TIME END TRIGGERED");
    }
}