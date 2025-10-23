using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    // singleton pattern
    public static ExitUI instance { get; private set; }
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
        yesButton ??= transform.Find("YesButton").GetComponent<Button>();
        yesButton.onClick.AddListener(OnYesButtonClick);

        noButton ??= transform.Find("NoButton").GetComponent<Button>();
        noButton.onClick.AddListener(OnNoButtonClick);

        Hide();
    }

    private void OnYesButtonClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnNoButtonClick()
    {
        Hide();
    }

    public void Show()
    { 
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
