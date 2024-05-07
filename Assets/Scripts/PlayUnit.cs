using UnityEngine;

public class PlayUnit : MonoBehaviour
{
    private GameManager gameManager;

    private Vector3 startPoint = new Vector3(0, 1, 12);

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    public void MovePlayUnit(Vector3 toPoint)
    {
        gameObject.transform.position = toPoint;
        gameManager.SpawnPlayUnits();
    }

    private void OnMouseDown()
    {
        gameManager.activePlayUnit = this;
    }
}
