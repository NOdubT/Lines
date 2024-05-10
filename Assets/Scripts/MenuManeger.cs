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
        PlayerSettings.instance.CheckScore();
        for(int i = 0; i < PlayerSettings.instance.bestScore.Count; i++)
        {
            BestPlayersText[i].text = $"{PlayerSettings.instance.bestScore[i].Name}: {PlayerSettings.instance.bestScore[i].Score}";
        }
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
