using System;
using Assets.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Menu")] [SerializeField] private GameObject _menuPanel = null;
        [SerializeField] private Button _startButton = null;
        [SerializeField] private Button _optionsButton = null;
        [SerializeField] private Button _quitButton = null;
        [Header("Options")] [SerializeField] private GameObject _optionsPanel = null;
        [SerializeField] private Toggle _muteToggle = null;
        [SerializeField] private Slider _volumeSlider = null;
        [SerializeField] private Button _saveButton = null;
        [SerializeField] private Button _backButton = null;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
            _optionsButton.onClick.AddListener(ShowOptions);
            _quitButton.onClick.AddListener(QuitGame);

            _muteToggle.onValueChanged.AddListener(Mute);
            _volumeSlider.onValueChanged.AddListener(ChangeVolume);
            _saveButton.onClick.AddListener(SaveOptions);
            _backButton.onClick.AddListener(ShowMenu);
        }

        private void ChangeVolume(float value)
        {
            throw new NotImplementedException();
        }

        private void SaveOptions()
        {
            throw new NotImplementedException();
        }

        private void Mute(bool value)
        {
            throw new NotImplementedException();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(Scenes.Game);
        }

        private void ShowOptions()
        {
            _menuPanel.SetActive(false);
            _optionsPanel.SetActive(true);
        }

        private void ShowMenu()
        {
            _optionsPanel.SetActive(false);
            _menuPanel.SetActive(true);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}