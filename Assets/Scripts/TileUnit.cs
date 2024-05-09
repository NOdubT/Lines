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

    public static string tileTagEmpty = "TileEmpty";
    public static string tileTagUnitPreview = "TileUnitPreview";
    public static string tileTagFull = "TileFull";

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
            StartCoroutine(CheckTag());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            playUnit = other.GetComponent<PlayUnit>();
            gameObject.tag = tileTagFull;
            CheckUnits();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            ClearTile();
        }
    }

    private void ClearTile()
    {
        gameObject.tag = tileTagEmpty;
        playUnit = null;
    }

    IEnumerator CheckTag()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameObject.CompareTag(tileTagFull))
        {
            gameManager.SpawnPlayUnits(transform.position);
        }
    }

    private void CheckUnits()
    {
        int countLB = CountUnitsInLine(LEFT_BOTTOM);
        int countB = CountUnitsInLine(BOTTOM);
        int countRB = CountUnitsInLine(RIGHT_BOTTOM);
        int countR = CountUnitsInLine(RIGHT);

        int totalCount = 0;
        if(countLB > ballsInLine)
        {
            totalCount += countLB - 1;
            RemoveUnitsInLine(LEFT_BOTTOM);
        }

        if (countB > ballsInLine)
        {
            totalCount += countB - 1;
            RemoveUnitsInLine(BOTTOM);
        }

        if (countRB > ballsInLine)
        {
            totalCount += countRB - 1;
            RemoveUnitsInLine(RIGHT_BOTTOM);
        }

        if (countR > ballsInLine)
        {
            totalCount += countR - 1;
            RemoveUnitsInLine(RIGHT);
        }

        if (totalCount > 0)
        {
            Destroy(playUnit.gameObject);
            gameManager.AddScore(CountScore(totalCount));
        }
    }

    private int CountScore(int totalCount)
    {
        if(totalCount > 5)
        {
            totalCount = 5 + (totalCount - 5) * 2;
        }
        return totalCount;
    }

    private void RemoveUnitsInLine(int direction)
    {
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, playUnit.UnitType());
        }

        direction = OposideDirection(direction);
        if (NeighborTile(direction) != null) // remove with out this
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, playUnit.UnitType());
        }
    }

    private void RemoveUnits(int direction, int balltype)
    {
        if (playUnit != null && playUnit.UnitType() == balltype)
        {
            if(NeighborTile(direction) != null)
            {
                NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, balltype);
            }
            Destroy(playUnit.gameObject);
            ClearTile();
        }
    }

    private int CountUnitsInLine(int direction)
    {
        int count = CountUnits(direction, playUnit.UnitType());

        direction = OposideDirection(direction);
        count += CountUnits(direction, playUnit.UnitType());
        return count;
    }

    private int CountUnits(int direction, int balltype)
    {
        if(playUnit != null && playUnit.UnitType() == balltype)
        {
            GameObject n_Tile = NeighborTile(direction);
            if (n_Tile != null)
            {
                return n_Tile.GetComponent<TileUnit>().CountUnits(direction, balltype) + 1;
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
