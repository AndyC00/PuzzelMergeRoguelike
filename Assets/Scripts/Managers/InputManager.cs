using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //mouse icons
    [SerializeField] private Texture2D defaultMouseIcon;
    [SerializeField] private Texture2D pressedMouseIcon;

    private CursorMode mode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    // singleton pattern
    public static InputManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        Cursor.SetCursor(defaultMouseIcon, Vector2.zero, mode);
    }

    void Update()
    {
        // --- Mouse ---
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Cursor.SetCursor(pressedMouseIcon, hotSpot, mode);
            }
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                Cursor.SetCursor(defaultMouseIcon, hotSpot, mode);
            }
        }

        // --- Keyboard ---
        var kb = Keyboard.current;
        if (kb != null)
        {
            // Esc key to quit menus and game
            if (kb.escapeKey.wasPressedThisFrame)
            {
                if (ConversationSystem.Instance.gameObject.activeSelf)
                {
                    ConversationSystem.Instance.Hide();
                }
                else if (ExitUI.instance.gameObject.activeSelf)
                {
                    ExitUI.instance.Hide();
                }
                else
                {
                    ExitUI.instance.Show();
                }
            }

            if (ConversationSystem.Instance.gameObject.activeSelf && (kb.numpadEnterKey.wasPressedThisFrame || kb.enterKey.wasPressedThisFrame || kb.eKey.wasPressedThisFrame))
            {
                ConversationSystem.Instance.OnNextButtonClick();
            }

            // for testing only
            if (kb.qKey.wasPressedThisFrame)
            {
                GameManager.instance.ResourceCount += 90;
            }
        }
    }
}
