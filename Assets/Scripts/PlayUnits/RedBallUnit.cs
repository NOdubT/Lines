using UnityEngine;

public class RedBallUnit : PlayUnit
{
    public GameObject parent;

    public override void MovePlayUnit(Vector3 toPoint)
    {
        parent.transform.position = toPoint;
        base.MovePlayUnit(toPoint);
    }

    public override int UnitType()
    {
        return RED;
    }
}
