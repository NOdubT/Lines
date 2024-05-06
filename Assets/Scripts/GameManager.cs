using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> ballObjects;

    [SerializeField] int maxSpawnUnit;
    [SerializeField] int range;
    [SerializeField] int tileScale;

    public BallUnit activBallUnit;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnUnit = 3;
        range = 4;
        tileScale = 2;

        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i < maxSpawnUnit; i++)
            {
                int ball = Random.Range(0, ballObjects.Count);
                Vector3 pos = new Vector3(Random.Range(-range, range) * tileScale, 1, Random.Range(-range, range) * tileScale);
                Instantiate(ballObjects[ball], pos, gameObject.transform.rotation);
            }
        }
    }
}
