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

    private void InitNextSpawnUnits()
    {
        nextSpawnUnitsList = new List<GameObject>();
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
                TileUnit randomPos = RandomEmptyTile();
                if(randomPos != null)
                {
                    posSpawn = randomPos.transform.position;
                } else
                {
                    gameOver = true;
                    GameOver();
                    return;
                }
                
            }
            playUnit.MovePlayUnit(posSpawn);
        }
        InitNextSpawnUnits();
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

    private void GameOver()
    {
        GameObject.Find("BestPlayerContainer").SetActive(true);
        GameObject.Find("PanelTop").SetActive(false);     
    }
}
