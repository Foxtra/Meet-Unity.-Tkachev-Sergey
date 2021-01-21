using UnityEngine;


namespace Assets.Scripts
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 4;
        [SerializeField] private int _damage = 1;
        private float _speed = 10f;

        private void Awake()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<EnemyController>();
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}