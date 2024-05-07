using UnityEngine;

public class TileNextUnit : MonoBehaviour
{
    public static string tileNextUnitTag = "TileNextUnit";

    public PlayUnit playUnit { get; set; }
    public int pathWeight { get; set; }

    private void Start()
    {
        pathWeight = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            playUnit = other.GetComponent<PlayUnit>();
            playUnit.isNew = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            playUnit.isNew = false;
            playUnit = null;
        }
    }
}
