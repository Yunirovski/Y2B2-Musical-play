using UnityEngine;
using TMPro;

public class ChooseMic : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Drag TMP dropdown here
    public MicrophoneListener listener; // Link to listener - made by Yuni

    void Start()
    {
        // Clear the ABC first!
        dropdown.ClearOptions();
        // Put all mics into dropdown
        dropdown.AddOptions(new System.Collections.Generic.List<string>(Microphone.devices));

        // When dropdown changes, call ChangeMic

        dropdown.onValueChanged.AddListener(ChangeMic);
    }

    // When you choose different mic
    void ChangeMic(int number)
    {
        // Tell the listener to switch - made by Yuni
        if (listener != null)
        {
            listener.UpdateMic(Microphone.devices[number]);
        }
    }
}