using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class InfoUiController : MonoBehaviour
    {
        [SerializeField] private Image _healthImage = null;
        [SerializeField] private GameObject _player = null;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = _player.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        private void Update()
        {
            _healthImage.fillAmount = (float) _playerController.CurrentHealth / 100 * _playerController.MaxHealth;
        }
    }
}