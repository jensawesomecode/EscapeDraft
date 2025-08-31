using UnityEngine; 
using System;

public static class EventBus
{
    public static event Action OnGameStarted;
    public static event Action OnGameWon;
    public static event Action OnGameLost;

    public static event Action<int> OnSecondTick;   // remaining seconds
    public static event Action OnTimeExpired;

    public static void GameStarted() => OnGameStarted?.Invoke();
    public static void GameWon() => OnGameWon?.Invoke();
    public static void GameLost() => OnGameLost?.Invoke();

    public static void SecondTick(int secondsLeft) => OnSecondTick?.Invoke(secondsLeft);
    public static void TimeExpired() => OnTimeExpired?.Invoke();
}
