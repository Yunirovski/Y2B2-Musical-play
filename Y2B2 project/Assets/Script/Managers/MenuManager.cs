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
    [SerializeField] private Button openCloseButton;

    [SerializeField] private Sprite arrowDownSprite;
    [SerializeField] private Sprite arrowUpSprite;

    [Header("Menu Switching")]
    [Tooltip("All menus")]
    [SerializeField] private GameObject[] menus;

    private Vector2 originalPos;

    private bool isOpen;

    private void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }

    void Start()
    {
        originalPos = menuPanel.transform.position;
    }

    private void Update()
    {
        Vector2 dest = isOpen ? (Vector2)targetPos.transform.position : originalPos;

        // Only lerp it when we aren't there yet
        if(Vector2.Distance(menuPanel.transform.transform.position, dest) > 0.01f)
            menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, dest, moveSpeed * Time.deltaTime);
        else
            menuPanel.transform.position = dest;

    }

    // Set the menu state
    private void SetMenuState(bool state)
    {
        isOpen = state;

        openCloseButton.image.sprite = isOpen ? arrowUpSprite : arrowDownSprite;
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
