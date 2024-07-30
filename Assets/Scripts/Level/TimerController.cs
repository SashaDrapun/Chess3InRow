using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;

    private float timeRemaining;
    private bool isTimerRunning;

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
            }
            UpdateTimerText();
        }
    }

    public void StartTimer(int seconds)
    {
        timeRemaining = seconds;
        isTimerRunning = true;
        
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}