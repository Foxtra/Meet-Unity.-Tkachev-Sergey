using UnityEngine;


namespace Assets.Scripts
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private GameObject _eggBall;
        [SerializeField] private Transform _firePosition;

        [SerializeField] private int _health = 5;
        [SerializeField] private float _rotateSpeed = 0.05f;
        [SerializeField] private float _fireRate = 0.8f;
        [SerializeField] private float _eggSpeed = 4f;

        private Quaternion _initialRotation;
        private Vector3 _eggDir;
        private GameObject _player;

        private float _nextFire;

        private bool _isReturning;
        private bool _fire;

        public void TakeDamage(int damage)
        {
            print("Ouch: " + damage);

            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }
        }

        private void Awake()
        {
            _initialRotation = transform.rotation;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FixedUpdate()
        {
            if (_isReturning)
            {
                ReturnInitialRotation();
            }


            if (_fire && Time.time > _nextFire)
            {
                Fire();
            }
        }

        private void Fire()
        {
            //don't use instantiate, use pool of objects instead
            var eggBall = Instantiate(_eggBall, _firePosition.position, transform.rotation);
            _eggDir = _player.transform.position - _firePosition.position;
            eggBall.GetComponent<Rigidbody>().AddForce(_eggDir.normalized * _eggSpeed, ForceMode.Impulse);
            _nextFire = Time.time + _fireRate;
        }

        private void ReturnInitialRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _initialRotation, _rotateSpeed);
            if (transform.rotation == _initialRotation)
            {
                _isReturning = false;
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                transform.LookAt(collider.transform);
                _fire = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isReturning = true;
                _fire = false;
            }
        }
    }
}