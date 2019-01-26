using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERotateAxis
{
    X,
    Y,
    Z,
}

public class RoomRotator : MonoBehaviour
{
    public void Rotate(int angle, ERotateAxis axis)
    {
        var a = this.transform.localEulerAngles;
        if (axis == ERotateAxis.X)
        {
            this.transform.Rotate(angle, 0, 0);
        }
        else if (axis == ERotateAxis.Y)
        {
            this.transform.Rotate(0, angle, 0);
        }
        else if (axis == ERotateAxis.Z)
        {
            this.transform.Rotate(0, 0, angle);
        }
    }
}
