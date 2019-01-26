using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ERotateAxis
{
    X,
    Y,
    Z,
}

public class RoomRotator : MonoBehaviour
{
    bool _IsPlayingAnim;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Rotate(ERotateAxis axis)
    {
        if (_IsPlayingAnim)
        {
            return;
        }
        StartCoroutine(RotateRoutine(axis, (int)(ConstValue._RoomRotateSpeed)));
    }

    IEnumerator RotateRoutine(ERotateAxis axis, int delta)
    {
        _IsPlayingAnim = true;
        int value = 0;
        while (value < 90)
        {
            yield return null;
            if (axis == ERotateAxis.X)
            {
                this.transform.Rotate(new Vector3(delta, 0, 0));
            }
            else if (axis == ERotateAxis.Y)
            {
                this.transform.Rotate(new Vector3(0, delta, 0));
            }
            else if (axis == ERotateAxis.Z)
            {
                this.transform.Rotate(new Vector3(0, 0, delta));
            }
            value += delta;
        }
        if (value != 90)
        {
            if (axis == ERotateAxis.X)
            {
                this.transform.Rotate(new Vector3(90 - value, 0, 0));
            }
            else if (axis == ERotateAxis.Y)
            {
                this.transform.Rotate(new Vector3(0, 90 - value, 0));
            }
            else if (axis == ERotateAxis.Z)
            {
                this.transform.Rotate(new Vector3(0, 0, 90 - value));
            }
        }
        _IsPlayingAnim = false;
    }
}
