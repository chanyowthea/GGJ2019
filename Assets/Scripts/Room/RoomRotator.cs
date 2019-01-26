using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum ERotateAxis
{
    X,
    Y,
    Z,
}

public class RoomRotator : MonoBehaviour
{
    bool _IsPlayingAnim;
    public Action<ERotateAxis, bool> _OnRotateStart;
    public Action<float> _OnRotating;
    public Action _OnRotateEnd;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Rotate(ERotateAxis axis, bool isClockWise = true)
    {
        if (_IsPlayingAnim)
        {
            return;
        }
        StartCoroutine(RotateRoutine(axis, (int)(ConstValue._RoomRotateSpeed * (isClockWise ? 1 : -1))));
        if (_OnRotateStart != null)
        {
            _OnRotateStart(axis, isClockWise);
        }
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
            if (_OnRotating != null)
            {
                _OnRotating(value / 90f);
            }
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
        if (_OnRotateEnd != null)
        {
            _OnRotateEnd();
        }
    }
}
