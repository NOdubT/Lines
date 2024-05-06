using UnityEngine;

public class TileUnit : MonoBehaviour
{
    public const int LEFT_TOP = 1;
    public const int TOP = 2;
    public const int RIGHT_TOP = 3;
    public GameObject LeftTopTile;
    public GameObject TopTile;
    public GameObject RightTopTile;

    public const int LEFT = 4;
    public const int RIGHT = 5;
    public GameObject LeftTile;
    public GameObject RightTile;

    public const int LEFT_BOTTOM = 6;
    public const int BOTTOM = 7;
    public const int RIGHT_BOTTOM = 8;
    public GameObject LeftBottomTile;
    public GameObject BottomTile;
    public GameObject RightBottomTile;

    private GameManager gameManager;

    public BallUnit ballUnit {  get; set; }

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        SetBall(true);
    }

    public void SetBall(bool swapnNext)
    {
        if (ballUnit == null && gameManager.activBallUnit != null)
        {
            ballUnit = gameManager.activBallUnit;
            ballUnit.MoveBall(this);
            gameManager.activBallUnit = null;

            if (swapnNext)
            {
                gameManager.SpawnNextBalls();
            }
        }
    }

    public void CheckLines()
    {

    }

    public GameObject GetNeighborTile(int direction)
    {
        switch (direction)
        {
            case LEFT_TOP: return LeftTopTile;
            case RIGHT_TOP: return RightTopTile;
            case LEFT_BOTTOM: return LeftBottomTile;
            case RIGHT_BOTTOM: return RightBottomTile;
            case TOP: return TopTile;
            case BOTTOM: return TopTile;
            case RIGHT: return RightTile;
            case LEFT: return LeftTile;

            default:
                return null;
        }
    }
}
