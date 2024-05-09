using System.Collections;
using UnityEngine;

public class PlayUnit : MonoBehaviour
{
    public const int NONE = 0;
    public const int RED = 1;
    public const int YELLOW = 2;
    public const int PURPLE = 3;

    public GameObject playUnitPreview;

    public static string playUnitTag = "PlayUnit";

    protected GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameField").GetComponent<GameManager>();
    }

    public virtual void MovePlayUnit(Vector3 toPoint)
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.transform.position = toPoint;
        gameManager.activePlayUnit = null;
    }

    private void OnMouseDown()
    {
        if (!playUnitPreview.activeSelf)
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
