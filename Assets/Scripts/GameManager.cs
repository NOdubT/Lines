using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playUnits;
    public List<GameObject> nextPlayUnitTiles;

    public TextMeshProUGUI playerScoreText;
    private PlayerSettings playerSettings;

    private GameObject[] _tileList;
    private List<GameObject> nextSpawnUnitsList;

    public PlayUnit activePlayUnit { set; get; }
    public bool gameOver { set; get; }

    void Start()
    {
        _tileList = GameObject.FindGameObjectsWithTag(TileUnit.tileUnitTag);
        playerSettings = GameObject.Find(PlayerSettings.player).GetComponent<PlayerSettings>();

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
        playerSettings.playerScore += scoreToAdd;
        playerScoreText.text = $"Score: {playerSettings.playerScore}";
    }

    public void InitNextSpawnUnits()
    {
        nextSpawnUnitsList = new List<GameObject>();
        List<GameObject> c_tileList = _tileList.ToList<GameObject>();
        foreach (GameObject tile in nextPlayUnitTiles)
        {
            GameObject go = Instantiate(playUnits[Random.Range(0, playUnits.Count)],
                tile.transform.position, gameObject.transform.rotation);
            TileUnit tileUnit = RandomEmptyTile(c_tileList);
            if (tileUnit != null)
            {
                go.GetComponentInChildren<PlayUnit>().playUnitPreview.transform.position = tileUnit.transform.position;
                c_tileList.Remove(tileUnit.gameObject);
            }
            nextSpawnUnitsList.Add(go);
        }
    }

    public void SpawnPlayUnits(Vector3 notInPoint)
    {
        foreach(GameObject go in nextSpawnUnitsList)
        {
            PlayUnit playUnit = go.GetComponentInChildren<PlayUnit>();
            Vector3 posSpawn = playUnit.playUnitPreview.transform.position;
            if (posSpawn == notInPoint)
            {
                posSpawn = RandomEmptyTile(_tileList.ToList<GameObject>()).transform.position;
            }
            playUnit.MovePlayUnit(posSpawn);
        }
        InitNextSpawnUnits();
    }

    private TileUnit RandomEmptyTile(List<GameObject> tileList)
    {
        if (tileList.Count > 0)
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
