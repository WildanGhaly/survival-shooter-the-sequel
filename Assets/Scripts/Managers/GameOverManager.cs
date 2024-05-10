using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject fade;
        void Update()
        {
            if (HealthSystem.Instance != null && HealthSystem.Instance.isDeath){
                gameOverPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        public void Restart()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            HealthSystem.Instance.SetIsDeath(false);
            gameOverPanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GameOver()
        {
            HealthSystem.Instance.SetIsDeath(false);
            gameOverPanel.SetActive(false);
            fade.SetActive(true);
            SceneManager.LoadScene(3);
        }
    }
}