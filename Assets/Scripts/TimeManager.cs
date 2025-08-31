using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    private Coroutine ticking;
    private int secondsLeft;

    public void StartCountdown(int seconds)
    {
        StopCountdown();
        secondsLeft = seconds;
        ticking = StartCoroutine(Tick());
    }

    public void StopCountdown()
    {
        if (ticking != null)
        {
            StopCoroutine(ticking);
            ticking = null;
        }
    }

    private IEnumerator Tick()
    {
        // immediate tick to update UI at start
        EventBus.SecondTick(secondsLeft);

        while (secondsLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            secondsLeft--;
            EventBus.SecondTick(secondsLeft);
        }

        EventBus.TimeExpired();
    }
}