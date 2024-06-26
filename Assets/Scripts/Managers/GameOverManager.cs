﻿using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject statisticsPanel;
        private bool isGameOver = false;
        void Start()
        {
            statisticsPanel.SetActive(false);
        }

        void Update()
        {
            if (HealthSystem.Instance != null && HealthSystem.Instance.isDeath && !isGameOver){
                gameOverPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isGameOver = true;
            }
        }

        public void Statistics()
        {
            gameOverPanel.SetActive(false);
            statisticsPanel.SetActive(true);
        }
        public void BackToGameOver()
        {
            gameOverPanel.SetActive(true);
            statisticsPanel.SetActive(false);
        }
        public void Restart()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            HealthSystem.Instance.SetIsDeath(false);
            gameOverPanel.SetActive(false);
            isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GameOver()
        {
            HealthSystem.Instance.SetIsDeath(false);
            gameOverPanel.SetActive(false);
            isGameOver = false;
            if(GameManager.INSTANCE.currentQuestID == 0){
                SceneManager.LoadScene(1);
            }else{
                SceneManager.LoadScene(4);
            }
        }
    }
}