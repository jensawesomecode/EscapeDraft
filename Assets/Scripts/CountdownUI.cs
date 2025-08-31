using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void OnEnable()  { EventBus.OnSecondTick += UpdateTimer; }
    void OnDisable() { EventBus.OnSecondTick -= UpdateTimer; }

    void UpdateTimer(int seconds)
    {
        int m = seconds / 60;
        int s = seconds % 60;
        if (timerText) timerText.text = $"{m:00}:{s:00}";
    }
}
