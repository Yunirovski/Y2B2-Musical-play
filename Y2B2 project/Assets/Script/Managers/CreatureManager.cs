using UnityEngine;
using UnityEngine.UI;

public class CreatureManager : MonoBehaviour
{
    [SerializeField] private bool stateSwitch;
    [SerializeField] private Slider loudnessSlider;

    [Header("Mic Settings")]
    [SerializeField] private MicrophoneListener listener;
    [SerializeField] private float loudnessMultiplier = 100f;

    [Header("Wake Up Settings")]
    [SerializeField] private float wu_wakeUpThreshold = 0.1f;
    [SerializeField] private float wu_requiredTimeAboveThreshold = 3f;
    [SerializeField] private float wu_requiredTimeBelowThreshold = 3f;
    [SerializeField] private float wu_timeBelowThreshold = 0f;
    [SerializeField] private float wu_timeAboveThreshold = 0f;
    [SerializeField] private bool wu_isAwake = false;

    [Header("Fall Asleep Settings")]
    [SerializeField] private float fa_fallAsleepThreshold = 0.1f;
    [SerializeField] private float fa_requiredTimeAboveThreshold = 3f;
    [SerializeField] private float fa_requiredTimeBelowThreshold = 3f;
    [SerializeField] private float fa_timeBelowThreshold = 0f;
    [SerializeField] private float fa_timeAboveThreshold = 0f;
    [SerializeField] private bool fa_isAsleep = false;

    [Header("Creature Sprite")]
    [SerializeField] private SpriteRenderer creatureSR;
    [SerializeField] private Sprite WakeUpSprite;
    [SerializeField] private Sprite AsleepSprite;

    public void switchBool()
    {
        stateSwitch = !stateSwitch;
        wu_timeBelowThreshold = 0;
        wu_timeAboveThreshold = 0;
        wu_isAwake = false;

        fa_timeAboveThreshold = 0;
        fa_timeBelowThreshold = 0;
        fa_isAsleep = false;
    }

    void Update()
    {
        float loudness = listener.GetLoudnessFromMic() * loudnessMultiplier;

        loudnessSlider.value = loudness;

        if (stateSwitch)
        {
            CheckToWake(loudness);
        }
        else
        {
            CheckToSleep(loudness);
        }

    }

    void CheckToWake(float currentLoudness)
    {
        if (wu_isAwake)
            return;

        creatureSR.sprite = AsleepSprite;

        if (currentLoudness > wu_wakeUpThreshold)
        {
            wu_timeAboveThreshold += Time.deltaTime;

            if(wu_timeAboveThreshold >= wu_requiredTimeAboveThreshold)
            {
                WakeUpCreature();
            }
        }
        else
        {
            wu_timeBelowThreshold += Time.deltaTime;

            if(wu_timeBelowThreshold >= wu_requiredTimeBelowThreshold)
            {
                wu_timeBelowThreshold = 0;
                wu_timeAboveThreshold = 0;
                ItsTooQuiet();
            }
        }
    }

    void CheckToSleep(float currentLoudness)
    {
        if (fa_isAsleep) 
            return;

        creatureSR.sprite = WakeUpSprite;

        if (currentLoudness <= fa_fallAsleepThreshold)
        {
            fa_timeBelowThreshold += Time.deltaTime;
            fa_timeAboveThreshold = 0; 

            if (fa_timeBelowThreshold >= fa_requiredTimeBelowThreshold)
            {
                ItsTooQuiet();
                fa_timeBelowThreshold = 0;
            }
        }
        else
        {
            fa_timeAboveThreshold += Time.deltaTime;

            if (fa_timeAboveThreshold >= fa_requiredTimeAboveThreshold)
            {
                fa_timeAboveThreshold = 0;
                fa_timeBelowThreshold = 0;
                WakeUpCreature();
            }
        }
    }

    private void WakeUpCreature()
    {
        creatureSR.sprite = WakeUpSprite;
        wu_isAwake = true;
    }

    private void ItsTooQuiet()
    {
        creatureSR.sprite = AsleepSprite;
        fa_isAsleep = true;
    }
}
