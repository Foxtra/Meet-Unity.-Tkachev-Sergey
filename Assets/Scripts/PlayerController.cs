using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private Vector3 _direction = Vector3.zero;
        [SerializeField] private GameObject _fireBall;
        [SerializeField] private Transform _firePosition;

        private bool _fire;

        private void Start()
        {
        }

        private void Update()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
                _fire = true;
            }
        }

        private void FixedUpdate()
        {
            var speed = _direction * _speed * Time.fixedDeltaTime;
            transform.Translate(speed);

            if (_fire)
            {
                Fire();
            }
        }

        private void Fire()
        {
            Instantiate(_fireBall, _firePosition.position, _firePosition.rotation);
            _fire = false;
        }
    }
}