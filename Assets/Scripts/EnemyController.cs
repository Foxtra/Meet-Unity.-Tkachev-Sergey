using Assets.Interfaces;
using UnityEngine;
using UnityEngine.AI;


namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private GameObject _eggBall;
        [SerializeField] private Transform _firePosition;
        [SerializeField] private Transform[] _patrolWay;

        [SerializeField] private int _health = 1;
        [SerializeField] private float _fireRate = 0.8f;
        [SerializeField] private float _patrolDelay = 2f;
        [SerializeField] private float _eggSpeed = 1f;

        private Vector3 _eggDir;
        private GameObject _player;
        private NavMeshAgent _agent;

        private float _nextFire;
        private float _waiting;

        private int _wayPointIndex;

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
            _player = GameObject.FindGameObjectWithTag("Player");
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(_patrolWay[0].position);
            _waiting = 0;
        }

        private void FixedUpdate()
        {
            if (!_fire)
            {
                _agent.isStopped = false;
                Patrol();
            }


            if (_fire && Time.time > _nextFire)
            {
                _agent.isStopped = true;
                Fire();
            }
        }

        private void Patrol()
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _waiting += Time.deltaTime;
                if (_waiting > _patrolDelay)
                {
                    _wayPointIndex = (_wayPointIndex + 1) % _patrolWay.Length;
                    _agent.SetDestination(_patrolWay[_wayPointIndex].position);
                    _waiting = 0f;
                }
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
                _fire = false;
            }
        }
    }
}