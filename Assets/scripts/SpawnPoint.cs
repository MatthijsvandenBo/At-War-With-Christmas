using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool canSpawn = true;

    private void OnTriggerStay(Collider other)
    {
        canSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }
}
