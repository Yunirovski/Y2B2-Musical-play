using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/* 
    INFORMATION:
    This script has been worked on by 2 people.
    Gijs has made most of this script, the things were it says "Made by Yuni" that has been added by Yuni.
*/


public class Creature : MonoBehaviour
{
    public enum CreatureState { Asleep, EyesOpen, GetUp, StandUp, Awake }

    [Serializable]
    public struct StateVisuals
    {
        public CreatureState state;
        public Sprite sprite;
        public string animationName;// Made by Yuni, add animation
    }

    [Header("References")]
    [SerializeField] private MicrophoneListener listener;
    [SerializeField] private Slider loudnessSlider;
    [SerializeField] private Slider sensitivitySlider; // Made by Yuni
    private Image creatureImage;
    private Animator animator;// Made by Yuni, Animator component

    [Header("State Settings")]
    [Tooltip("Match each state to a sprite")]
    [SerializeField] private List<StateVisuals> stateVisualsList;
    [Tooltip("Current state of the creature")]
    [SerializeField] private CreatureState currentState = CreatureState.Asleep;

    [Header("Settings")]
    [Tooltip("Multiplies the mic input")]
    [SerializeField] private float loudnessMultiplier = 100f;
    [Tooltip("Volume level requiored for it to be considered 'Noise'")]
    [SerializeField] private float threshold = 0.1f;
    [Tooltip("Time required to change state")]
    [SerializeField] private float timeRequirement = 3f;
    [Tooltip("How long is needed of the opposite input (noise/silence) for the progress timers to reset")]
    [SerializeField] private float resetDelay = 3f;
    [Tooltip("Multiplier for mic volume")]
    [SerializeField] private float sensitivity = 25.0f; // Made by Yuni

    [Header("Becoming Awake")]
    [Tooltip("How long the player have been loud, DO NOT CHANGE")]
    [SerializeField] private float timerAbove;
    [Tooltip("How long the player has been quited, DO NOT CHANGE")]
    [SerializeField] private float silenceCooldown;

    [Header("Falling Asleep")]
    [Tooltip("How long the player has been quiet, DO NOT CHANGE")]
    [SerializeField] private float timerBelow;
    [Tooltip("How long the player has been loud, DO NOT CHANGE")]
    [SerializeField] private float noiseCooldown;

    private Dictionary<CreatureState, Sprite> spriteStateMap;
    private Dictionary<CreatureState, string> animationMap; // Made by Yuni, NEW: Maps state to animation name
    private void Awake()
    {
        // Grab the component
        creatureImage = GetComponent<Image>();
        animator = GetComponent<Animator>(); // Made by Yuni
    }

    void Start()
    {
        // Initialize the dictionary
        spriteStateMap = new Dictionary<CreatureState, Sprite>();
        animationMap = new Dictionary<CreatureState, string>(); // Made by Yuni
        // Fill the dictionary 
        foreach (var visual in stateVisualsList)
            if (!spriteStateMap.ContainsKey(visual.state))
                spriteStateMap.Add(visual.state, visual.sprite);

        foreach (var visual in stateVisualsList)// Made by Yuni
            if (!animationMap.ContainsKey(visual.state))// Made by Yuni
                animationMap.Add(visual.state, visual.animationName);// Made by Yuni

        // Set the start state
        SetState(currentState);
    }

    void Update()
    {
        // Get the current loudness of the microphone
        float loudness = listener.GetLoudnessFromMic() * loudnessMultiplier;

        // Update the visual loudness slider
        if (loudnessSlider != null)
            loudnessSlider.value = loudness;

        HandleStates();
        UpdateTimers(loudness);


        {
            // Made by Yuni
            // 1. Get volume from mic
            float rawLoudness = listener.GetLoudnessFromMic() * loudnessMultiplier;

            // 2. Use slider value as sensitivity if the slider exists
            if (sensitivitySlider != null)
            {
                sensitivity = sensitivitySlider.value;
            }

            // 3. Multiply volume by sensitivity
            float processedLoudness = rawLoudness * sensitivity;

            // 4. Show the result
            if (loudnessSlider != null)
                loudnessSlider.value = processedLoudness;

            HandleStates();

            // 5. Process the change
            UpdateTimers(processedLoudness);
        }
    }


    private void UpdateTimers(float loudness)
    {
        // Audio detected || MOVE TOWARDS WAKING UP
        if(loudness > threshold)
        {
            timerAbove += Time.deltaTime;
            silenceCooldown = 0;
        }
        else // Silence detected
        {
            silenceCooldown += Time.deltaTime;

            // Wait 3 secs of silence, until resetting
            if(silenceCooldown >= resetDelay)
            {
                timerAbove = 0;
            }
        }

        // Silence audio || MOVE TOWARDS FALLING ASLEEP
        if(loudness <= threshold)
        {
            timerBelow += Time.deltaTime;
            noiseCooldown = 0;
        }
        else // Noise detected
        {
            noiseCooldown += Time.deltaTime;
        
            // Wait 3 secs of audio, until resetting
            if(noiseCooldown >= resetDelay)
            {
                timerBelow = 0;
            }
        }
    }

    private void HandleStates()
    {
        // If the player has been loud long enough, go up one state
        if (timerAbove >= timeRequirement)
        {
            NextStage(1);
            timerAbove = 0;
        }
        // Else if the player has been quiet long enough, go down one state
        else if (timerBelow >= timeRequirement)
        {
            NextStage(-1);
            timerBelow = 0;
        }
    }

    private void SetState(CreatureState state)
    {
        // Update the current state
        currentState = state;

        // Everytime the states change, reset all timers
        timerAbove = 0;
        timerBelow = 0;
        silenceCooldown = 0;
        noiseCooldown = 0;

        // Swap the creature its sprite to match the new state
        if (spriteStateMap.ContainsKey(state))
            creatureImage.sprite = spriteStateMap[state];
    }

    private void NextStage(int dir)
    {
        // Convert state to number, add the direction (1 to go up, -1 to go down), clamp it so it stays within the list
        int nextIndex = Mathf.Clamp((int)currentState + dir, 0, stateVisualsList.Count - 1);

        // Convert the number back into a state
        SetState((CreatureState)nextIndex);
    }

    // Function for a UI button to switch the start state. So WAKING UP || FALLING ASLEEP
    public void SwitchState()
    {
        if (currentState == CreatureState.Asleep)
        {
            SetState(CreatureState.Awake);
        }
        else
        {
            SetState(CreatureState.Asleep); 
        }
    }
}
