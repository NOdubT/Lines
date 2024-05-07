using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playUnits;
    public List<GameObject> nextPlayUnitTiles;

    private GameObject[] _tileList;
    private float timeDelay;

    private List<GameObject> nextSpawnUnitsList;

    public PlayUnit activePlayUnit { set; get; }
    public bool gameOver { set; get; }

    private void Start()
    {
        timeDelay = 0.05f;
        _tileList = GameObject.FindGameObjectsWithTag("Tile");
        InitNextSpawnUnits();
    }

    public void InitNextSpawnUnits()
    {
        nextSpawnUnitsList = new List<GameObject>();
        foreach (GameObject tile in nextPlayUnitTiles)
        {
            nextSpawnUnitsList.Add(Instantiate(playUnits[Random.Range(0, playUnits.Count)],
                tile.transform.position + Vector3.up, gameObject.transform.rotation));
        }
    }

    public void SpawnPlayUnits()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeDelay);
        foreach (var spawnUnit in nextSpawnUnitsList)
        {
            TileUnit tileUnit = RandomEmptyTile(_tileList.ToList<GameObject>());
            if (tileUnit != null)
            {
                spawnUnit.transform.position = tileUnit.transform.position + Vector3.up;
            }
            else
            {
                gameOver = true;
            }
            yield return new WaitForSeconds(timeDelay);
        }
        InitNextSpawnUnits();
    }

    private TileUnit RandomEmptyTile(List<GameObject> tileList)
    {
        if(tileList.Count > 0)
        {
            int index = Random.Range(0, tileList.Count);
            TileUnit tileUnit = tileList[index].GetComponent<TileUnit>();
            if (tileUnit.playUnit != null)
            {
                tileList.RemoveAt(index);
                return RandomEmptyTile(tileList);
            }
            return tileUnit;
        }
        return null;
    }

}
