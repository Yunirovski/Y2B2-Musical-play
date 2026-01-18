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

    // ---  made by yuni ---
    [Header("Creature Settings - made by yuni")]
    [Tooltip("List of all creature GameObjects/Images")]
    [SerializeField] private Image[] creatureImages;

    [Tooltip("The preview image for creature selection")]
    [SerializeField] private Image backgroundCreature;

    [Tooltip("The actual creature image in the game scene")]
    [SerializeField] private Image actualCreature;

    private int currentIndexCreature = 0;
    // ---  made by yuni ---

    private void Start()
    {
        UpdateBackground();
        UpdateCreature(); // made by yuni
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

    // ---  made by yuni ---
    private void SwitchCreature(int dir)
    {
        if (creatureImages.Length == 0) return;

        currentIndexCreature += dir;

        if (currentIndexCreature >= creatureImages.Length)
            currentIndexCreature = 0;
        else if (currentIndexCreature < 0)
            currentIndexCreature = creatureImages.Length - 1;

        UpdateCreature();
    }

    private void UpdateCreature()
    {
        if (creatureImages.Length == 0) return;

        // enable only the selected one
        for (int i = 0; i < creatureImages.Length; i++)
        {
            if (creatureImages[i] != null)
            {
                // made by yuni: toggle GameObject 
                bool isActive = (i == currentIndexCreature);
                creatureImages[i].gameObject.SetActive(isActive);
            }
        }

        // made by yuni:  sync preview images 
        if (creatureImages[currentIndexCreature] != null)
        {
            Sprite selectedSprite = creatureImages[currentIndexCreature].sprite;

            if (backgroundCreature != null)
                backgroundCreature.sprite = selectedSprite;

            if (actualCreature != null)
                actualCreature.sprite = selectedSprite;
        }
    }

    public void NextCreature() { SwitchCreature(1); } // made by yuni
    public void PreviousCreature() { SwitchCreature(-1); } // made by yuni
    // ---  made by yuni ---
}