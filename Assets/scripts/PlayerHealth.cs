using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public bool isDead = false;
    public int hitPlayerID;
    private int killID;

    public void HealthChange(float Value)
    {
        if (hitPlayerID == this.gameObject.GetComponent<PlayerController>().ID) return;
        playerHealth += Value;
        if (playerHealth <= 0)
        {
            isDead = true;
            killID = hitPlayerID;
            GameManager.instance.GiveKill(killID);
            PlayerDead();
        }
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            isDead = true;
            PlayerDead();
        }
    }

    private void PlayerDead()
    {

    }
}
