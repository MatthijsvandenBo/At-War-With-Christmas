using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] public float timer = 0;
    public bool timerIsRunning = false;
    public List<TMP_Text> timeText;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (timer <= 0 && timerIsRunning)
        {
            timerIsRunning = false;
            timer = 0;
            GameManager.instance.GameEnd();
            return;
        }
        else if (timerIsRunning)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        TMP_Text[] timertexts = timeText.ToArray();
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        for (int i = 0; i < timertexts.Length; i++)
        {
            timertexts[i].text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
