using UnityEngine;

public class TileUnit : MonoBehaviour
{
    private GameManager gameManager;

    public PlayUnit playUnit { get; set; }
    public int pathWeight { get; set; }

    void Start()
    {
        pathWeight = 0;
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if(gameManager.activePlayUnit != null)
        {
            gameManager.activePlayUnit.MovePlayUnit(transform.position + Vector3.up);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayUnit"))
        {
            playUnit = other.GetComponent<PlayUnit>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayUnit"))
        {
            playUnit = null;
        }
    }
}
