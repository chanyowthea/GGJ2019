using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraManager : MonoBehaviour
{
    [SerializeField] Camera _Camera; 
    [SerializeField] Transform _Corner; 
    [SerializeField] Transform _Center; 

    // Use this for initialization
    void Start()
    {
        var dir = _Center.position - _Corner.position; 
        _Camera.transform.position = dir.normalized * 14f + Vector3.up * 6; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
