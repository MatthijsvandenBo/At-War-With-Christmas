using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players = new List<PlayerController>();
    private PlayerController[] allPplayers;
    private bool gamestart = false;
    private bool gameover = false;
    private float timer = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += SetID;
        PlayerInputManager.instance.onPlayerLeft += RemoveID;
    }

    private void SetID(PlayerInput obj)
    {
        obj.gameObject.GetComponent<PlayerController>().ID = FindObjectsOfType<PlayerController>().Length - 1;
        players.Add(obj.gameObject.GetComponent<PlayerController>());
    }
    private void RemoveID(PlayerInput obj)
    {
        players.Remove(obj.gameObject.GetComponent<PlayerController>());
    }

    public void GiveKill(int killPlayerId)
    {
        allPplayers = players.ToArray();
        allPplayers[killPlayerId].killCount++;
    }

    private void Update()
    {
        if (players.Count >= 2&& !gamestart)
        {
            Timer.instance.timerIsRunning = true;
            gamestart = true;
        }
        if (gameover)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 5)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void GameEnd()
    {
        allPplayers = players.ToArray();
        int mostkills = 0, mostkillsID = -1;
        SoundManager.instance.EndGameSound();
        Debug.Log("game end");
        for (int i = 0; i < allPplayers.Length; i++)
        {
            if (allPplayers[i].killCount >= mostkills)
            {
                mostkills = allPplayers[i].killCount;
                mostkillsID = allPplayers[i].ID;
            }
        }
        if (mostkills == 1)
        {
            Debug.Log("player " + mostkillsID + " wins with " + mostkills + "kill");
        }
        else
        {
            Debug.Log("player " + mostkillsID + " wins with " + mostkills + "kills");
        }
    }
}
