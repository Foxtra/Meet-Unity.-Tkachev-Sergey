using UnityEngine;


namespace Assets.Scripts
{
    public class DoorsManager : MonoBehaviour
    {
        public GameObject Key1;
        public GameObject Key2;
        public GameObject Key3;
        public GameObject Key4;

        private void Update()
        {
            if (Key1 == null)
            {
                var door1 = transform.GetChild(0).gameObject;
                door1.SetActive(false);
            }

            if (Key2 == null)
            {
                var door2 = transform.GetChild(1).gameObject;
                door2.SetActive(false);
            }

            if (Key3 == null && Key4 == null)
            {
                var door3 = transform.GetChild(2).gameObject;
                door3.SetActive(false);
            }
        }
    }
}