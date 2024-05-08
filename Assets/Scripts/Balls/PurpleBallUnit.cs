using UnityEngine;

public class PurpleBallUnit : PlayUnit
{
    public GameObject parent;

    public override void MovePlayUnit(Vector3 toPoint)
    {
        parent.transform.position = toPoint;
    }

    public override int UnitType()
    {
        return PURPLE;
    }
}
