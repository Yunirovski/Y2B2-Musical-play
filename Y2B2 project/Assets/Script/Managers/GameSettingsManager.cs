using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameSettingsManager : MonoBehaviour
{
    [Tooltip("All background sprites")]
    [SerializeField] private VideoClip[] backgroundVideos;
    [SerializeField] private VideoPlayer videoPlayer;

    [Tooltip("This is the image to for the switching backgrounds")]
    [SerializeField] private RawImage backgroundSwitchVideo;
    [Tooltip("Image for the actual background")]
    [SerializeField] private RawImage actualBackgroundVideo;

    private int currentIndexBG = 0;

    private void Start()
    {
        UpdateBackground();
    }

    private void SwitchBackground(int dir)
    {
        if (backgroundVideos.Length == 0 || backgroundSwitchVideo == null)
            return;

        // Move the index forward or backward
        currentIndexBG += dir;

        // Wrap around if it goes out of bounds
        if (currentIndexBG >= backgroundVideos.Length)
            currentIndexBG = 0;
        else if (currentIndexBG < 0) 
            currentIndexBG = backgroundVideos.Length - 1;

        UpdateBackground();
    }

    // Apply the backgrounds
    private void UpdateBackground()
    {
        // Get the current sprite
        VideoClip selectedClip = backgroundVideos[currentIndexBG];

        // Update the UI images
        videoPlayer.clip = selectedClip;
    }

    // Functions for the buttons
    public void NextBackground() { SwitchBackground(1); }
    public void PreviousBackground() { SwitchBackground(-1); }
}
