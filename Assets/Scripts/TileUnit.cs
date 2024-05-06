using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUnit : MonoBehaviour
{
    public GameObject LeftTopTile;
    public GameObject TopTile;
    public GameObject RightTopTile;

    public GameObject LeftTile;
    public GameObject RightTile;

    public GameObject LeftBottomTile;
    public GameObject BottomTile;
    public GameObject RightBottomTile;

    private GameManager gameManager;

    public bool isEmpty {  get; set; }

    private void Start()
    {
        isEmpty = true;

        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        SetBall(true);
    }

    public void SetBall(bool swapnNext)
    {
        if (isEmpty && gameManager.activBallUnit != null)
        {
            gameManager.activBallUnit.MoveBall(this);
            gameManager.activBallUnit = null;
            isEmpty = false;

            if (swapnNext)
            {
                gameManager.SpawnNextBalls();
            }           
        }
    }
}
