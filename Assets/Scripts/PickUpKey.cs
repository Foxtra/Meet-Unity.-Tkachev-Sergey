using UnityEngine;


namespace Assets.Scripts
{
    public class PickUpKey : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}