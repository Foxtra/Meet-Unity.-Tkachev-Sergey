using Assets.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        [Header("Menu")] [SerializeField] private GameObject _pausePanel = null;
        [SerializeField] private Button _continueButton = null;
        [SerializeField] private Button _menuButton = null;
        [SerializeField] private Button _quitButton = null;

        [Header("EndGame")] [SerializeField] private GameObject _endGamePanel = null;
        [SerializeField] private Button _endGameMenuButton = null;
        [SerializeField] private Button _endGameQuitButton = null;

        [SerializeField] private GameObject _boss = null;

        private EnemyController _bossController;

        private void Awake()
        {
            _bossController = _boss.GetComponent<EnemyController>();

            _continueButton.onClick.AddListener(ContinueGame);
            _menuButton.onClick.AddListener(GoToMenu);
            _quitButton.onClick.AddListener(QuitGame);

            _endGameMenuButton.onClick.AddListener(GoToMenu);
            _endGameQuitButton.onClick.AddListener(GoToMenu);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopGame();
            }

            if (!_bossController.IsAlive)
            {
                Invoke(nameof(ShowEndGameScreen), 2);
            }
        }

        private void ShowEndGameScreen()
        {
            _endGamePanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void ContinueGame()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void StopGame()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        private void GoToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(Scenes.Menu);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}