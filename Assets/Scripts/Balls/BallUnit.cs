using UnityEngine;

public class BallUnit : MonoBehaviour
{
    private GameManager gameManager;
    private TileUnit c_tileUnit;
    private float offsetY = 1;

    private void Awake()
    {
        c_tileUnit = GameObject.Find("TileNextBall").GetComponent<TileUnit>();
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    public void MoveBall(TileUnit tileUnit)
    {
        c_tileUnit.isEmpty = true;
        c_tileUnit = tileUnit;
        Vector3 pos = tileUnit.transform.position;
        pos.y += offsetY;
        gameObject.transform.position = pos;
    }

    private void OnMouseDown()
    {
        if (c_tileUnit.CompareTag("Tile"))
        {
            gameManager.activBallUnit = this;
        }
    }

    public virtual BallDict UnitType()
    {
        return BallDict.NONE;
    }
}
