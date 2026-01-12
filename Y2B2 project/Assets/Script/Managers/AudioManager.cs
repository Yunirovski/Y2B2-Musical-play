using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //AudioSource component
    public AudioSource audioSource;
    // The specific audio file
    public AudioClip clickSound;

   
    public void PlayClickSound()
    {
        // plays the sound
        audioSource.PlayOneShot(clickSound);
    }
}