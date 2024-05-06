using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> ballObjects;
    public TextMeshProUGUI playerScoreText;
    public GameObject nextBallTile;

    private int maxSpawnUnit;  
    private GameObject[] gameField;
    private PlayerSettings playerSettings;
    private Vector3 offset;

    public BallUnit activBallUnit { set; get; }
    public List<GameObject> nextBallObjects {  set; get; }

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnUnit = 3;
        offset = new Vector3(0, 1, -0.5f);
        nextBallObjects = new List<GameObject>();

        gameField = GameObject.FindGameObjectsWithTag("Tile");
        playerSettings = GameObject.Find("Player").GetComponent<PlayerSettings>();

        NextBallObjects();
        SpawnNextBalls();
    }

    public void AddScore(int scoreToAdd)
    {
        playerSettings.playerScore += scoreToAdd;
        playerScoreText.text = $"Score: {playerSettings.playerScore}";
    }

    private void NextBallObjects()
    {
        nextBallObjects.Clear();
        for (int i = 0; i < maxSpawnUnit; i++)
        {
            int unit = Random.Range(0, ballObjects.Count);
            Vector3 pos = nextBallTile.transform.position + offset + Vector3.right * i * nextBallTile.transform.localScale.x;
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
        if (allTiles[tile].GetComponent<TileUnit>().ballUnit == null)
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
