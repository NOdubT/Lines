using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playUnits;
    public List<GameObject> nextPlayUnitTiles;

    public TextMeshProUGUI playerScoreText;
    private PlayerSettings playerSettings;

    private List<GameObject> nextSpawnUnitsList;

    public PlayUnit activePlayUnit { set; get; }
    public bool gameOver { set; get; }

    void Start()
    {
        playerSettings = GameObject.Find("Player").GetComponent<PlayerSettings>();

        nextSpawnUnitsList = new List<GameObject>();

        InitNextSpawnUnits();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnPlayUnits(Vector3.zero);
    }

    public void AddScore(int scoreToAdd)
    {
        playerSettings.player.Score += scoreToAdd;
        playerScoreText.text = $"Score: {playerSettings.player.Score}";
    }

    private void InitNextSpawnUnits()
    {
        foreach (GameObject tile in nextPlayUnitTiles)
        {
            GameObject go = Instantiate(playUnits[Random.Range(0, playUnits.Count)],
                tile.transform.position, gameObject.transform.rotation);
            TileUnit tileUnit = RandomEmptyTile();
            if (tileUnit != null)
            {
                go.GetComponentInChildren<PlayUnit>().playUnitPreview.transform.position = tileUnit.transform.position;
                tileUnit.gameObject.tag = TileUnit.tileTagUnitPreview;
            }
            else
            {
                go.GetComponentInChildren<PlayUnit>().playUnitPreview.transform.position = Vector3.down;
            }
            nextSpawnUnitsList.Add(go);
        }
    }

    public void SpawnPlayUnits(Vector3 notInPoint)
    {
        List<GameObject> goNotAdded = nextSpawnUnitsList.GetRange(0, nextSpawnUnitsList.Count);
        foreach (GameObject go in nextSpawnUnitsList)
        {
            if (SpawnNextUnit(go, notInPoint))
            {
                goNotAdded.Remove(go);
            }
        }
        
        if (goNotAdded.Count > 0)
        {
            foreach (GameObject go in goNotAdded)
            {
                SpawnNextUnit(go, notInPoint);
            }
        }

        if (RandomEmptyTile() == null)
        {
            gameOver = true;
            return;
        }

        nextSpawnUnitsList.Clear();
        InitNextSpawnUnits();
    }

    private bool SpawnNextUnit(GameObject go, Vector3 notInPoint)
    {
        PlayUnit playUnit = go.GetComponentInChildren<PlayUnit>();
        Vector3 posSpawn = playUnit.playUnitPreview.transform.position;
        if (posSpawn == notInPoint)
        {
            TileUnit randomPos = RandomEmptyTile();
            if (randomPos != null)
            {
                randomPos.tag = TileUnit.tileTagUnitPreview;
                posSpawn = randomPos.transform.position;
            }
            else
            {
                return false;
            }
        }
        playUnit.MovePlayUnit(posSpawn);
        return true;
    }

    private TileUnit RandomEmptyTile()
    {
        GameObject[] tileList = GameObject.FindGameObjectsWithTag(TileUnit.tileTagEmpty);
        if (tileList.Length > 0)
        {
            int index = Random.Range(0, tileList.Length);
            return tileList[index].GetComponent<TileUnit>();
        }
        return null;
    }
}
