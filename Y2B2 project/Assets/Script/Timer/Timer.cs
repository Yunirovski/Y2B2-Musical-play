using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* INFORMATION:
    This script has been worked on by 2 people.
    Yuni has made most of this script, the things were it says "Made by Gijs" that has been added by Gijs.
*/

public class SimpleTimer : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;

    private float timeLeft = 0f;
    private bool isRunning = false;

    // Made by Gijs
    [SerializeField] private TMP_Text timeDisplayInGame;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Button playPauseButton;
    // Until here

    private void Start()
    {
        // Made by Gijs
        timeDisplayInGame.transform.parent.gameObject.SetActive(false);
        timeDisplayInGame.text = "";
        // Until here
    }

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
        // Made by Gijs
        if (playPauseButton == null) return;
        playPauseButton.image.sprite = isRunning ? pauseSprite : playSprite;
        // Until here
    }
    //Show time in HH:MM:SS format
    void UpdateUI()
    {
        int h = Mathf.FloorToInt(timeLeft / 3600);
        int m = Mathf.FloorToInt((timeLeft % 3600) / 60);
        int s = Mathf.FloorToInt(timeLeft % 60);
        timeDisplay.text = string.Format("{0:00}:{1:00}:{2:00}", h, m, s);

        // Made by Gijs
        timeDisplayInGame.transform.parent.gameObject.SetActive(isRunning);
        timeDisplayInGame.text = isRunning ? $"{h:00}:{m:00}:{s:00}" : "";
        // Until here
    }

    void TimeEnd()
    {
        Debug.Log("TIME END TRIGGERED");
    }
}