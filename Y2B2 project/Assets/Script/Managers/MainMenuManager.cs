using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Load the scene based on the string
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
