using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] respawn;

    private void Update()
    {
        int random = Random.Range(0, respawn.Length);
        for (int i = 0; i < FindObjectsOfType<PlayerHealth>().Length; i++)
        {
            if (FindObjectsOfType<PlayerHealth>()[i].isDead)
            {
                if (respawn[random].GetComponent<SpawnPoint>().canSpawn)
                {
                    FindObjectsOfType<PlayerHealth>()[i].transform.position = respawn[random].transform.position;
                    FindObjectsOfType<PlayerHealth>()[i].playerHealth = 100;
                    FindObjectsOfType<PlayerHealth>()[i].isDead = false;
                }

            }
        }
    }
}
