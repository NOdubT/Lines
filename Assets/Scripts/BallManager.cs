using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public List<GameObject> ballObjects;

    [SerializeField] int maxSpawnBall;
    [SerializeField] int range;
    [SerializeField] int tileScale;
    [SerializeField] int timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnBall = 3;
        range = 4;
        tileScale = 2;
        timeDelay = 1;

        StartCoroutine(SpawnBalls());
    }

    // Update is called once per frame
    IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeDelay);
            for (int i = 0; i < maxSpawnBall; i++)
            {
                int ball = Random.Range(0, ballObjects.Count);
                Vector3 pos = new Vector3(Random.Range(-range, range) * tileScale, 1, Random.Range(-range, range) * tileScale);
                Instantiate(ballObjects[ball], pos, gameObject.transform.rotation);
            }
        }
    }
}
