using UnityEngine;
using UnityEngine.InputSystem;

namespace SimplePool
{
    public class BulletPool : PoolItem
    {
        [Tooltip("The Speed that this object will carry in m/s")]
        [SerializeField] private float speed = 10f;

        [Tooltip("The time after this object will destroy/die")]
        private float deathTimer;
        [SerializeField] private int damage = -10;
        public int playerID = -1;

        protected override void Activate()
        {
            deathTimer = 0.0f;
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            deathTimer += Time.deltaTime;
            if (deathTimer >= 3.0f)
            {
                ReturnToPool();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerHealth playHeath = collision.gameObject.GetComponent<PlayerHealth>();
            SoundManager.instance.HitSound();
            playHeath.hitPlayerID = playerID;
            playHeath.HealthChange(damage);
            ReturnToPool();
            playerID = -1;
        }
    }
}
