using UnityEngine;


namespace Assets.Scripts
{
    public class PickUpHealth : MonoBehaviour
    {
        [SerializeField] private int _healthToRestore = 2;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                var player = collider.GetComponent<PlayerController>();
                if (player.TakeHealth(_healthToRestore))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}