using UnityEngine;


namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform[] SpawnPlaces;
        public GameObject Enemy;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                foreach (var SpawnPlace in SpawnPlaces)
                {
                    Instantiate(Enemy, SpawnPlace.position, transform.rotation);
                }
            }
        }
    }
}