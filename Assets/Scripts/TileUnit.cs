using System.Collections;
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

    public static string tileUnitTag = "Tile";

    public PlayUnit playUnit { get; set; }
    public int pathWeight { get; set; }

    private void Start()
    {
        pathWeight = 0;
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.activePlayUnit != null)
        {
            gameManager.activePlayUnit.MovePlayUnit(transform.position);
            StartCoroutine(CheckBalls());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            playUnit = other.GetComponent<PlayUnit>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            playUnit = null;
        }
    }

    IEnumerator CheckBalls()
    {
        yield return new WaitForSeconds(0.05f);
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
            Destroy(playUnit.gameObject);
            gameManager.AddScore(addScore);
        } else
        {
            gameManager.SpawnPlayUnits();
        }
    }

    private void RemoveBallsInLine(int direction)
    {
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, playUnit.UnitType());
        }

        direction = OposideDirection(direction);
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, playUnit.UnitType());
        }
    }

    private void RemoveBalls(int direction, int balltype)
    {
        if (playUnit != null && playUnit.UnitType() == balltype)
        {
            if(NeighborTile(direction) != null)
            {
                NeighborTile(direction).GetComponent<TileUnit>().RemoveBalls(direction, balltype);
            }
            Destroy(playUnit.gameObject);
        }
    }

    private int CountBallsInLine(int direction)
    {
        int count = CountBalls(direction, playUnit.UnitType());

        direction = OposideDirection(direction);
        count += CountBalls(direction, playUnit.UnitType());
        return count;
    }

    private int CountBalls(int direction, int balltype)
    {
        if(playUnit != null && playUnit.UnitType() == balltype)
        {
            GameObject n_Tile = NeighborTile(direction);
            if (n_Tile != null)
            {
                return n_Tile.GetComponent<TileUnit>().CountBalls(direction, balltype) + 1;
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
            case BOTTOM: return BottomTile;
            case RIGHT: return RightTile;
            case LEFT: return LeftTile;

            default:
                return null;
        }
    }
}
