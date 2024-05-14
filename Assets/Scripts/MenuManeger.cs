using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManeger : MonoBehaviour
{
    public GameObject TopContainer;
    public GameObject RightContainer;
    public GameObject BestPlayersContainer;

    public TMP_InputField NameInput;

    public List<TextMeshProUGUI> BestPlayersText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
        NameInput.text = PlayerSettings.instance.player.Name;
        SetBestPlayer();
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
