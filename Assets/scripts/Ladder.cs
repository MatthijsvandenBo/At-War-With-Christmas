using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        other.GetComponentInParent<PlayerController>().onladder = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInParent<PlayerController>().onladder = false;
    }
}
