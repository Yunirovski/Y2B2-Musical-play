using UnityEngine;
using UnityEngine.UI;

public class MicrophoneListener : MonoBehaviour
{
    [SerializeField] private int sampleWindow = 64; // Amount of audio samples
    private AudioClip clip; // The recorded clip
    //private string activeMicName; // Current active mic
    private string currentMicName; // made by Yuni

    private bool isMuted = false;

    [Header("Mute Button")]
    [SerializeField] private Button muteMicButton;
    [SerializeField] private Color mutedMicColor = Color.red;
    [SerializeField] private Color unmutedMicColor = Color.white;

    void Start()
    {
        // Init with default mic - made by Yuni
        if (Microphone.devices.Length > 0)
        {
            UpdateMic(Microphone.devices[0]);
        }
        muteMicButton.image.color = unmutedMicColor;
    }

    //public void MicToAudioClip(string micName)
    //{
    //    if (activeMicName != null)
    //        Microphone.End(activeMicName);
    //
    //    activeMicName = micName;
    //
    //    clip = Microphone.Start(activeMicName, true, 1, AudioSettings.outputSampleRate);
    //}
    // New method to change mic source - made by Yuni
    public void UpdateMic(string newMicName)
    {
        currentMicName = newMicName;
        clip = Microphone.Start(currentMicName, true, 1, AudioSettings.outputSampleRate);
    }
    private void MicToAudioClip()
    {
        string micName = Microphone.devices[0]; // Get the mic name
        clip = Microphone.Start(micName, true, 1, AudioSettings.outputSampleRate); // Start recording
    }

    // Get the loudness from the microphone
    public float GetLoudnessFromMic()
    {
        if (isMuted || string.IsNullOrEmpty(currentMicName)) return 0f;

        // Use currentMicName instead of index 0 - made by Yuni
        int clipPos = Microphone.GetPosition(currentMicName);
        return GetLoudnessFromAudioClip(clipPos, clip);  // Calculate the loudness
    }

    // Get the loudness from the audio clip
    private float GetLoudnessFromAudioClip(int clipPos, AudioClip clip)
    {
        // Where to start reading data
        int startPos = clipPos - sampleWindow;

        if (startPos < 0)
            return 0; // Not enough data 

        // Array to hold the data
        float[] waveData = new float[sampleWindow];

        // Get the data into a array
        clip.GetData(waveData, startPos);

        // Get the total loudness
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        // Return the loudness
        return totalLoudness / sampleWindow;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        muteMicButton.image.color = isMuted ? mutedMicColor : unmutedMicColor;
    }
}
