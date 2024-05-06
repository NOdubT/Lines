using UnityEngine;

public class BallUnit : MonoBehaviour
{
    public const int NONE = 0;
    public const int RED = 1;
    public const int YELLOW = 2;
    public const int PURPLE = 3;

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
        c_tileUnit.ballUnit = null;
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

    public virtual int UnitType()
    {
        return NONE;
    }
}
