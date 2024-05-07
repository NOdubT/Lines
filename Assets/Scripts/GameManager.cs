using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playUits;

    private int maxPlayUnitSpawn = 3;
    private GameObject[] _tileList;
    public PlayUnit activePlayUnit { set; get; }
    public bool gameOver { set; get; }

    private void Start()
    {
        _tileList = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void SpawnPlayUnits()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < maxPlayUnitSpawn; i++)
        {
            GameObject go = Instantiate(playUits[Random.Range(0, playUits.Count)]);
            TileUnit tileUnit = RandomEmptyTile(_tileList.ToList<GameObject>());
            if (tileUnit != null)
            {
                go.transform.position = tileUnit.transform.position + Vector3.up;
            }
            else
            {
                gameOver = true;
            }
        }
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
