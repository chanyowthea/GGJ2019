using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField] Transform _WallLeft;
    [SerializeField] Transform _WallRight;
    [SerializeField] Transform _WallFront;
    [SerializeField] Transform _WallBack;
    [SerializeField] Transform _WallFloor;
    [SerializeField] Transform _WallCeiling;

    [SerializeField] Transform _FrontRightCorner;
    [SerializeField] Transform _BackRightCorner;
    [SerializeField] Transform _FrontLeftCorner;
    [SerializeField] Transform _BackLeftCorner;
    [SerializeField] Transform _RoomCenter;

    private void Start()
    {
        SetData(8);
    }

    public void SetData(int roomSize = 10)
    {
        float halfSize = (roomSize - 1) / 2f; 
        _WallLeft.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallLeft.transform.localEulerAngles = new Vector3(0, 0, 90);
        _WallLeft.transform.localPosition = new Vector3(-halfSize, halfSize, 0); 

        _WallRight.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallRight.transform.localEulerAngles = new Vector3(0, 0, 90);
        _WallRight.transform.localPosition = new Vector3(halfSize, halfSize, 0);

        _WallFront.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallFront.transform.localEulerAngles = new Vector3(0, 90, 90);
        _WallFront.transform.localPosition = new Vector3(0, halfSize, halfSize);

        _WallBack.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallBack.transform.localEulerAngles = new Vector3(0, 90, 90);
        _WallBack.transform.localPosition = new Vector3(0, halfSize, -halfSize);

        _WallFloor.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallFloor.transform.localEulerAngles = new Vector3(0, 0, 0);
        _WallFloor.transform.localPosition = new Vector3(0, 0, 0);

        _WallCeiling.transform.localScale = new Vector3(roomSize, 1, roomSize);
        _WallCeiling.transform.localEulerAngles = new Vector3(0, 0, 0);
        _WallCeiling.transform.localPosition = new Vector3(0, 0, halfSize * 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
