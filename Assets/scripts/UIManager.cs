using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    private int playerHealth = 100;
    void Start()
    {
        healthText.text = playerHealth.ToString("");   
    }

    public void HealthUpdate()
    {
        healthText.text = playerHealth.ToString("");   
    }
}
