using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManeger : MonoBehaviour
{
    public GameObject TopContainer;
    public GameObject RightContainer;
    public GameObject BestPlayersContainer;

    public List<TextMeshProUGUI> BestPlayersText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
        SetBestPlayer();
    }

    private void Update()
    {
        if (gameManager.gameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameManager.gameOver = true;
        PlayerSettings.instance.CheckScore();
        SetBestPlayer();
        TopContainer.SetActive(false);
        BestPlayersContainer.SetActive(true);
    }

    private void SetBestPlayer()
    {
        for (int i = 0; i < PlayerSettings.instance.bestScore.Count; i++)
        {
            string text = PlayerSettings.instance.bestScore[i].Name + ": " + PlayerSettings.instance.bestScore[i].Score;
            BestPlayersText[i].text = text;
        }
    }

    public void BestScore()
    {
        BestPlayersContainer.SetActive(!BestPlayersContainer.activeSelf);
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
