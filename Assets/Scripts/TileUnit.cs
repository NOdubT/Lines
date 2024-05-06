using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUnit : MonoBehaviour
{
    private GameManager gameManager;

    public bool isEmpty;

    private void Start()
    {
        isEmpty = true;

        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.activBallUnit != null)
        {
            gameManager.activBallUnit.MoveBall(gameObject.transform.position);
            gameManager.activBallUnit = null;
        }
    }
}
