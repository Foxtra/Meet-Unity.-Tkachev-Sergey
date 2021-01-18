﻿using UnityEngine;


namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int _health = 1;

        public void GetDamage(int damage)
        {
            print("Ouch: " + damage);

            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}