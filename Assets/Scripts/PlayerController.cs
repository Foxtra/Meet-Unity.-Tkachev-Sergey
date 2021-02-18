using Assets.Constants;
using Assets.Interfaces;
using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Transform _jumpCheck;
        [SerializeField] private Transform _AttackPoint;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _rotateSpeed = 6f;
        [SerializeField] private float _rayCastJumpLength = 0.1f;
        [SerializeField] private float _rayCastAttackLength = 0.3f;
        [SerializeField] private float _overlapRadius = 0.5f;

        [SerializeField] private int _currentHealth = 10;
        [SerializeField] private int _maxHealth = 10;
        [SerializeField] private int _attackDamage = 1;
        [SerializeField] private int _strongAttackDamage = 2;

        private Rigidbody _rigidbody;
        private Animator _animator;
        private Vector3 _direction = Vector3.zero;
        private RaycastHit _hit;

        private bool _isJump;
        private bool _isAttack;
        private bool _isStrongAttack;
        public bool IsAlive { get; private set; } = true;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                IsAlive = false;
                _direction = Vector3.zero;
                Die();
            }
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
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (IsAlive)
            {
                _direction.x = Input.GetAxisRaw("Horizontal");
                _direction.z = Input.GetAxisRaw("Vertical");

                _isJump = Input.GetKeyDown(KeyCode.Space);
                _isAttack = Input.GetKeyDown(KeyCode.Mouse0);
                _isStrongAttack = Input.GetKeyDown(KeyCode.Mouse1);
            }
        }

        private void Die()
        {
            _animator.SetTrigger(AnimationStates.Death);
        }

        private void FixedUpdate()
        {
            Move();
            Attack();
        }

        private void Attack()
        {
            if (_isAttack)
            {
                _animator.SetTrigger(AnimationStates.Attack);

                if (!CheckObjectByRayCast(_AttackPoint, _rayCastAttackLength, out _hit)) return;
                if (_hit.transform.CompareTag(Tags.Enemy))
                {
                    var enemy = _hit.transform.GetComponent<EnemyController>();
                    enemy.TakeDamage(_attackDamage);
                }
            }

            if (_isStrongAttack)
            {
                _animator.SetTrigger(AnimationStates.StrongAttack);
                DoStrongAttack();
            }
        }

        private void DoStrongAttack()
        {
            var colliders = Physics.OverlapSphere(transform.position, _overlapRadius, _layerMask);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag(Tags.Enemy))
                {
                    var enemy = collider.GetComponent<EnemyController>();
                    enemy.TakeDamage(_strongAttackDamage);
                }
            }
        }

        private void Move()
        {
            if (_isJump)
            {
                Jump();
            }
            else
            {
                if (CheckObjectByRayCast(_jumpCheck, _rayCastJumpLength, out _hit))
                {
                    _animator.SetBool(AnimationStates.IsJump, false);
                }
            }

            if (_direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction),
                    _rotateSpeed * Time.fixedDeltaTime);
                _animator.SetBool(AnimationStates.IsMove, true);
                _rigidbody.MovePosition(transform.position + _moveSpeed * Time.fixedDeltaTime * _direction);
            }
            else
            {
                _animator.SetBool(AnimationStates.IsMove, false);
            }
        }

        private void Jump()
        {
            if (!CheckObjectByRayCast(_jumpCheck, _rayCastJumpLength, out _hit)) return;
            _animator.SetBool(AnimationStates.IsJump, true);
            _rigidbody.AddForce(transform.up * _jumpSpeed, ForceMode.Impulse);
        }

        private bool CheckObjectByRayCast(Transform startPosition, float rayCastLength, out RaycastHit hit)
        {
            return Physics.Raycast(startPosition.position, startPosition.TransformDirection(Vector3.forward), out hit,
                rayCastLength, _layerMask);
        }
    }
}