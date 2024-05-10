using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField] List<GameObject> canMoveTo;

    public PlayUnit playUnit { get; set; }
    public bool inPath { get; set; }

    private void Start()
    {
        inPath = false;
        if (TopTile != null) canMoveTo.Add(TopTile);
        if (RightTile != null) canMoveTo.Add(RightTile);
        if (LeftTile != null) canMoveTo.Add(LeftTile);
        if (BottomTile != null) canMoveTo.Add(BottomTile);
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.activePlayUnit != null && CanMove(gameManager.activePlayUnit.transform.position))
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
            ClearTile(null);
        }
    }

    private void ClearTile(GameObject unitToDestroy)
    {
        gameObject.tag = tileTagEmpty;
        if (unitToDestroy != null)
        {
            Destroy(unitToDestroy);
        }
        playUnit = null;
    }

    IEnumerator CheckTag()
    {
        yield return new WaitForSeconds(0.2f);
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
        if (countLB > ballsInLine)
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
            ClearTile(playUnit.gameObject);
            gameManager.AddScore(CountScore(totalCount));
        }
    }

    private int CountScore(int totalCount)
    {
        if (totalCount > 5)
        {
            totalCount = 5 + (totalCount - 5) * 2;
        }
        return totalCount;
    }

    private void RemoveUnitsInLine(int direction)
    {
        if (NeighborTile(direction) != null)
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, playUnit.UnitType());
        }

        direction = OposideDirection(direction);
        if (NeighborTile(direction) != null)
        {
            NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, playUnit.UnitType());
        }
    }

    private void RemoveUnits(int direction, int balltype)
    {
        if (playUnit != null && playUnit.UnitType() == balltype)
        {
            if (NeighborTile(direction) != null)
            {
                NeighborTile(direction).GetComponent<TileUnit>().RemoveUnits(direction, balltype);
            }
            ClearTile(playUnit.gameObject);
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
        if (playUnit != null && playUnit.UnitType() == balltype)
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

    private bool CanMove(Vector3 toPoint)
    {
        toPoint += Vector3.down * toPoint.y;
        List<TileUnit> tilesInPath = new List<TileUnit>();
        tilesInPath.Add(this);
        tilesInPath = AddPathCircle(tilesInPath, toPoint);

        foreach (TileUnit tile in tilesInPath)
        {
            tile.inPath = false;
        }

        return tilesInPath.Last().transform.position == toPoint;
    }

    private List<TileUnit> AddPathCircle(List<TileUnit> tilesInCircle, Vector3 toPoint)
    {
        if (tilesInCircle.Last().transform.position == toPoint)
        {
            return tilesInCircle;
        }

        List<TileUnit> tilesInNextCircle = new List<TileUnit>();
        foreach (TileUnit tile in tilesInCircle)
        {
            tilesInNextCircle.AddRange(tile.BuildPath(toPoint));
            if (tilesInNextCircle.Count > 0 && tilesInNextCircle.Last().transform.position == toPoint)
            {
                tilesInCircle.AddRange(tilesInNextCircle);
                return tilesInCircle;
            }
        }

        if (tilesInNextCircle.Count > 0)
        {
            tilesInCircle.AddRange(AddPathCircle(tilesInNextCircle, toPoint));
        }

        return tilesInCircle;
    }

    private List<TileUnit> BuildPath(Vector3 toPoint)
    {
        List<TileUnit> tilesInPath = new List<TileUnit>();

        foreach (GameObject go in canMoveTo)
        {
            if (AddTileToPath(go, toPoint))
            {
                tilesInPath.Add(go.GetComponent<TileUnit>());
                if (go.transform.position == toPoint)
                {
                    break;
                }
            }
        }

        return tilesInPath;
    }

    private bool AddTileToPath(GameObject tileUnit, Vector3 toPoint)
    {
        if (tileUnit != null)
        {
            if (!tileUnit.CompareTag(tileTagFull))
            {
                if (!tileUnit.GetComponent<TileUnit>().inPath)
                {
                    tileUnit.GetComponent<TileUnit>().inPath = true;
                    return true;
                }
            }
            else
            {
                if (tileUnit.transform.position == toPoint)
                {
                    tileUnit.GetComponent<TileUnit>().inPath = true;
                    return true;
                }
            }

        }
        return false;
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
