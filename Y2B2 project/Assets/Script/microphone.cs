using UnityEngine;
using TMPro;

public class ChooseMic : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Drag TMP dropdown here

    void Start()
    {
        // Put all mics into dropdown
        dropdown.AddOptions(new System.Collections.Generic.List<string>(Microphone.devices));

        // Start first mic
        Microphone.Start(Microphone.devices[0], true, 1, 44100);

        // When dropdown changes, call ChangeMic
        dropdown.onValueChanged.AddListener(ChangeMic);
    }

    // When you choose different mic
    void ChangeMic(int number)
    {
        Microphone.End(Microphone.devices[number]);
        Microphone.Start(Microphone.devices[number], true, 1, 44100);
    }
}