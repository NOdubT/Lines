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
        SpawnPlayUnits();
    }

    public void AddScore(int scoreToAdd)
    {
        playerSettings.playerScore += scoreToAdd;
        playerScoreText.text = $"Score: {playerSettings.playerScore}";
    }

    public void InitNextSpawnUnits()
    {
        nextSpawnUnitsList = new List<GameObject>();
        foreach (GameObject tile in nextPlayUnitTiles)
        {
            GameObject go = Instantiate(playUnits[Random.Range(0, playUnits.Count)],
                tile.transform.position, gameObject.transform.rotation);
            TileUnit tileUnit = RandomEmptyTile(_tileList.ToList<GameObject>());
            if (tileUnit != null)
            {
                go.GetComponentInChildren<PlayUnit>().playUnitPreview.transform.position = tileUnit.transform.position;
            }
            nextSpawnUnitsList.Add(go);
        }
    }

    public void SpawnPlayUnits()
    {
        foreach(GameObject playUnit in nextSpawnUnitsList)
        {
            playUnit.transform.position = playUnit.GetComponentInChildren<PlayUnit>().playUnitPreview.transform.position;
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
