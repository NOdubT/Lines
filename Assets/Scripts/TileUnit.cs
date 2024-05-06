using UnityEditorInternal;
using UnityEngine;

public class TileUnit : MonoBehaviour
{
    public const int NONE = 0;

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
    private int ballsInLine = 5;

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

            if (!CheckBalls())
            {
                if (swapnNext)
                {
                    gameManager.SpawnNextBalls();
                }
            }
        }
    }

    private bool CheckBalls()
    {
        int countLB = CountBallsInLine(LEFT_BOTTOM);
        int countB = CountBallsInLine(BOTTOM);
        int countRB = CountBallsInLine(RIGHT_BOTTOM);
        int countR = CountBallsInLine(RIGHT);

        int addScore = 0;
        if(countLB > ballsInLine)
        {
            addScore += countLB - 1;
            RemoveBallsInLine(LEFT_BOTTOM);
        }

        if (countB > ballsInLine)
        {
            addScore += countB - 1;
            RemoveBallsInLine(BOTTOM);
        }

        if (countRB > ballsInLine)
        {
            addScore += countRB - 1;
            RemoveBallsInLine(RIGHT_BOTTOM);
        }

        if (countR > ballsInLine)
        {
            addScore += countR - 1;
            RemoveBallsInLine(RIGHT);
        }

        if (addScore > 0)
        {
            Destroy(ballUnit.gameObject);
            gameManager.AddScore(addScore);
            return true;
        }
        return false;
    }

    private void RemoveBallsInLine(int direction)
    {
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, ballUnit.UnitType());
        }

        direction = OposideDirection(direction);
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, ballUnit.UnitType());
        }
    }

    private void RemoveBalls(int direction, int balltype)
    {
        if (ballUnit != null && ballUnit.UnitType() == balltype)
        {
            if(NeighborTile(direction) != null)
            {
                NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, balltype);
            }
            Destroy(ballUnit.gameObject);
        }
    }

    private int CountBallsInLine(int direction)
    {
        int countR = CountBalls(direction, ballUnit.UnitType());

        direction = OposideDirection(direction);
        countR += CountBalls(direction, ballUnit.UnitType());
        return countR;
    }

    private int CountBalls(int direction, int balltype)
    {
        if(ballUnit != null && balltype == ballUnit.UnitType())
        {
            if(NeighborTile(direction) != null)
            {
                return NeighborTile(direction).GetComponent<TileUnit>().CountBalls(direction, balltype) + 1;
            }
            return 1;
        }
        return 0;
    }

    private int OposideDirection(int direction)
    {
        switch (direction)
        {
            case LEFT_TOP: return RIGHT_BOTTOM;
            case RIGHT_TOP: return LEFT_BOTTOM;
            case LEFT_BOTTOM: return RIGHT_TOP;
            case RIGHT_BOTTOM: return LEFT_TOP;
            case TOP: return BOTTOM;
            case BOTTOM: return TOP;
            case RIGHT: return LEFT;
            case LEFT: return RIGHT;

            default:
                return NONE;
        }
    }

    private GameObject NeighborTile(int direction)
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
