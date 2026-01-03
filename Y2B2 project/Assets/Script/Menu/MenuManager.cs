using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Tooltip("Panel for the menu")]
    [SerializeField] private GameObject menuPanel;
    [Tooltip("Target position for where the menu panel should move")]
    [SerializeField] private GameObject targetPos;
    [Tooltip("Speed the panel moves")]
    [SerializeField] private float moveSpeed;
    [Tooltip("Button for opening and closing the menu")]
    [SerializeField] private Button OpenCloseButton;

    private Vector2 originalPos;
    private TMP_Text buttonText;

    private bool isOpen;

    void Start()
    {
        originalPos = menuPanel.transform.position;
        buttonText = OpenCloseButton.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        Vector2 dest;

        if (isOpen)
            dest = targetPos.transform.position;
        else
            dest = originalPos;

        menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, dest, moveSpeed * Time.deltaTime);
    }

    public void OpenCloseMenu()
    {
        isOpen = !isOpen;
    
        if (isOpen)
            buttonText.text = "Close Menu";
        else
            buttonText.text = "Open Menu";
    }
}
