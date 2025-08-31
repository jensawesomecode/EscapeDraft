using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }

    [Tooltip("Total seconds for the run (5 minutes = 300).")]
    public int runSeconds = 300;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        EventBus.OnTimeExpired += HandleTimeExpired;
    }

    void OnDisable()
    {
        EventBus.OnTimeExpired -= HandleTimeExpired;
    }

    public void StartGame()
    {
        LoadScene(SceneNames.Room, () =>
        {
            var timer = FindAnyObjectByType<TimerManager>();
            if (timer == null) timer = I.GetComponent<TimerManager>();
            timer.StartCountdown(runSeconds);
            EventBus.GameStarted();
        });
    }

    public void WinGame()
    {
        StopTimerIfAny();
        LoadScene(SceneNames.Win, () => EventBus.GameWon());
    }

    public void LoseGame()
    {
        StopTimerIfAny();
        LoadScene(SceneNames.Lose, () => EventBus.GameLost());
    }

    public void GoToStart()
    {
        StopTimerIfAny();
        LoadScene(SceneNames.Start);
    }

    void HandleTimeExpired() => LoseGame();

    void StopTimerIfAny()
    {
        var timer = GetComponent<TimerManager>();
        if (timer != null) timer.StopCountdown();
    }

    void LoadScene(string name, System.Action afterLoad = null)
    {
        SceneManager.LoadScene(name);
        // If you prefer async:
        // StartCoroutine(LoadAsync(name, afterLoad));
        afterLoad?.Invoke();
    }
}
