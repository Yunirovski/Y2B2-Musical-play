using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    // Load the scene based on the string
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
