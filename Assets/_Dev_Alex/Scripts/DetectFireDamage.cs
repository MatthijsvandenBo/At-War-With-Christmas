using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFireDamage : MonoBehaviour
{
    [SerializeField] private bool beInFire;
    [SerializeField] private bool stopDealingDamage;

    void Update()
    {
    //    if (beInFire == true)
    //    {
    //        if (stopDealingDamage == false)
    //        {
    //            stopDealingDamage = true;
    //            StartCoroutine(TakingDamageFromFire());
    //        }
    //    }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            //beInFire = true;
            Debug.Log("you are on Fire, get out!");
            col.GetComponent<PlayerHealth>().hitPlayerID = -1;
            col.GetComponent<PlayerHealth>().HealthChange(-10 * Time.deltaTime);
                    }
    }


    
    //IEnumerator TakingDamageFromFire()
    //{
    //    yield return new WaitForSeconds(1);
    //    playerHealthHP.HealthChange(-10);
    //    stopDealingDamage = false;
    //    beInFire = false;
    //}
}
