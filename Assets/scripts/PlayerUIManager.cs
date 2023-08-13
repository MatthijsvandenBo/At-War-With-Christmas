using SimplePool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text ammo;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text kills;

    private void Start()
    {
        Timer.instance.timeText.Add(timer);
    }

    private void Update()
    {
        health.text = " " + this.gameObject.GetComponentInParent<PlayerHealth>().playerHealth.ToString() + " health";
        ammo.text = " " + this.gameObject.GetComponentInParent<PlayerControlShoot>().amountOFTimesShootButtonPressed.ToString() + "/12 ";
        kills.text = " " + this.gameObject.GetComponentInParent<PlayerController>().killCount.ToString() + " kills";
    }
}
