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
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pressedMouseIcon, hotSpot, mode);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultMouseIcon, hotSpot, mode);
        }

        // Esc key to quit menus and game
        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (ConversationSystem.Instance.gameObject.activeSelf && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E)))
        {
            ConversationSystem.Instance.OnNextButtonClick();
        }

        if (Input.GetKeyDown(KeyCode.Q))    // Testing use only
        {
            GameManager.instance.ResourceCount += 90;
        }
    }
}
