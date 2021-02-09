using UnityEngine;


namespace Assets.Scripts
{
    public class TrapCutterController : MonoBehaviour
    {
        [SerializeField] private int _damage = 2;

        private Animation _anim;

        private void Awake()
        {
            _anim = gameObject.GetComponentInParent<Animation>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_anim.isPlaying)
                {
                    var player = other.gameObject.GetComponent<PlayerController>();
                    player.TakeDamage(_damage);
                }
            }

            if (other.gameObject.CompareTag("Rock"))
            {
                _anim.Stop();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Rock"))
            {
                _anim.Play();
            }
        }
    }
}