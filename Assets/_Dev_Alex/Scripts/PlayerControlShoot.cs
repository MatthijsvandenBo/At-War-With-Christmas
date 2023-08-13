using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimplePool
{
    public class PlayerControlShoot : MonoBehaviour
    {
        public ObjectPooling bulletPool;
        private InputActionAsset inputAsset;
        private InputActionMap player;
        [SerializeField] private GameObject firePoint;

        public int amountOFTimesShootButtonPressed = 12;

        private void Awake()
        {
            inputAsset = this.GetComponent<PlayerInput>().actions;
            player = inputAsset.FindActionMap("Player");
            bulletPool = FindObjectOfType<ObjectPooling>();
        }

        private void OnEnable()
        {
            player.FindAction("Shoot").performed += DoShoot;
            player.Enable();
        }
        private void OnDisable()
        {
            player.FindAction("Shoot").performed -= DoShoot;
            player.Disable();
        }

        private void DoShoot(InputAction.CallbackContext obj)
        {
            SoundManager.instance.GunSound();
            GameObject bullet = bulletPool.GetPooledObject(firePoint.transform.position, firePoint.transform.rotation, null);
            bullet.GetComponent<BulletPool>().playerID = this.gameObject.GetComponent<PlayerController>().ID;
            amountOFTimesShootButtonPressed--;
        }

        private void FixedUpdate()
        {
            if (amountOFTimesShootButtonPressed <= 0)
            {
                StartCoroutine(waitingUntilShooting());
            }
        }
        public IEnumerator waitingUntilShooting()
        {
            player.FindAction("Shoot").performed -= DoShoot;
            yield return new WaitForSeconds(2f);
            amountOFTimesShootButtonPressed = 12;
            player.FindAction("Shoot").performed += DoShoot;
        }

        public void TurnOffShooting(bool off)
        {
            if (off)
            {
                player.FindAction("Shoot").performed -= DoShoot;
            }
            else
            {
                player.FindAction("Shoot").performed += DoShoot;
            }
        }
    }
}