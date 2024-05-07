using UnityEngine;

public class TileNextUnit : MonoBehaviour
{
    public static string tileNextUnitTag = "TileNextUnit";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            other.GetComponent<PlayUnit>().isNew = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayUnit.playUnitTag))
        {
            other.GetComponent<PlayUnit>().isNew = false;
        }
    }
}
