using Assets.Constants;
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
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _AttackPoint;

        [SerializeField] private int _health = 1;
        [SerializeField] private int _attackDamage = 1;
        [SerializeField] private float _fireRate = 0.8f;
        [SerializeField] private float _patrolDelay = 2f;
        [SerializeField] private float _eggSpeed = 1f;
        [SerializeField] private float _rayCastAttackLength = 0.3f;

        private Vector3 _eggDir;
        private GameObject _player;
        private NavMeshAgent _agent;
        private Animator _animator;
        private RaycastHit _hit;
        private PlayerController _playerController;

        private float _nextFire;
        private float _waiting;

        private int _wayPointIndex;

        private bool _fire;
        private bool _isFollow;
        private bool _isAlive = true;
        private bool _isWait;

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
            _playerController = _player.GetComponent<PlayerController>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(_patrolWay[0].position);
            _animator = GetComponentInChildren<Animator>();
            _waiting = 0;
        }

        private void FixedUpdate()
        {
            if (_isAlive)
            {
                if (_isWait)
                {
                    _waiting += Time.deltaTime;
                    if (_waiting > _patrolDelay) _isWait = false;
                }
                else
                {
                    if (!_isFollow)
                    {
                        _agent.isStopped = false;
                        Patrol();
                    }
                    else
                    {
                        FollowPlayer();
                    }
                }
            }
        }

        private void FollowPlayer()
        {
            _agent.SetDestination(_player.transform.position);

            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _agent.isStopped = true;
                _animator.SetBool(AnimationStates.Walk, false);
                _nextFire += Time.deltaTime;

                if (_nextFire > _fireRate)
                {
                    Attack();
                    _nextFire = 0f;
                }
            }
            else
            {
                _agent.isStopped = false;
            }
        }

        private void Attack()
        {
            _animator.SetTrigger(AnimationStates.Attack);

            if (!CheckObjectByRayCast(_AttackPoint, _rayCastAttackLength, out _hit)) return;
            if (_hit.transform.CompareTag(Tags.Player))
            {
                _playerController.TakeDamage(_attackDamage);
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
                    _animator.SetBool(AnimationStates.Walk, true);
                }
                else
                {
                    _animator.SetBool(AnimationStates.Walk, false);
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
            _isAlive = false;
            _animator.SetTrigger(AnimationStates.Death);
            // Destroy(gameObject);
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (_playerController.IsAlive)
                {
                    transform.LookAt(collider.transform);
                    _isFollow = true;
                }
                else
                {
                    _isFollow = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isFollow = false;
                _isWait = true;
                _waiting = 0f;
                _agent.SetDestination(transform.position);
            }
        }

        private bool CheckObjectByRayCast(Transform startPosition, float rayCastLength, out RaycastHit hit)
        {
            return Physics.Raycast(startPosition.position, startPosition.TransformDirection(Vector3.forward), out hit,
                rayCastLength, _layerMask);
        }
    }
}