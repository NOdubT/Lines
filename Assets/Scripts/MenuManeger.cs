using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManeger : MonoBehaviour
{
    public GameObject TopContainer;
    public GameObject RightContainer;
    public GameObject BestPlayersContainer;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        TopContainer.SetActive(false);
        BestPlayersContainer.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
