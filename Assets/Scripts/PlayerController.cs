using UnityEngine;


namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private Vector3 _direction = Vector3.zero;

        private void Start()
        {
        }

        private void Update()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            var speed = _direction * _speed * Time.fixedDeltaTime;
            transform.Translate(speed);
        }
    }
}