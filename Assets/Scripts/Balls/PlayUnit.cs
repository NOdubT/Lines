using UnityEngine;

public class PlayUnit : MonoBehaviour
{
    public const int NONE = 0;
    public const int RED = 1;
    public const int YELLOW = 2;
    public const int PURPLE = 3;

    public static string playUnitTag = "PlayUnit";

    private GameManager gameManager;

    public bool isNextSpawn {  get; set; }

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    public virtual void MovePlayUnit(Vector3 toPoint)
    {
        gameObject.transform.position = toPoint;
    }

    private void OnMouseDown()
    {
        if (!isNextSpawn)
        {
            if(gameManager.activePlayUnit != null)
            {
                gameManager.activePlayUnit.GetComponent<Animator>().enabled = false;
            }
            gameObject.GetComponent<Animator>().enabled = true;
            gameManager.activePlayUnit = this;
        }
    }

    public virtual int UnitType()
    {
        return NONE;
    }
}
