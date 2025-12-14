using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    [Header("Mic Settings")]
    [SerializeField] private MicrophoneListener listener;
    [SerializeField] private float loudnessMultiplier = 100f;

    [Header("Wake Up Settings")]
    [SerializeField] private float wakeUpThreshold = 0.1f;
    [SerializeField] private float requiredTimeAboveThreshold = 3f;
    [SerializeField] private float requiredTimeBelowThreshold = 3f;
    [SerializeField] private float timeAboveThreshold = 0f;
    private float timeBelowThreshold = 0f;

    [Header("Creature Sprite")]
    [SerializeField] private SpriteRenderer creatureSR;
    [SerializeField] private Sprite WakeUpSprite;
    [SerializeField] private Sprite AsleepSprite;

    private bool isAwake = false;

    void Update()
    {
        WakeUp();
    }

    #region Wake Up
    private void WakeUp()
    {
        if (isAwake)
            return;

        float loudness = listener.GetLoudnessFromMic() * loudnessMultiplier;

        if (loudness > wakeUpThreshold)
        {
            timeAboveThreshold += Time.deltaTime;

            if (timeAboveThreshold >= requiredTimeAboveThreshold)
            {
                WakeUpCreature();
            }
        }
        else
        {
            timeBelowThreshold += Time.deltaTime;

            if (timeBelowThreshold >= requiredTimeBelowThreshold)
            {
                ItsTooQuiet();
            }
        }
    }

    private void WakeUpCreature()
    {
        isAwake = true;

        creatureSR.sprite = WakeUpSprite;

        Debug.Log("CREATURE IS AWAKE!!!");
    }

    private void ItsTooQuiet()
    {
        timeAboveThreshold = 0;
        timeBelowThreshold = 0;

        creatureSR.sprite = AsleepSprite;

        Debug.Log("ITS TOO QUIET!!!");
    }
    #endregion
}
