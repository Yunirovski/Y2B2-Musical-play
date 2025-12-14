using UnityEngine;

public class ScaleFromAudioClip : MonoBehaviour
{
    [SerializeField] private Vector2 minScale; // Smallest scale
    [SerializeField] private Vector2 maxScale; // Largest scale
    [SerializeField] private MicrophoneListener listener; // Reference
    [SerializeField] private float loudnessMultiplier = 100; // Multiplier
    [SerializeField] private float threshold = 0.1f; // Minimum threshold

    void Update()
    {
        // Get loudness and apply the multiplier
        float loudness = listener.GetLoudnessFromMic() * loudnessMultiplier;

        // if loudness is too low, set it to zero
        if (loudness < threshold)
            loudness = 0;

        // Change the scale based on the loudness
        transform.localScale = Vector2.Lerp(minScale, maxScale, loudness);
    }
}
