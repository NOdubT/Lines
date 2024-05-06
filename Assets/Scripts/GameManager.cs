using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> ballObjects;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerScoreText;

    private int maxSpawnUnit;  
    private GameObject[] gameField;
    private PlayerSettings playerSettings;

    public BallUnit activBallUnit { set; get; }
    public List<GameObject> nextBallObjects {  set; get; }

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnUnit = 3;
        nextBallObjects = new List<GameObject>();

        gameField = GameObject.FindGameObjectsWithTag("Tile");
        playerSettings = GameObject.Find("Player").GetComponent<PlayerSettings>();
        playerNameText.text = playerSettings.playerName;

        NextBallObjects();
        SpawnNextBalls();
    }

    public void AddScore(int score)
    {
        playerSettings.playerScore += score;
        playerScoreText.text = $"Score: {playerSettings.playerScore}";
    }

    private void NextBallObjects()
    {
        nextBallObjects.Clear();
        for (int i = 0; i < maxSpawnUnit; i++)
        {
            int unit = Random.Range(0, ballObjects.Count);
            Vector3 pos = new Vector3(-2 + 2 * i, 0, 10.5f);
            nextBallObjects.Add(Instantiate(ballObjects[unit], pos, gameObject.transform.rotation));
        }
    }

    public void SpawnNextBalls()
    {        
        foreach (GameObject ball in nextBallObjects)
        {
            activBallUnit = ball.GetComponent<BallUnit>();
            RandomEmptyTile(gameField.ToList<GameObject>()).SetBall(false);
        }
        NextBallObjects();
    }

    private TileUnit RandomEmptyTile(List<GameObject> allTiles)
    {
        int tile = Random.Range(0, allTiles.Count);
        if (allTiles[tile].GetComponent<TileUnit>().isEmpty)
        {
            return allTiles[tile].GetComponent<TileUnit>();
        }
        else
        {
            allTiles.RemoveAt(tile);
            return RandomEmptyTile(allTiles);
        }       
    }
}
