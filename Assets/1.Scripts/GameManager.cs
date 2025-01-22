using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("CarRef")]
    [SerializeField] private CarController carController;

    [Header("UI Start")]
    [SerializeField] private GameObject uiStart;
    [SerializeField] private Button btnStart;

    [Header("UI GameOver")]
    [SerializeField] private GameObject uiGameOver;
    [SerializeField] private Button btnReStart;
    [SerializeField] private Button btnQuit;
    [SerializeField] private TextMeshProUGUI textScore;

    private void Awake()
    {
        uiGameOver.SetActive(false);

        instance = this;
        Time.timeScale = 0;
    }

    public void OnBtnStart()
    {
        uiStart.SetActive(false);
        carController.uiInGame.SetActive(true);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        carController.uiInGame.SetActive(false);
        uiGameOver.SetActive(true);
        textScore.text = "Score:" + carController.score.ToString();
    }

    public void OnBtnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnBtnQuit()
    {
        Application.Quit();
    }

}
