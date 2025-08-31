using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI titleText;
    public Button startButton;

    void Start()
    {
        // Title defaults
        if (titleText != null)
        {
            titleText.text = "Escape The Room";
            titleText.fontSize = 96; // big & legible; tweak for your canvas scaling
        }

        // Button wire-up
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                // (Optionally play a click SFX here)
                FindAnyObjectByType<GameManager>()?.StartGame();
            });
        }
    }
}
