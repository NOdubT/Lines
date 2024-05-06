using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUnit : MonoBehaviour
{
    private GameManager gameManager;

    private float posY;

    private void Start()
    {
        posY = 1;

        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    public void MoveBall(Vector3 pos)
    {
        pos.y = posY;
        gameObject.transform.position = pos;
    }

    private void OnMouseDown()
    {
        gameManager.activBallUnit = this;
    }
}
