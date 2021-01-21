using Assets.Interfaces;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _rotateSpeed = 6f;

        [SerializeField] private int _currentHealth = 10;
        [SerializeField] private int _maxHealth = 10;

        private Rigidbody _rigidbody;
        private Vector3 _direction = Vector3.zero;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }

        public bool TakeHealth(int healthToRestore)
        {
            if (_currentHealth + healthToRestore < _maxHealth)
            {
                _currentHealth += healthToRestore;
                return true;
            }

            if (_currentHealth + healthToRestore == _maxHealth)
            {
                _currentHealth = _maxHealth;
                return true;
            }

            return false;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.z = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if (_direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction),
                    _rotateSpeed * Time.fixedDeltaTime);
            }

            _rigidbody.MovePosition(transform.position + _moveSpeed * Time.fixedDeltaTime * _direction);
        }
    }
}