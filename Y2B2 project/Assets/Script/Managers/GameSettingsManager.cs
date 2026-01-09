using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{
    [Tooltip("All background sprites")]
    [SerializeField] private Sprite[] backgrounds;

    [Tooltip("This is the image to for the switching backgrounds")]
    [SerializeField] private Image backgroundSwitchImage;
    [Tooltip("Image for the actual background")]
    [SerializeField] private Image actualBackgroundImage;

    private int currentIndexBG = 0;

    private void Start()
    {
        UpdateBackground();
    }

    private void SwitchBackground(int dir)
    {
        if (backgrounds.Length == 0 || backgroundSwitchImage == null)
            return;

        // Move the index forward or backward
        currentIndexBG += dir;

        // Wrap around if it goes out of bounds
        if (currentIndexBG >= backgrounds.Length)
            currentIndexBG = 0;
        else if (currentIndexBG < 0) 
            currentIndexBG = backgrounds.Length - 1;

        UpdateBackground();
    }

    // Apply the backgrounds
    private void UpdateBackground()
    {
        // Get the current sprite
        Sprite selectedSprite = backgrounds[currentIndexBG];

        // Update the UI images
        backgroundSwitchImage.sprite = selectedSprite;
        actualBackgroundImage.sprite = selectedSprite;
    }

    // Functions for the buttons
    public void NextBackground() { SwitchBackground(1); }
    public void PreviousBackground() { SwitchBackground(-1); }
}
