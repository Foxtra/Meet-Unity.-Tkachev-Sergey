using Assets.Interfaces;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour, ITakeDamage
    {
        //[SerializeField] private GameObject _fireBall;
        //[SerializeField] private Transform _firePosition;

        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _rotateSpeed = 6f;

        [SerializeField] private int _currentHealth = 10;
        [SerializeField] private int _maxHealth = 10;

        private Rigidbody _rigidbody;
        private Vector3 _direction = Vector3.zero;

        private bool _fire;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.z = Input.GetAxisRaw("Vertical");

            //if (Input.GetButtonDown("Fire1"))
            //{
            //    _fire = true;
            //}
        }

        private void FixedUpdate()
        {
            if (_direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction),
                    _rotateSpeed * Time.fixedDeltaTime);
            }

            _rigidbody.MovePosition(transform.position + _moveSpeed * Time.fixedDeltaTime * _direction);

            //if (_fire)
            //{
            //    Fire();
            //}
        }

        //private void Fire()
        //{
        //    Instantiate(_fireBall, _firePosition.position, _firePosition.rotation);
        //    _fire = false;
        //}
    }
}