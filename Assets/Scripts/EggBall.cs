using UnityEngine;


namespace Assets.Scripts
{
    public class EggBall : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 4f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private int _damage = 1;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            Destroy(gameObject, _lifeTime);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var enemy = other.gameObject.GetComponent<PlayerController>();
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}