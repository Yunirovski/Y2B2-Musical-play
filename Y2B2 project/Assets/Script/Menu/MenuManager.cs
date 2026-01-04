using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Bar")]
    [Tooltip("Panel for the menu")]
    [SerializeField] private GameObject menuPanel;
    [Tooltip("Target position for where the menu panel should move")]
    [SerializeField] private GameObject targetPos;
    [Tooltip("Speed the panel moves")]
    [SerializeField] private float moveSpeed;
    [Tooltip("Button for opening and closing the menu")]
    [SerializeField] private Button OpenCloseButton;

    [Header("Menu Switching")]
    [Tooltip("All menus")]
    [SerializeField] private GameObject[] menus;

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

    // Set the menu state
    private void SetMenuState(bool state)
    {
        isOpen = state;

        if (isOpen)
            buttonText.text = "Close Menu";
        else
            buttonText.text = "Open Menu";
    }

    // Open and close the menu bar
    public void OpenCloseMenu()
    {
        SetMenuState(!isOpen);
    }

    // Change the menus
    public void ChangeMenu(int menuNumber)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == menuNumber);
        }

        // Close menu
        SetMenuState(false);
    }

    // Close all menu's
    public void CloseEveryMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

        SetMenuState(false);
    }
}
